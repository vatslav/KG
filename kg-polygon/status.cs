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
namespace shareData
{
    class editor
    { 
        public int curModes;
        public int curCaptures;
        public bool isDragging;
        public int pen;
        public Point curPoint;
        public SLine curLine;
        public int curLineIndex;
        protected int visibility = 4;
        protected Graphics canvas;
        protected PictureBox defaultCanvas;
       // protected Pen l1 = new Pen(Color.Blue, 2.0f);//цвет линии
        protected Pen blade = new Pen(Color.Blue, 2.0f);//фон
        //сглаживание
        protected Bitmap bmp;
        protected Graphics bmpGr;
        
        public void initial(PictureBox initialForm)
        {
            canvas = initialForm.CreateGraphics();
            defaultCanvas = initialForm;
            bmp  = new Bitmap(initialForm.Width, initialForm.Height);
            bmpGr = Graphics.FromImage(bmp);
            bmpGr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
        }

        public ArrayList points = new ArrayList();
        
        protected double d(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow(b.Y - a.Y, 2));
        }
        protected Point segmentCenter(Point a, Point b)
        {
            Point c = a;
            c.X = (a.X + b.X) / 2;
            c.Y = (a.Y + b.Y) / 2;
            return c;
        }
        public void drawingDown(MouseEventArgs e) 
        {
            //тут потом сделать правильно не всегда делать ее блек
            
            switch (pen)
            {
                
                case (int)penType.line:
                    switch(curModes) 
                    {
                        case (int)modes.MODE_DROW:
                            isDragging = true;
                            curPoint = e.Location;
                            curLine.a = e.Location;
                            curLine.color = Color.Black;
                            break;

                        case (int)modes.MODE_MOVE:
                            int index = getLine(e.Location);
                            //если мы куда попали в фигуру
                            if (curCaptures != (int)captures.TAKE_NONE)
                            {
                                isDragging = true;
                                curLineIndex = index;
                                curPoint = e.Location;
                                if (curCaptures ==(int) captures.TAKE_CENTR)
                                    curLine = (SLine)points[curLineIndex];

                            }
                            break;

                

                        case (int)modes.MODE_DELETE:
                            curLineIndex = getLine(e.Location);
                            if (curLineIndex != -1)
                            {
                                points.RemoveAt(curLineIndex);
                                drawingSciene(defaultCanvas, e);
                            }
                            break;
                    }
                    break;
                case (int) penType.poligon:
                    break;
            }
        }

        public void drawingUp(MouseEventArgs e)
        {
            switch (pen)
            {
                case (int)penType.line:
                    switch (curModes)
                    {
                        case (int)modes.MODE_DROW:
                            isDragging = false;
                            curLine.b = e.Location;
                            points.Add(curLine);
                            break;
                        case (int)modes.MODE_MOVE:
                            isDragging = false;
                            drawingSciene();
                            break;
                    }
                    break;
                case (int)penType.poligon:
                    break;
            }
        }


        public int getLine(Point midPoint)
        {
            int dx = (int)midPoint.X;
            int dy = (int)midPoint.Y;
            int ptr = 0;
            foreach (SLine line in points)
            {
                if (d(line.a, midPoint) + d(midPoint, line.b) - d(line.a, line.b) < visibility)
                {

                    if (d(line.a, midPoint) < visibility)
                        curCaptures = (int)captures.TAKE_PT1;
                    else if (d(line.b, midPoint) < visibility)
                        curCaptures = (int)captures.TAKE_PT2;
                    else curCaptures = (int)captures.TAKE_CENTR;
                    return ptr;
                }

                ptr++;

            }
            curCaptures = (int)captures.TAKE_NONE;
            return -1;
        }

        public void drawingScieneOnly()
        {
            foreach (SLine line in points)
            {
                blade.Color = line.color;
                bmpGr.DrawLine(blade, line.a, line.b);
            }
        }

        public void drawingSciene()
        {
            bmpGr.Clear(Color.White);
            drawingScieneOnly();
            canvas.DrawImage(bmp, 0, 0);
            return;
        }
        public void drawingSciene(SLine line)
        {
            bmpGr.Clear(Color.White);
            drawingScieneOnly();
            blade.Color = line.color;
            bmpGr.DrawLine(blade, line.a, line.b);
            canvas.DrawImage(bmp, 0, 0);
            return;
        }
        

