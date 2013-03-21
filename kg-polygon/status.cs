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
        public Point curPoint;
        public SLine curLine;
        public int curLineIndex;
        protected int visibility = 4;
        protected Graphics canvas;
        protected PictureBox defaultCanvas;
        public void initial(PictureBox initialForm)
        {
            canvas = initialForm.CreateGraphics();
            defaultCanvas = initialForm;
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
            switch(curModes) 
            {
                case (int)modes.MODE_DROW:
                    isDragging = true;
                    curPoint = e.Location;
                    curLine.a = e.Location;
                    break;

                case (int)modes.MODE_MOVE:
                    //testD(e.Location);
                    int index = getLine(e.Location);
                    //если мы куда попали в фигуру
                    if (curCaptures != (int)captures.TAKE_NONE)
                    {
                        /*включаем режим перетаскивания
                          запоминаем индекс редактируемого элемента
                         пишем редактируемую фигуру во временный контейнер
                         удаляем ее из основного хранилища*/
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
                        DrawingFigure(defaultCanvas, e);
                    }
                    break;
            }
        }

        public void drawingUp(MouseEventArgs e)
        {
            switch(curModes)
            {
                case (int)modes.MODE_DROW:
                    isDragging = false;
                    curLine.b = e.Location;
                    points.Add(curLine);
                    break;
                case (int)modes.MODE_MOVE:
                    isDragging = false;
                    DrawingFigure(null, e);
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
                // Console.WriteLine(d(line.a, midPoint) + d(midPoint, line.b).ToString() + " " + d(line.a, line.b).ToString());

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
        public void DrawingFigure(PictureBox pictureBox1, MouseEventArgs e)
        {
            if (pictureBox1 == null) pictureBox1 = defaultCanvas;

            Pen l1 = new Pen(Color.Blue, 2.0f);//цвет линии
            Pen l2 = new Pen(Color.White, 2.0f);//фон
            //сглаживание
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics bmpGr = Graphics.FromImage(bmp);
            bmpGr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            switch (curModes)
            {
                case (int)modes.MODE_DROW:


                    if (isDragging)
                    {

                        bmpGr.Clear(Color.White);
                        //отрисовка фигур из хранилища
                        foreach (SLine point in points)
                        {
                            bmpGr.DrawLine(l2, point.a, point.b);
                            bmpGr.DrawLine(l1, point.a, point.b);
                        }
                        //отрисовка текущего изображения, того которое тянем
                        bmpGr.DrawLine(l2, curLine.a, curLine.b);
                        bmpGr.DrawLine(l1, curLine.a, e.Location);
                        canvas.DrawImage(bmp, 0, 0);


                    }
                    else
                    {
                        curLine.b = e.Location;//хоть и работает и понимаю что делает, когда мы сюда попадем?
                    }
                    break;
                case (int)modes.MODE_MOVE:


                    bmpGr.Clear(Color.White);
                    if (isDragging)
                    {
                        if (curCaptures!=(int)captures.TAKE_CENTR)
                            curLine = (SLine)points[curLineIndex];
                        SLine tempLine = (SLine)points[curLineIndex];
                        switch (curCaptures)
                        {
                            //меняем кординаты у перетягиваемого изображения прямо в хранилище
                            //готовимся к отрисовке
                            case (int)captures.TAKE_PT1:
                                curLine.a = e.Location;
                                break;
                            case (int)captures.TAKE_PT2:
                                curLine.b = e.Location;
                                break;
                            case (int)captures.TAKE_CENTR:
                                
                                int dx = e.Location.X - curPoint.X;
                                int dy = e.Location.Y - curPoint.Y;

                                tempLine.a.X = Math.Abs(e.Location.X - (curLine.a.X - curPoint.X));
                                tempLine.a.Y = Math.Abs(e.Location.Y - (curLine.a.Y - curPoint.Y));
                                tempLine.b.X = Math.Abs(e.Location.X - (curLine.b.X - curPoint.X));
                                tempLine.b.Y = Math.Abs(e.Location.Y - (curLine.b.Y - curPoint.Y));
                                break;
                        }
                        if (curCaptures != (int)captures.TAKE_CENTR)
                            points[curLineIndex] = (object)curLine;
                        else points[curLineIndex] = (object)tempLine;
                        //отрисовка фигур из хранилища
                        foreach (SLine point in points)
                        {
                            bmpGr.DrawLine(l2, point.a, point.b);
                            bmpGr.DrawLine(l1, point.a, point.b);
                        }
                        //Эффект полупрозачности!
                        ///хоть это и здорово не пойму почему тут получается рисование полузпрозрачным?
                        switch (curCaptures)
                        {
                            case (int)captures.TAKE_PT1:
                                bmpGr.DrawLine(l2, curLine.a, curLine.b);
                                bmpGr.DrawLine(l1, curLine.a, e.Location);
                                break;
                            case (int)captures.TAKE_PT2:
                                bmpGr.DrawLine(l2, curLine.a, curLine.b);
                                bmpGr.DrawLine(l1, e.Location, curLine.b);
                                break;
                        }
                        canvas.DrawImage(bmp, 0, 0);

                    }
                    else
                    {

                        foreach (SLine point in points)
                        {
                            bmpGr.DrawLine(l2, point.a, point.b);
                            bmpGr.DrawLine(l1, point.a, point.b);
                        }
                        canvas.DrawImage(bmp, 0, 0);


                    }


                    break;
                case (int)modes.MODE_DELETE:

                    bmpGr.Clear(Color.White);
                    foreach (SLine point in points)
                    {
                        bmpGr.DrawLine(l2, point.a, point.b);
                        bmpGr.DrawLine(l1, point.a, point.b);
                    }
                    canvas.DrawImage(bmp, 0, 0);
                    break;

            }
        }

        public void DeleteFigure(MouseEventArgs e)
        {

        }

        public void testD(Point e)
        {
            Point a = e;
            Point b = e;
            Point c = e;


            c.X = 5;
            c.Y = 0;

            a.X = 0;
            a.Y = 0;

            b.X = 10;
            b.Y = 0;
            Console.WriteLine(d(a, c).ToString() + " " + d(b, c).ToString() + " " + d(a, b).ToString());

        }
        
        public void pointsDebug()
        {
            Console.WriteLine("----------------");
            foreach (SLine line in points)
            {
                Console.WriteLine(line.a.ToString() + " " + line.b.ToString());
            }
        }


    }
}