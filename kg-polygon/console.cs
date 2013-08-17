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
//using System.Text.RegularExpressions.Regex;
using System.Text.RegularExpressions;

namespace kg_polygon
{
    public class console
    {
        RichTextBox consoleWindow;
        TextBox consoleEntry;
        string buf;
        public console() { }
        string welcomString = "Векторный редактор 2.0> ";
        String answer;
        public void init(RichTextBox consoleWindow, TextBox consoleEntry)
        {
            this.consoleEntry = consoleEntry;
            this.consoleWindow = consoleWindow;


        }
        public  void execComands()
        {
            buf = consoleEntry.Text;
            buf = buf.Trim();

            consoleEntry.Text = "";
            
            //Environment.NewLine
            if (buf.Length > 0)
            {
               // buf = buf.Substring(0, buf.Length - 1);
                consoleWindow.Text += welcomString + buf + Environment.NewLine;
                
                if (buf=="help")
                        help();
                else if (buf.StartsWith("move"))
                {
                    if (equal(@"move \d \d \d"))
                        answer = "верно";
                    else
                        answer = "не верный синтаксис move";
                    


                }
                else
                        defolt();



                        consoleWindow.Text += answer + Environment.NewLine;
            }


        }
        public void Print(string str)
        {
            //consoleWindow.Text += "                                            " + str;
            //consoleWindow.Text = str;
        }
        private void help()
        {
            answer = @"help - справка
move <НомерЛинии> <смещение по оси Х> <смещение по оси Y> ";
            
        }
        private void defolt()
        {
            answer = "Команда не распознана";
        }

        private bool equal(String obj, String pattern)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(obj);
            return match.Success;
        }
        private bool equal(String pattern)
        {
            String obj = buf;
            Regex regex = new Regex(pattern);
            Match match = regex.Match(obj);
            return match.Success;
        }
    }
}
