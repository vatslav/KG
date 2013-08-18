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
        bool isStart = true;
        //List<string> inputComands = new List<string>();
        AutoCompleteStringCollection inputComands = new AutoCompleteStringCollection();
        public void init(RichTextBox consoleWindow, TextBox consoleEntry, editor blob1)
        {
            
            this.consoleEntry = consoleEntry;
            inputComands.Add("scale ");
            inputComands.Add("move ");
            inputComands.Add("rotate ");
            inputComands.Add("help ");
            consoleEntry.AutoCompleteCustomSource = inputComands;
            consoleEntry.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            consoleEntry.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
            



            this.consoleWindow = consoleWindow;
            this.blob = blob1;
            consoleWindow.Text = "Для получения справки по командам консоли введите help" + Environment.NewLine + welcomString ;
            


        }
        public  void execComands()
        {
            buf = consoleEntry.Text;
            buf = buf.Trim();

            consoleEntry.Text = "";
            
            //Environment.NewLine
            if (buf.Length > 0)
            {
                inputComands.Add(buf);
               // buf = buf.Substring(0, buf.Length - 1);
                if (!isStart)
                    consoleWindow.Text += welcomString;
                isStart = false;
                consoleWindow.Text +=  buf + Environment.NewLine;
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
                            blob.aft.moveTo(Convert.ToSingle(substrings[1]), Convert.ToSingle(substrings[2]));
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
                        if (substrings.Length > 4)
                        {
                            answer = "неверный синтаксис scale";
                            return;
                        }
                        try
                        {
                            SLine temp = blob.points[Convert.ToInt32(substrings[3])];
                            float x, y;
                            x = Convert.ToSingle(substrings[1]);
                            y = Convert.ToSingle(substrings[2]);
                            blob.aft.scale2D(ref temp, x, y);

                            blob.points[Convert.ToInt32(substrings[3])] = temp;
                            blob.applyMatrix(Convert.ToInt32(substrings[3]));
                            blob.drawingSciene();
                            answer = success;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            answer = "неверный синтаксис scale";
                        }
                        catch (IndexOutOfRangeException)
                        {
                            answer = "неверный синтаксис scale";
                        }

                }
                else if (buf.StartsWith("rotate"))
                {
                    string[] substrings = Regex.Split(buf, @"\s");
                    if (substrings.Length > 3)
                    {
                        answer = "неверный синтаксис scale";
                        return;
                    }
                    try
                    {
                        int index = Convert.ToInt32(substrings[2]);
                        SLine temp = blob.points[index];
                        Double x;
                        x = Convert.ToDouble(substrings[1]);
                        blob.aft.rotate(ref temp, x);

                        blob.points[index] = temp;
                        blob.applyMatrix(index);
                        blob.drawingSciene();
                        answer = success;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        answer = "неверный синтаксис rotate";
                    }
                    catch (IndexOutOfRangeException)
                    {
                        answer = "неверный синтаксис rotate";
                    }
                }

                else
                    defolt();



                consoleWindow.Text += answer + Environment.NewLine;
                
            }


        }
        public void Print(string str)
        {
            consoleWindow.Text += "                                            " + str;
            consoleWindow.Text = str;
        }
        private void help()
        {
            answer = @"help - справка
move <смещение по оси Х> <смещение по оси Y> <НомерЛинии> 
scale <Коэффициент для оси Х> <Коэффициент для оси Y> <НомерЛинии>
rotate <угол поворота> <НомерЛинии>";
            
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
