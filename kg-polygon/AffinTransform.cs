using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kg_polygon;
using System.Drawing;
using shareData;
using System.Diagnostics;
using System.Drawing.Drawing2D;
namespace kg_polygon
{
 public class AffinTransform
 {
  public AffinTransform() { }
  enum scaleMethod { natural, apiScaleOnly, apiScaleMore, mix };
  int curScaleMethod = 0;
  //преобразует масштабирование линия + точка в линии + коеф. Х + коеф. У
  public float[] scale(ref SLine figure, Point curPoint)
  {
   float x, y;
   x = (float)Math.Abs((curPoint.X - figure.bW.X) / (float)(figure.aW.X - figure.bW.X));
   y = (float)Math.Abs((curPoint.Y - figure.bW.Y) / (float)(figure.aW.Y - figure.bW.Y));
   //Debug.WriteLine("" + curPoint.X + " "+ figure.bW.X + " " + figure.aW.X +" " + figure.bW.X);
   //Debug.WriteLine(""+x+ " " + y);
   //Debug.WriteLine("" + x + y);
   
   scale(ref figure, x, y);
   float[] myArray = { x, y };
   return  myArray;
  }

  //масштабирование с использованием матриц (без c# WinForms api)
  private void naturalScale(ref SLine figure, float x, float y)
  {
   float dx = figure.getCentr(0);
   float dy = figure.getCentr(1);
   Debug.WriteLine("ТОЧКИ DXYDY" + dx + " " + dy);
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

  private void Drawing2DScaleOnly(ref SLine figure, float x, float y)
  {
   figure.affinMatrix.Scale(x, y, MatrixOrder.Append);
   figure.applyAffinMatrix();
  }

  private void Drawing2DScaleMore(ref SLine figure, float x, float y)
  {
   float dx = figure.getCentr(0);
   float dy = figure.getCentr(1);  
   figure.affinMatrix.Translate(-dx, -dy, MatrixOrder.Append);
   figure.affinMatrix.Scale(x, y, MatrixOrder.Append);
   figure.affinMatrix.Translate(dx, dy, MatrixOrder.Append);
   figure.applyAffinMatrix();
  }


  //private void 

  //масшатабирование  линии + коеф. Х + коеф. У в разных режимых
  public void scale(ref SLine figure, float x, float y)
  {
   //
   Debug.WriteLine(figure.ToString());
   Debug.WriteLine(figure.ToStringMx());
   naturalScale(ref figure, x, y);
   //Matrix amx = new Matrix(1, 0, 0, 1, -figure.getCentr(0), -figure.getCentr(1));
   //figure.affinMatrix.Multiply(amx);

   ////amx = new Matrix(x, 0, 0, y, 0, 0);
   ////figure.affinMatrix.Multiply(amx);

   //figure.affinMatrix.Scale(x, y);

   //amx = new Matrix(1, 0, 0, 1, figure.getCentr(0), figure.getCentr(1));
   //figure.affinMatrix.Multiply(amx);

   //figure.applyAffinMatrix();




   //figure.affinMatrix.Scale(x, y);
   //figure.applyAffinMatrix();
   //figure.affinMatrix.Shear(figure.getCentr(0), figure.getCentr(1));
   //figure.applyAffinMatrix();
   Debug.WriteLine(figure.ToString());
   Debug.WriteLine(figure.ToStringMx());
  }
 }
}
