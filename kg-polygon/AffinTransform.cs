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
    private double bufAngel;
  //преобразует масштабирование линия + точка в линии + коеф. Х + коеф. У
  public float[] scale(ref SLine figure, Point curPoint)
  {

   float x, y;
   x = (float)Math.Abs((curPoint.X - figure.bW.X) / (float)(figure.aW.X - figure.bW.X) );
   y = (float)Math.Abs((curPoint.Y - figure.bW.Y) / (float)(figure.aW.Y - figure.bW.Y) );
   float[] myArray = { x, y };

   blob.konsole.Print("direcrion="+findDirection() );
   if (double.IsNaN(x) || x == 0)
   {
    x = (float)1;
    if (findDirection() == 2)
    {
        
    }
   }
   if (double.IsNaN(y) || y == 0)
   {
    y = (float)1;
   }
   scale2D(ref figure, x, y);
   return  myArray;
  }

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
   figure.applyAffinMatrix();
  }
  //масшатабирование  линии + коеф. Х + коеф. У в разных режимых
  private void scale2D(ref SLine figure, float x, float y)
  {
   float dx = figure.getCentrX();
   float dy = figure.getCentrY();  
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

    public void rotate(ref SLine figure, Point curPoint)
    {
        double angel = findAngel(blob.curFigure.turnPoint, curPoint);
        rotate(ref figure, angel);
        
    }
     private void rotate(ref SLine figure, double angel)
     {
         PointF centerPointsArr = new PointF(figure.turnPoint.X, figure.turnPoint.Y);

         figure.affinMatrix.RotateAt((float)(bufAngel - angel), centerPointsArr);
         bufAngel = angel;
         blob.drawingScieneOnly();
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
         double c = d(line.a, line.b); //длина отрезка
         double a = line.a.X - line.b.X; //проекциия на Х  //поворот на 90 градусов, от того кто а, кто б
         double b = line.a.Y - line.b.Y; // проекция на У

         //return findAngel(a, b, c, findDirection(curLineIndex));


         return findAngel(line.a, line.b);


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
         if (a == 0 || b == 0 || c == 0)
             angel = 0;

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
  //private void 

  

 }
}
