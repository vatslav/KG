using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kg_polygon;
using System.Drawing;
using shareData;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace kg_polygon
{
 public class AffinTransform
 {

    editor blob;
    public AffinTransform(editor blob1) { blob = blob1; }
    public AffinTransform() { }
    private double bufAngel=0;

  //преобразует масштабирование линия + точка в линии + коеф. Х + коеф. У
  public float[] scale(ref SLine figure, Point curPoint, int numberPoint)
  {//TODO
      // если растояние от мышки до края фигуры больше visibility, то нужно найти угол между мышкой и краев
      //относительно центра и повернуть на него - это отдельной песней, можно после перехода на рекнатглы
      //сейчас все остальное доделать
      bool crach = false;
   float x, y;
   x = y = 0;
   int limit = 1;
   int crashAngel = 1;
   if (numberPoint == 1)
   {
       x = (float)Math.Abs((curPoint.X - figure.bW.X) / (float)(figure.aW.X - figure.bW.X));
       y = (float)Math.Abs((curPoint.Y - figure.bW.Y) / (float)(figure.aW.Y - figure.bW.Y));
   }
   else if (numberPoint == 2)
   {
       x = (float)Math.Abs((curPoint.X - figure.aW.X) / (float)(figure.aW.X - figure.bW.X));
       y = (float)Math.Abs((curPoint.Y - figure.aW.Y) / (float)(figure.aW.Y - figure.bW.Y));
   }
   float[] myArray = { x, y };
   try
   {
       blob.konsole.Print("direcrion=" + findDirection() + " angel=" + findAngel(figure).ToString().Substring(0, 5));
       Point center = new Point(figure.getCentrX(), figure.getCentrY());
       //blob.konsole.Print(findAngel(figure.aW,curPoint,  center).ToString());
   }
   catch (ArgumentOutOfRangeException) { }
   if (double.IsNaN(x) || x == 0 || double.IsNaN(y) || y == 0)
       crach = true;

   if (Math.Abs(90 - findAngel(figure)) < limit && findDirection() == 2)
       rotateCrach(ref figure, crashAngel);
   else if (Math.Abs(180 - findAngel(figure)) < limit && findDirection() == 2)
       rotateCrach(ref figure, -crashAngel);
   else if (Math.Abs(90 - findAngel(figure)) < limit && findDirection() == 1)
       rotateCrach(ref figure, -crashAngel);
   else if (findAngel(figure) < limit && findDirection() == 1)
       rotateCrach(ref figure, crashAngel);
   else if (Math.Abs(360 - findAngel(figure)) < limit && findDirection() == 4)
       rotateCrach(ref figure, -crashAngel);
   else if (Math.Abs(270 - findDirection()) < limit && findDirection() == 3)
       rotateCrach(ref figure, crashAngel);
   else if (Math.Abs(270 - findDirection()) < limit && findDirection() == 4)
       rotateCrach(ref figure, -crashAngel);
   if (crach)
       rotateCrach(ref figure, 5);

   if (!crach)
       scale2D(ref figure, x, y);
   return  myArray;
  }
  //public float findAngel(Point a, Point b, Point c);

  //масштабирование с использованием матриц (без c# WinForms api)
  private void naturalScale(ref SLine figure, float x, float y)
  {
   float dx = figure.getRotateX();
   float dy = figure.getRotateY();
   //матрица переноса центра в ноль
   Matrix amx = new Matrix(1, 0, 0, 1, -dx, -dy);
   //применяем матрицу 
   figure.affinMatrix.Multiply(amx, MatrixOrder.Append);

   //масштабирование
   amx = new Matrix(x, 0, 0, y, 0, 0);
   figure.affinMatrix.Multiply(amx, MatrixOrder.Append);

   //матрица возврощения обратно
   amx = new Matrix(1, 0, 0, 1, dx, dy);
   figure.affinMatrix.Multiply(amx, MatrixOrder.Append);
   //figure.applyAffinMatrix();
  }
  //масшатабирование  линии + коеф. Х + коеф. У в разных режимых
  public void scale2D(ref SLine figure, float x, float y)
  {
    float dx = figure.getRotateX();
   float dy = figure.getRotateY();  
   figure.affinMatrix.Translate(-dx, -dy, MatrixOrder.Append);
   figure.affinMatrix.Scale(x, y, MatrixOrder.Append);
   figure.affinMatrix.Translate(dx, dy, MatrixOrder.Append);
  }

  public void scale(ref SLine figure, float x, float y)
  {
      scale2D(ref figure, x, y);
  }



  public void rotate(Point curPoint)
  {
      SLine line = blob.curFigure;
      rotate(ref line, curPoint);
      blob.curFigure = line;
  }

  public void moveTo(float x, float y, int index)
  {
      SLine line = blob.points[index];
      line.affinMatrix = blob.points[index].affinMatrix.Clone();
      line.affinMatrix.Translate(x, y, MatrixOrder.Append);
      blob.points[index] = line;
  }
  public void moveTo(float x, float y)
  {
      moveTo(x, y, blob.CurLineIndex);

  }
    public void rotate(ref SLine figure, Point curPoint)
    {
        double angel = findAngel(blob.curFigure.turnPoint, curPoint);
        rotate(ref figure, angel);
        
    }
     public void rotate(ref SLine figure, double angel)
     {
         PointF centerPointsArr = new PointF(figure.getCentrX(), figure.getCentrY());

         figure.affinMatrix.RotateAt((float)(bufAngel - angel), centerPointsArr,MatrixOrder.Append);
         bufAngel = angel;
         blob.drawingScieneOnly();
     }
     //используется только при граничных условиях в трансформировании
     private void rotateCrach(ref SLine figure, double angel)
     {
         PointF centerPointsArr = new PointF(figure.getCentrX(), figure.getCentrY());
         figure.affinMatrix.RotateAt((float)(angel), centerPointsArr, MatrixOrder.Append);
     }
     public void rotateCrach(ref SLine figure, Point curPoint)
     {
         double angel = findAngel(figure.aW, curPoint);
         blob.konsole.Print("angel rotate=" + angel.ToString());   
         rotateCrach(ref figure, angel);

     }

     //найти угол по индексу линии
     public int findDirection()
     {
         return findDirection(blob.curFigure.aW, blob.curFigure.bW);
     }

     //найти угол по индексу линии
     public int findDirection(ref SLine figure)
     {
         return findDirection(figure.aW, figure.bW);
     }

     public int findDirection(Point a, Point b)
     {//найти направление отрезка, проходящего через 2 данные точки
         int x, y;


         //опередяляем направление вдоль оси Х, х=0 => совпадают с осью
         if (b.X - a.X > 0)
             x = 1;
         else if (b.X - a.X < 0)
             x = -1;
         else
             x = -1;

         if (b.Y - a.Y < 0)
             y = 1;
         else if (b.Y - a.Y > 0)
             y = -1;
         else
             y = -1;

         if (x == 1 && y == 1)
             return 3;
         if (x == -1 && y == 1)
             return 4;
         if (x == -1 && y == -1)
             return 1;
         if (x == 1 && y == -1)
             return 2;
         return 0;
     }
     
     public double findAngel(SLine line)
     {//найти угол данной линии
         double c = d(line.aW, line.bW); //длина отрезка
         double a = line.aW.X - line.bW.X; //проекциия на Х  //поворот на 90 градусов, от того кто а, кто б
         double b = line.aW.Y - line.bW.Y; // проекция на У

         //return findAngel(a, b, c, findDirection(curLineIndex));


         return findAngel(line.aW, line.bW);


     }
     public double findAngel(Point A, Point B, Point C)
     {
         double a = d(A, B);
         double b = d(A, C);
         double c = d(C, B);

         return findAngel(a, b, c, findDirection(B, C));
     }


     public double findAngel(Point A, Point B)
     {//находит угол отрезка проход. через 2 данные точки (функция обертка)
         double c = d(A, B);
         double b = A.Y - B.Y;
         double a = A.X - B.X;
         return findAngel(a, b, c, findDirection(A, B));
     }

     public double findAngel(double a, double b, double c, int direction)
     {//находит уголл
         if (a == 0) a = 0.000000000001;
         if (c == 0) c = 0.000000000001;
         if (b == 0) b = 0.000000000001;
         double p = (a + b + c) / 2;
         double R = (a * b * c) / (4 * Math.Sqrt(p * (p - a) * (p - b) * (p - c)));
         double sinAngel = b / (2 * R);
         double angel = Math.Asin(sinAngel) * (180 / Math.PI);
         sinAngel = angel;


         switch (direction)
         {
             case (2):
                 angel = 90 - (-1 * angel) + 90;
                 break;
             case (3):
                 angel = -1 * angel + 180;
                 break;
             case (4):
                 angel = (90 - angel) + 270;
                 break;

         }
         return angel;
     }
     public void handScale(string scaleCoef)
     {
         float scaleXY;
         SLine tempLine;
         try
         {
             scaleXY = (float)Convert.ToDouble(scaleCoef);
             tempLine = blob.curFigure;
         }
         catch (FormatException)
         { return; }
         catch (ArgumentOutOfRangeException)
         { return; }

         AffinTransform aft = new AffinTransform();
         aft.scale(ref tempLine, scaleXY, scaleXY);
         blob.curFigure = tempLine;
     }

     protected double d(Point a, Point b)
     {//растояние между 2 точками
         return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow(b.Y - a.Y, 2));
     }




  

 }
}
