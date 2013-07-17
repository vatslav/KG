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
  public void scale(ref SLine figure, Point curPoint)
  {
   float x, y;
   x = (float)Math.Abs((curPoint.X - figure.bW.X) / (float)(figure.aW.X - figure.bW.X));
   y = (float)Math.Abs((curPoint.Y - figure.bW.Y) / (float)(figure.aW.Y - figure.bW.Y));
   //Debug.WriteLine("" + curPoint.X + " "+ figure.bW.X + " " + figure.aW.X +" " + figure.bW.X);
   //Debug.WriteLine(""+x+ " " + y);
   //Debug.WriteLine("" + x + y);

   scale(figure, x, y);
  
   return;
  }
  public void scale( SLine figure, float x, float y)
  {
   Debug.WriteLine(figure.ToString());
   Debug.WriteLine(figure.ToStringMx());
   figure.affinMatrix = new Matrix();
   //figure.affinMatrix.Scale(x, y);
  }
 }
}
