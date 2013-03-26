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
        protected int visibility = 15;
        protected Graphics canvas;
        protected PictureBox defaultCanvas;
        protected Pen primaryPen = new Pen(Color.Blue, 2.0f);//линия
        protected Pen secondryPen = new Pen(Color.DarkOrange, 1.0f);//лиkния
        protected Bitmap bmp;
        protected Graphics bmpGr;         //сглаживание
        protected int dx, dy;
        
        public void initial(PictureBox initialForm)
        {
            canvas = initialForm.CreateGraphics();
            defaultCanvas = initialForm;
            bmp  = new Bitmap(initialForm.Width, initialForm.Height);
            bmpGr = Graphics.FromImage(bmp);
            bmpGr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (true)
            {
                curModes = (int) modes.MODE_DROW;
                //string path = @ "\\vmware-host\Shared Folders\Документы\1.vff";
                //loadStorage("\\\\vmware-host\\Shared Folders\\Документы\\1.vff");

            }
            
        }
        private Point addition(Point a, Point b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }

        public List<SLine> points = new List<SLine>();
        
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
                                if (curCaptures == (int)captures.TAKE_CENTR)
                                {
                                    curLine = (SLine)points[curLineIndex];
                                    dx = e.Location.X - curLine.a.X ;
                                    dy = e.Location.Y - curLine.a.Y ;
                                }
                            }
                            break;

                

                        case (int)modes.MODE_DELETE:
                            curLineIndex = getLine(e.Location);
                            if (curLineIndex != -1)
                            {
                                points.RemoveAt(curLineIndex);
                                drawingSciene();
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
        private bool dForSquare(Point primary, Point curPoint)
        {
            foreach (Point point in getPointsTransform(primary))
                if (d(point, curPoint) <= visibility)
                    return true;
            return false;
        }

        public int getLine(Point midPoint)
        {
            int ptr = 0;
            foreach (SLine line in points)
            {
                foreach (Point p in getPointsTransform(midPoint))
                {
                    Console.WriteLine("{0} - {1}",p, d(p,midPoint));
                    
                }
                
                Console.WriteLine("||{0} + {1} - {2} = {4} < {3} ", d(line.a, midPoint), d(midPoint, line.b), d(line.a, line.b), visibility, d(line.a, midPoint) + d(midPoint, line.b) - d(line.a, line.b));
                if (d(line.a, midPoint) + d(midPoint, line.b) - d(line.a, line.b) < visibility || dForSquare(line.a, midPoint) || dForSquare(line.b,midPoint) )
                {
                    Console.WriteLine("POPAL");

                    if (d(line.a, midPoint) < visibility)
                        curCaptures = (int)captures.TAKE_PT1;
                    else if (d(line.b, midPoint) < visibility)
                        curCaptures = (int)captures.TAKE_PT2;
                    else 
                        curCaptures = (int)captures.TAKE_CENTR;
                    return ptr;
                }

                ptr++;

            }
            curCaptures = (int)captures.TAKE_NONE;
            return -1;
        }
        protected Point[] getPointsTransform(Point primary)
        { //тут должно быть еще умножение на угол
            int mathVis = (int) (visibility / ( Math.Sqrt(2)));
            Point a = new Point(primary.X - mathVis, primary.Y - mathVis);
            Point b = new Point(primary.X - mathVis, primary.Y + mathVis);
            Point c = new Point(primary.X + mathVis, primary.Y + mathVis);
            Point d = new Point(primary.X + mathVis, primary.Y - mathVis);
            Point[] arr = new Point[]{a,b,c,d};
            return arr;
        }

        public void drawingScieneOnly()
        {
            foreach (SLine line in points)
            {
                primaryPen.Color = line.color;
                bmpGr.DrawLine(primaryPen, line.a, line.b);
            }
            if (curModes == (int)modes.MODE_MOVE && curLineIndex!=-1)
            {
                bmpGr.DrawPolygon(secondryPen, getPointsTransform(points[curLineIndex].a));
                bmpGr.DrawPolygon(secondryPen, getPointsTransform(points[curLineIndex].b));
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
            primaryPen.Color = line.color;
            bmpGr.DrawLine(primaryPen, line.a, line.b);
            canvas.DrawImage(bmp, 0, 0);
            return;
        }
        

        public void drawingSciene(PictureBox pictureBox1, MouseEventArgs e)
        {

            if (curModes == (int)modes.MODE_DROW)
            {

                curLine.b = e.Location;
                if (isDragging)
                    drawingSciene(curLine);
            }

            else
            {
                
                if (isDragging)
                    switch (curCaptures)
                    {
                        //меняем кординаты у перетягиваемого изображения прямо в хранилище
                        //готовимся к отрисовке
                        case (int)captures.TAKE_PT1:
                            
                            curLine = points[curLineIndex];
                            curLine.a = e.Location;
                            points[curLineIndex] = curLine;
                            break;
                        case (int)captures.TAKE_PT2:
                            curLine = points[curLineIndex];
                            curLine.b = e.Location;
                            points[curLineIndex] = curLine;
                            break;
                        case (int)captures.TAKE_CENTR:
                            SLine tempLine = points[curLineIndex];
                            //tempLine.a.X = e.Location.X + (curLine.a.X - curPoint.X);
                            //tempLine.a.Y = e.Location.Y + (curLine.a.Y - curPoint.Y);
                            //tempLine.b.X = e.Location.X + (curLine.b.X - curPoint.X);
                            //tempLine.b.Y = e.Location.Y + (curLine.b.Y - curPoint.Y);
                            //Point[] ps;
                            tempLine.affinMatrix = new Matrix(1, 0, 0, 1, e.Location.X - curPoint.X, e.Location.Y - curPoint.Y);
                            curPoint.X = e.Location.X;
                            curPoint.Y = e.Location.Y;
                            Point[] ps = new Point[2];
                            ps[0]=tempLine.a;
                            ps[1]=tempLine.b;
                            tempLine.affinMatrix.TransformPoints(ps);
                            tempLine.a = ps[0];
                            tempLine.b = ps[1];
                           // ps = {tempLine.a, tempLine.b};
                            //Point p = new Point(1, 0);

                            //Point[] ps = {new Point( 1, 0), new Point(0, 0), new Point(1, 0), 
                            //              new Point(e.Location.X - curPoint.X), new Point (e.Location.Y - curPoint.Y), 1 };
                            //tempLine.affinMatrix = new Matrix(1, 0, 0, 0, 1, 0, e.Location.X - curPoint.X, e.Location.Y - curPoint.Y, 1);
                            points[curLineIndex] = tempLine;
                            break;
                    }
                

                drawingSciene();


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
            Console.WriteLine(path);
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
        public Point aW, bW;// нудно сделать использование их везде
       // public List<Matrix> affinMatrixes; //список матриц афинного преобразования
        public Matrix affinMatrix;
        public List<SLine> figures; //список тоек - для не отрезков (может сделать отрезки частью этого?)
        //public Point[] figures;

        SLine(Point a, Point b) { this.a = aW = a; this.b = bW = b; typeObj = 0; color = Color.DeepSkyBlue; figures = new List<SLine>(); affinMatrix = new Matrix(); }
        //SLine(Point a, Point b, int t, Color c) { this.a = aW = a; this.b = bW = b; typeObj = t; color = c; figures = new List<SLine>(); affinMatrix = new List<Matrix>(); }

    }
}