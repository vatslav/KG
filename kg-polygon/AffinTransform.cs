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
  enum scaleMethod { natural, apiScaleOnly, apiScaleMore, mix };
  //преобразует масштабирование линия + точка в линии + коеф. Х + коеф. У
  public float[] scale(ref SLine figure, Point curPoint)
  {

   float x, y;
   x = (float)Math.Abs((curPoint.X - figure.bW.X) / (float)(figure.aW.X - figure.bW.X) );
   y = (float)Math.Abs((curPoint.Y - figure.bW.Y) / (float)(figure.aW.Y - figure.bW.Y) );
   float[] myArray = { x, y };

   blob.konsole.Print("direcrion="+blob.findDirection(blob.CurLineIndex));
   if (double.IsNaN(x) || x == 0)
   {
    x = (float)1;
   }
   if (double.IsNaN(y) || y == 0)
   {
    y = (float)1;
   }

   
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
   Drawing2DScaleMore(ref figure, x, y);

  }
 }
}
