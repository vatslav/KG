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
    public class console
    {
        RichTextBox consoleWindow;
        TextBox consoleEntry;
        string buf;
        public console() { }
        public void init(RichTextBox consoleWindow, TextBox consoleEntry)
        {
            this.consoleEntry = consoleEntry;
            this.consoleWindow = consoleWindow;


        }
        public void execComands()
        {
            buf = consoleEntry.Text;
            consoleWindow.Text += "                                            " + buf;
            //execComands

        }
        public void Print(string str)
        {
            //consoleWindow.Text += "                                            " + str;
            consoleWindow.Text = str;
        }
    }
}
