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
  public AffinTransform() { }
  enum scaleMethod { natural, apiScaleOnly, apiScaleMore, mix };
  //преобразует масштабирование линия + точка в линии + коеф. Х + коеф. У
  public float[] scale(ref SLine figure, Point curPoint)
  {

   float x, y;
   Debug.WriteLine("CURLINE=" + curPoint);
   x = (float)Math.Abs((curPoint.X - figure.bW.X) / (float)(figure.aW.X - figure.bW.X) );
   y = (float)Math.Abs((curPoint.Y - figure.bW.Y) / (float)(figure.aW.Y - figure.bW.Y) );
   float[] myArray = { x, y };
   
   //if (figure.d_aW(curPoint) < 5 || figure.d_bW(curPoint) < 5)
   // return myArray;
   if (double.IsNaN(x) || x == 0)
   {
    x = (float)1;
    // MessageBox.Show(""+0);
   }
   if (double.IsNaN(y) || y == 0)
   {
    y = (float)1;
    //MessageBox.Show("" + 1);
   }
   //Debug.WriteLine("" + curPoint.X + " "+ figure.bW.X + " " + figure.aW.X +" " + figure.bW.X);
   //Debug.WriteLine(""+x+ " " + y);
   //Debug.WriteLine("" + x + y);
   
   scale(ref figure, x, y);
   

   return  myArray;
  }

  //масштабирование с использованием матриц (без c# WinForms api)
  private void naturalScale(ref SLine figure, float x, float y)
  {
   float dx = figure.getCentr(0);
   float dy = figure.getCentr(1);
   //матрица переноса центра в ноль
   Matrix amx = new Matrix(1, 0, 0, 1, -dx, -dy);
   //применяем матрицу 
   figure.affinMatrix.Multiply(amx, MatrixOrder.Append);

   //масштабирование
   Debug.WriteLine("ТОЧКИ XY" + x + " " + y);
   amx = new Matrix(x, 0, 0, y, 0, 0);
   figure.affinMatrix.Multiply(amx, MatrixOrder.Append);

   //матрица возврощения обратно
   amx = new Matrix(1, 0, 0, 1, dx, dy);
   figure.affinMatrix.Multiply(amx, MatrixOrder.Append);
   figure.applyAffinMatrix();
  }

  private void Drawing2DScaleMore(ref SLine figure, float x, float y)
  {
   float dx = figure.getCentr(0);
   float dy = figure.getCentr(1);  
   figure.affinMatrix.Translate(-dx, -dy, MatrixOrder.Append);
   figure.affinMatrix.Scale(x, y, MatrixOrder.Append);
   figure.affinMatrix.Translate(dx, dy, MatrixOrder.Append);
   //figure.applyAffinMatrix();
  }


  //private void 

  //масшатабирование  линии + коеф. Х + коеф. У в разных режимых
  public void scale(ref SLine figure, float x, float y)
  {
   //
   Debug.WriteLine(figure.ToString());
   Debug.WriteLine(figure.ToStringMx());
   Drawing2DScaleMore(ref figure, x, y);
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
