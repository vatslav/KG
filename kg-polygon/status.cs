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
                            curLine.aW = e.Location;
                            curLine.color = Color.Black;
                            curLine.affinMatrix = new Matrix(1,0, 0,1, 0,0); 
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
                            curLine.bW = e.Location;
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
                
                Console.WriteLine("||{0} + {1} - {2} = {4} < {3} ", d(line.aW, midPoint), d(midPoint, line.bW), d(line.aW, line.bW), visibility, d(line.aW, midPoint) + d(midPoint, line.bW) - d(line.aW, line.bW));
                if (d(line.aW, midPoint) + d(midPoint, line.bW) - d(line.aW, line.bW) < visibility || dForSquare(line.aW, midPoint) || dForSquare(line.bW,midPoint) )
                {
                    Console.WriteLine("POPAL");

                    if (d(line.aW, midPoint) < visibility)
                        curCaptures = (int)captures.TAKE_PT1;
                    else if (d(line.bW, midPoint) < visibility)
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

        private void applyMatrix(int indexLine)
        {
            SLine tempLine = points[indexLine];
            Point[] ps = new Point[2];
            ps[0] = tempLine.a;
            ps[1] = tempLine.b;
            tempLine.affinMatrix.TransformPoints(ps);
            tempLine.aW = ps[0];
            tempLine.bW = ps[1];
            points[indexLine] = tempLine;
            return;
        }
        private void popMatrix(int indexLine)
        {
            SLine tempLine = points[indexLine];
            Point[] ps = new Point[2];
            ps[0] = tempLine.a;
            ps[1] = tempLine.b;
            tempLine.affinMatrix.TransformPoints(ps);
            tempLine.a = ps[0];
            tempLine.b = ps[1];
            tempLine.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            points[indexLine] = tempLine;
            return;
        }

        public void drawingScieneOnly()
        {

            foreach (SLine line in points)
            {
                primaryPen.Color = line.color;
                bmpGr.DrawLine(primaryPen, line.aW, line.bW);

            }

            //отрисовка короба
            if (curModes == (int)modes.MODE_MOVE && curLineIndex != -1)
            {
                bmpGr.DrawPolygon(secondryPen, getPointsTransform(points[curLineIndex].aW));
                bmpGr.DrawPolygon(secondryPen, getPointsTransform(points[curLineIndex].bW));


                applyMatrix(curLineIndex);
                SLine tempLine = points[curLineIndex];
                if (tempLine.bW.X - tempLine.aW.X == 0)
                    tempLine.bW.X += 1;
                double gip = (int) d(tempLine.aW,tempLine.bW);
                double xN = tempLine.bW.X - tempLine.aW.X;
                double yN = Math.Sqrt((xN * xN) + (gip * gip));
                double k = yN * (3.14 / 180); //(double)(tempLine.bW.Y - tempLine.aW.Y) / (double)(tempLine.bW.X - tempLine.aW.X);
                double shisl = tempLine.bW.Y - tempLine.aW.Y;
                double znam = tempLine.bW.X - tempLine.aW.X;
                double Fi = (90-Math.Atan(k)) ;
                
                int c = 200;
                int dC = (int)(d(tempLine.aW, tempLine.bW));
                int x = (int)(  Math.Cos(Fi)) * c +((tempLine.aW.X + tempLine.bW.X)/2 );
                int y = (int)( Math.Sin(Fi)) * c  +((tempLine.aW.Y + tempLine.bW.Y) / 2) ;
                Console.WriteLine("k={0}, Fi={1}, x={2}, y={3} ||  atan=k{4}, yN = {5}", k, Fi, x, y, Math.Atan(k), yN);
               // Console.WriteLine("bY={0}, aW.Y={1}, bw.X={2}, bw.Y={3}",tempLine.bW.Y, tempLine.aW.Y, tempLine.bW.X, tempLine.aW.X);
                
                Point trancePoint = new Point(x,y );
                
               bmpGr.DrawEllipse(secondryPen, trancePoint.X, trancePoint.Y, 5, 5);

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
            bmpGr.DrawLine(primaryPen, line.aW, line.bW);
            canvas.DrawImage(bmp, 0, 0);
            return;
        }
        

        public void drawingSciene(PictureBox pictureBox1, MouseEventArgs e)
        {
            
            if (curModes == (int)modes.MODE_DROW)
            {

                curLine.bW = e.Location;
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
                            popMatrix(curLineIndex);
                            curLine = points[curLineIndex];
                            curLine.a = e.Location;
                            points[curLineIndex] = curLine;
                            break;
                        case (int)captures.TAKE_PT2:
                            popMatrix(curLineIndex);
                            curLine = points[curLineIndex];
                            curLine.b = e.Location;
                            points[curLineIndex] = curLine;
                            break;
                        case (int)captures.TAKE_CENTR:
                            SLine tempLine = points[curLineIndex];
                            Matrix coordinans = new Matrix(1,0, 0,1, 
                                (e.Location.X - curPoint.X),
                                (e.Location.Y - curPoint.Y) );


                            tempLine.affinMatrix.Multiply(coordinans);
                            curPoint.X = e.Location.X;
                            curPoint.Y = e.Location.Y;

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
                Console.WriteLine(ptr.ToString()+"|"+line.aW.ToString() + " " + line.bW.ToString());
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
                    textFile.WriteLine("{4} {0},{1} {2},{3}", line.aW.X, line.aW.Y, line.bW.X, line.bW.Y, line.typeObj);
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