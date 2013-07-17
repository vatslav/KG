using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kg_polygon;
using System.Drawing;
using shareData;
namespace kg_polygon
{
 public class AffinTransform
 {
  public AffinTransform() { }
  public void scale(ref SLine figure, Point curPoint)
  {
   figure.a = new Point(0,0);

  }
  public void scale(SLine figure, float x, float y)
  {

  }
 }
}
