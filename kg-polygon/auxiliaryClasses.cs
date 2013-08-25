using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using kg_polygon;
using drawingObjects;
using System.Drawing.Drawing2D;

namespace kg_polygon
{
    public static class aux
    {



    public static int mxL(int lenth)
    {
        if (lenth > 5)
            return 5;
        else
            return lenth;

    }
    public static String substr(String str)
    {
        int end = 5;
        if (str.Length < end)
            end = str.Length;
        return str.Substring(0, end);
    }




    }



}
