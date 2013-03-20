﻿using System;
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
        public void initial(PictureBox initialForm)
        {
            canvas = initialForm.CreateGraphics();
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
        public int getLine(Point midPoint)
        {
            int dx = (int)midPoint.X;
            int dy = (int)midPoint.Y;
            int ptr = 0;
            foreach (SLine line in points)
            {
                // Console.WriteLine(d(line.a, midPoint) + d(midPoint, line.b).ToString() + " " + d(line.a, line.b).ToString());

                if (d(line.a, midPoint) + d(midPoint, line.b) - d(line.a, line.b) < 1)
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
            Pen l1 = new Pen(Color.Blue, 2.0f);//цвет линии
            Pen l2 = new Pen(Color.White, 2.0f);//фон
            //сглаживание
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics bmpGr = Graphics.FromImage(bmp);
            bmpGr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            bmpGr.Clear(Color.White);

            bmpGr.Clear(Color.White);
            if (curModes == (int)modes.MODE_DROW) 
            {
                
                if (isDragging)
                {

                    //отрисовка фигур из хранилища
                    foreach (SLine point in points)
                    {
                        bmpGr.DrawLine(l2, point.a, point.b);
                        bmpGr.DrawLine(l1, point.a, point.b);
                    }
                    //отрисовка текущего изображения, того которое тянем
                    bmpGr.DrawLine(l2, curLine.a, curLine.b);
                    bmpGr.DrawLine(l1, curLine.a, e.Location);
                    

                }
                else
                {
                    curLine.b = e.Location;//хоть и работает и понмаю что делает, когда мы сюда попадем?
                }
            }
            else if (curModes == (int)modes.MODE_MOVE)
            {
                if (isDragging)
                {
                    switch (curCaptures)
                    {
                        case (int)captures.TAKE_PT1:
                            //отрисовка текущего изображения, того которое тянем
                            curLine = (SLine)points[curLineIndex];
                            curLine.a = e.Location;
                            points[curLineIndex] = (object)curLine;
                            break;
                        case (int)captures.TAKE_PT2:
                            //отрисовка текущего изображения, того которое тянем
                            curLine = (SLine)points[curLineIndex];
                            curLine.a = e.Location;
                            points[curLineIndex] = (object)curLine;
                            break;
                    }
                    //отрисовка фигур из хранилища
                    foreach (SLine point in points)
                    {
                        bmpGr.DrawLine(l2, point.a, point.b);
                        bmpGr.DrawLine(l1, point.a, point.b);
                    }

                }
            }
            //ставим на место
            canvas.DrawImage(bmp, 0, 0);
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