        public void drawingSciene(PictureBox pictureBox1, MouseEventArgs e)
        {

            switch (curModes)
            {
                case (int)modes.MODE_DROW:
                    curLine.b = e.Location;
                    if (isDragging)
                        drawingSciene(curLine);
                    break;
                case (int)modes.MODE_MOVE:
                    if (isDragging)
                    {

                        switch (curCaptures)
                        {
                            //меняем кординаты у перетягиваемого изображения прямо в хранилище
                            //готовимся к отрисовке
                            case (int)captures.TAKE_PT1:
                                curLine = (SLine)points[curLineIndex];
                                curLine.a = e.Location;
                                points[curLineIndex] = (object)curLine;
                                break;
                            case (int)captures.TAKE_PT2:
                                curLine = (SLine)points[curLineIndex];
                                curLine.b = e.Location;
                                points[curLineIndex] = (object)curLine;
                                break;
                            case (int)captures.TAKE_CENTR:
                                SLine tempLine = (SLine)points[curLineIndex];
                                tempLine.a.X = e.Location.X + (curLine.a.X - curPoint.X);
                                tempLine.a.Y = e.Location.Y + (curLine.a.Y - curPoint.Y);
                                tempLine.b.X = e.Location.X + (curLine.b.X - curPoint.X);
                                tempLine.b.Y = e.Location.Y + (curLine.b.Y - curPoint.Y);
                                points[curLineIndex] = (object)tempLine;
                                break;
                        }
                        drawingSciene();
                    }
                    break;
                case (int)modes.MODE_DELETE:
                    drawingSciene();
                    break;

            }
        }
        
        public void pointsDebug()
        {
            Console.WriteLine("----------------");
            int ptr = 0;
            foreach (SLine line in points)
            {
                Console.WriteLine(ptr.ToString()+"|"+line.a.ToString() + " " + line.b.ToString());
                ptr++;
            }
        }

        public void safeStorage(string path)
        {
            System.IO.StreamWriter textFile = new System.IO.StreamWriter(path);
            try
            {
                foreach (SLine line in points)
                {
                   // textFile.WriteLine(line.a.ToString() + " " + line.b.ToString());
                    textFile.WriteLine("{4} {0},{1} {2},{3}", line.a.X, line.a.Y, line.b.X, line.b.Y, line.typeObj);
                }

            }
            catch (Exception ex)
            {
                textFile.Close();
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            textFile.Close();
        }

        public void loadStorage(string path)
        {
            
            char[] delimeterChar = { ' ', ',' };
            string[] lines = System.IO.File.ReadAllLines(path);
            SLine tempLine = curLine; ;
            string[] tempStr;

            ArrayList tempArr = new ArrayList();
            tempArr.Clear();
            int ptr = 0;
            foreach (string line in lines)
            {
                if (line == "") break;
                tempStr = line.Split(delimeterChar);
                tempLine.typeObj = Convert.ToInt32(tempStr[0]);
                tempLine.a.X = Convert.ToInt32(tempStr[1]);
                tempLine.a.Y = Convert.ToInt32(tempStr[2]);
                tempLine.b.X = Convert.ToInt32(tempStr[3]);
                tempLine.b.Y = Convert.ToInt32(tempStr[4]);
                tempArr.Add(tempLine);
                ptr++;
            }
            points.Clear();
            foreach (object obj in tempArr)
              points.Add((SLine)obj);
            drawingSciene();
            tempArr.Clear();
            

        }


    }
    
    struct SLine
    {
        public int typeObj;
        public Color color;
        public Point a, b;
        public Point aW, bW;
        SLine(Point a, Point b) { this.a = aW = a; this.b = bW = b; typeObj = 0; color = Color.DeepSkyBlue; }
        SLine(Point a, Point b, int t, Color c) { this.a = aW= a; this.b = bW= b; typeObj = t; color = c; }
        //SLine() { typeObj = 0; }
        //public int typeObj;
        

    }
}