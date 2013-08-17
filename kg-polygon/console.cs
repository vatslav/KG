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
using shareData;
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
        String success = "преобразование выполнено успешно";
        editor blob;
        public void init(RichTextBox consoleWindow, TextBox consoleEntry, editor blob1)
        {
            
            this.consoleEntry = consoleEntry;
            this.consoleWindow = consoleWindow;
            this.blob = blob1;
            
            


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
                buf = buf.Replace(".", ",");
                if (buf=="help")
                        help();
                else if (buf.StartsWith("move"))
                {
                    if (equal(@"move -?\d* -?\d* -?\d*"))
                    {
                        string[] substrings = Regex.Split(buf, @"\s");
                        try
                        {
                            blob.points[Convert.ToInt32(substrings[3])].affinMatrix.Translate(Convert.ToSingle(substrings[1]), Convert.ToSingle(substrings[2]), MatrixOrder.Append);
                            blob.applyMatrix(Convert.ToInt32(substrings[3]));
                            blob.drawingSciene();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            answer = "move: линии с таким номером не существует";
                        }
                        answer = success;
                    }
                    else
                        answer = "не верный синтаксис move";
                }

                else if (buf.StartsWith("scale"))
                {
                    
                        string[] substrings = Regex.Split(buf, @"\s");
                        
                        try
                        {
                            SLine temp = blob.points[Convert.ToInt32(substrings[3])];
                            float x, y;
                            x = Convert.ToSingle(substrings[1]);
                            y= Convert.ToSingle(substrings[2]);
                            blob.aft.scale2D(ref temp, x, y);
                            blob.drawingSciene();
                            blob.points[Convert.ToInt32(substrings[3])] = temp;
                            answer = success;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            answer = "не верный синтаксис scale";
                        }
                        


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
