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
        public List<SLine> points = new List<SLine>();
        public bool zoom = false;
        public console konsole = new console();
        public double curAngel=0;
        public SLine bufLine;
        
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
        public void initial(RichTextBox rtb, TextBox tb)
        {
            konsole.init(rtb, tb);


        }
        public void changeZoom(MouseEventArgs e)
        {//смена состояние (маштабирвоание/тансформация
            if (curModes == (int)modes.MODE_MOVE && curCaptures != (int)captures.TAKE_NONE)
            {
                if (zoom)
                    zoom = false;
                else
                    zoom = true;
            }
            
        }
       
        
        protected double d(Point a, Point b)
        {//растояние между 2 точками
            return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow(b.Y - a.Y, 2));
        }

        protected Point segmentCenter(Point a, Point b)
        {//середина между 2 точкми
            Point c = a;
            c.X = (a.X + b.X) / 2;
            c.Y = (a.Y + b.Y) / 2;
            return c;
            
        }
        public void drawingDown(MouseEventArgs e) 
        {//при нажатии ЛКМ на холсте
            
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
                                //if (curCaptures == (int)captures.TAKE_CENTR)
                                //{
                                    curLine = (SLine)points[curLineIndex];

                                //}
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
        {//при отпускание ЛКМ на холсте
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
                            applyMatrix(curLineIndex);
                            popMatrix(curLineIndex);

                            drawingSciene();
                            break;
                    }
                    break;
                case (int)penType.poligon:
                    break;
            }
        }
        private bool dForSquare(Point primary, Point curPoint)
        {//находится ли curPoint в окрености primary (окресность = visibility)
            foreach (Point point in getPointsTransform(primary))
                if (d(point, curPoint) <= visibility)
                    return true;
            return false;
        }

        public int getLine(Point midPoint)
        {//возвращает индекс линии, которой принадлежит данная точка
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
                    if (d(line.turnPoint, midPoint) < visibility)
                        curCaptures = (int)captures.TAKE_TURN;
                    else if (d(line.aW, midPoint) < visibility)
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
        { //массив точек для трансформации (см. место где вызываю данную функцию)
            int mathVis = (int) (visibility / ( Math.Sqrt(2)));
            Point a = new Point(primary.X - mathVis, primary.Y - mathVis);
            Point b = new Point(primary.X - mathVis, primary.Y + mathVis);
            Point c = new Point(primary.X + mathVis, primary.Y + mathVis);
            Point d = new Point(primary.X + mathVis, primary.Y - mathVis);
            Point[] arr = new Point[]{a,b,c,d};
            return arr;
        }

        private void applyMatrix(int indexLine)
        {//применить матрицу афинных преобразований, без сброса самой матрицы
            SLine tempLine = points[indexLine];
            Point[] ps = new Point[2];
            ps[0] = tempLine.a;
            ps[1] = tempLine.b;
            tempLine.affinMatrix.TransformPoints(ps);
            tempLine.aW = ps[0];
            tempLine.bW = ps[1];
            //tempLine.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            points[indexLine] = tempLine;
            return;
        }
        private void popMatrix(int indexLine)
        {//применить матрицу афинных преобразований к фигуре и сбросить матрицу афинных преобразований
            SLine tempLine = points[indexLine];
            Point[] ps = new Point[2];
            ps[0] = tempLine.a;
            ps[1] = tempLine.b;
            tempLine.affinMatrix.TransformPoints(ps);
            tempLine.a = ps[0];
            tempLine.b = ps[1];
            //tempLine.aW = ps[0];
            //tempLine.bW = ps[1];
            tempLine.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            points[indexLine] = tempLine;
            return;
        }

        private void popMatrix(SLine tempLine)
        {
            Point[] ps = new Point[2];
            ps[0] = tempLine.a;
            ps[1] = tempLine.b;
            tempLine.affinMatrix.TransformPoints(ps);
            tempLine.a = ps[0];
            tempLine.b = ps[1];
            tempLine.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            return;
        }
        private Point tranformPoint(float ugol, Point pTrance, Point pSelf)
        {//возвращает по некоторому эзетерическому алгоритму точку, за через которую потом будем делать поворот
            Matrix angel = new Matrix(1, 0, 0, 1, 0, 0);
            PointF tempP = pTrance;
            PointF tempPTr = pSelf;
        
            angel.RotateAt(ugol, tempP);
            PointF[] arrCenter = { tempPTr };
            angel.TransformPoints(arrCenter);

            pSelf.X = (int)arrCenter[0].X;
            pSelf.Y = (int)arrCenter[0].Y;
            return pSelf;

        }
        private Point[] tranformPoint(float ugol, Point pTrance, Point[] pSelf)
        {// -||- но массив
           // Console.WriteLine("point one " + pSelf[0].ToString());
            for (int i = 0; i < pSelf.Count(); i++)
            {

                pSelf[i] = tranformPoint(ugol, pTrance, pSelf[i]);

                i++;
            }
         //   Console.WriteLine("point two " + pSelf[0].ToString());
            return pSelf;

        }
        private SLine tranformPoint(float ugol, SLine temp)
        {//100500 функция, которая пытается дать правильные координаты (тщетно)
            Point[] tempArr = {temp.a,temp.b};
            Point c = new Point(40, 40);
            tempArr = tranformPoint(ugol, temp.b, tempArr);
         //   Console.WriteLine(tempArr[0].ToString() + " " + temp.ToString());
            temp.a = tempArr[0];
            temp.b = tempArr[1];
            Console.WriteLine(temp.ToString());
            return temp;


        }
        public void repaireLine(int index)
        {//востановление линии в случае, если произошло переполнение
            SLine temp = new SLine();
            
            temp = points[index];
            temp.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            temp.aW = curLine.a;
            temp.bW = curLine.b;
            points[index] = temp;
        }

        public void drawingScieneOnly()
        {//отрисовки всех фигур + короб (без пост и пред действий)
            //потому что иногда нужна толко отрисовка без других действий

            foreach (SLine line in points)
            {
                primaryPen.Color = line.color;
                bmpGr.DrawLine(primaryPen, line.aW, line.bW);

            }
            SLine tempL = new SLine();
            //отрисовка короба
            try
            {
                if (curModes == (int)modes.MODE_MOVE && curLineIndex != -1)
                {
                    tempL = points[curLineIndex];
                    Console.WriteLine(points[curLineIndex].aW);    
                    if (double.IsNaN(points[curLineIndex].aW.X) || double.IsNaN(points[curLineIndex].aW.Y) || points[curLineIndex].aW.X < -99999 ||
                    points[curLineIndex].aW.X > 99999)
                    {
                        Console.WriteLine(points[curLineIndex].aW);
                        printLine(points[curLineIndex]);
                        //popMatrix(points[curLineIndex]);
                        repaireLine(curLineIndex);
                        return;
                    }
                    bmpGr.DrawPolygon(secondryPen, getPointsTransform(points[curLineIndex].aW));
                    bmpGr.DrawPolygon(secondryPen, getPointsTransform(points[curLineIndex].bW));
                    if (curCaptures != (int) captures.TAKE_TURN)
                    {
                       
                        changeTurnPoint();

                    }
                    applyMatrix(curLineIndex);
                    //popMatrix(curLineIndex);
                    SLine myLine = points[curLineIndex];
                    bmpGr.DrawEllipse(secondryPen, myLine.turnPoint.X, myLine.turnPoint.Y, 5, 5);



                    //Point x = new Point();
                    //Point y = new Point();
                    //x.X = Math.Max(myLine.aW.X, myLine.bW.X);
                    //x.Y = Math.Min(myLine.aW.Y, myLine.bW.Y);

                    //y.X = Math.Min(myLine.aW.X, myLine.bW.X);
                    //y.Y = Math.Max(myLine.aW.Y, myLine.bW.Y);
                    //bmpGr.DrawEllipse(secondryPen,x.X, x.Y, 5, 5);
                    //bmpGr.DrawEllipse(secondryPen, y.X, y.Y, 5, 5);


                }
            }
            catch (StackOverflowException)
                {
                   // points[curLineIndex]= curLine;
                    //applyMatrix(curLineIndex);
                }

            

        }

        public void drawingSciene()
        {//очистка холста, отрисовка, подмена холста
            bmpGr.Clear(Color.White);

            drawingScieneOnly();
            canvas.DrawImage(bmp, 0, 0);
            return;
            
        }
        public void drawingSciene(SLine line)
        {//очистка холста, отрисовка, отрисовка текущей линии (которая меняем), подмена холста
            bmpGr.Clear(Color.White);
            drawingScieneOnly();
            primaryPen.Color = line.color;
            bmpGr.DrawLine(primaryPen, line.aW, line.bW);
            canvas.DrawImage(bmp, 0, 0);
            return;
        }
      
        

        public void drawingSciene(PictureBox pictureBox1, MouseEventArgs e)
        {//отрисовка + выполнение действий пользователя (поворот, маштабирвоание, трансформ, деформ)
            
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
                        case (int)captures.TAKE_TURN:
                           
                            
                            //double angel = findAngel(points[curLineIndex]);
                            double angel = findAngel(points[curLineIndex].turnPoint, e.Location);
                            konsole.Print(angel.ToString());

                            curLine = points[curLineIndex];
                            PointF pf = new PointF(points[curLineIndex].turnPoint.X, points[curLineIndex].turnPoint.Y);

                            points[curLineIndex].affinMatrix.RotateAt((float) (curAngel-angel), pf);
                            curAngel = angel;
                            //curLine = points[curLineIndex];
                            //curLine.turnPoint = e.Location;
                            //points[curLineIndex] = curLine;
                            //changeTurnPoint();
                            konsole.Print(points[curLineIndex].aW.ToString());
                            //Console.WriteLine("AWX" + points[curLineIndex].aW.ToString());

                            
                            drawingScieneOnly();
                            break;


                        case (int)captures.TAKE_PT1:
                            if (zoom)
                            {//если деформ
                                popMatrix(curLineIndex);
                                curLine = points[curLineIndex];
                               // points[curLineIndex]. = e.Location;
                                curLine.a = e.Location;
                                points[curLineIndex] = curLine;
                            }
                            else
                            {//если  маштаб
                                SLine tempLine = points[curLineIndex];
                                tempLine.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
                                printLine(tempLine);
                                float x,y;
                                x = (float)Math.Abs((e.Location.X - tempLine.b.X) / (float)(tempLine.a.X - tempLine.b.X));
                                y = (float)Math.Abs((e.Location.Y - tempLine.b.Y) / (float)(tempLine.a.Y - tempLine.b.Y));
                                if (points[curLineIndex].a.X == points[curLineIndex].b.X ||
                                    points[curLineIndex].aW.X == points[curLineIndex].bW.X)
                                {
                                    //y = 2;
                                    //x = 2;
                                }
                                if (points[curLineIndex].a.Y == points[curLineIndex].b.Y ||
                                   points[curLineIndex].aW.Y == points[curLineIndex].bW.Y)
                                {
                                    //x = 2;
                                 //   y = 2;
                                }


                                popMatrix(curLineIndex);
  
                                Matrix coordinans0 = new Matrix(1, 0, 
                                    0, 1,
                                    -curLine.turnPoint.X, -curLine.turnPoint.Y);
                                points[curLineIndex].affinMatrix.Multiply(coordinans0);
                                
                                popMatrix(curLineIndex);

                                coordinans0 = new Matrix(x, 0, 
                                    0, y,
                                    0, 0);
                                points[curLineIndex].affinMatrix.Multiply(coordinans0);
                                popMatrix(curLineIndex);

                                coordinans0 = new Matrix(1, 0,
                                    0, 1,
                                    curLine.turnPoint.X, curLine.turnPoint.Y);
                                points[curLineIndex].affinMatrix.Multiply(coordinans0);
                                //angel = findAngel(tempLine);



                                popMatrix(curLineIndex);

                            }
                            break;
                        case (int)captures.TAKE_PT2:
                            if (zoom)
                            {
                                popMatrix(curLineIndex);
                                curLine = points[curLineIndex];
                                curLine.b = e.Location;
                                points[curLineIndex] = curLine;
                            }
                            else
                            {

                            }
                            break;
                        case (int)captures.TAKE_CENTR:
                            SLine tempLine3 = points[curLineIndex];
                            Matrix coordinans3 = new Matrix(1,0, 0,1, 
                                (e.Location.X - curPoint.X),
                                (e.Location.Y - curPoint.Y) );


                            tempLine3.affinMatrix.Multiply(coordinans3);
                            curPoint.X = e.Location.X;
                            curPoint.Y = e.Location.Y;

                            break;
                    }
                

                drawingSciene();


            }
                    
        }
        
        public void pointsDebug()
        {//выводил состояние точек (да, у нас не практикуется использовать отладчик)
           // Console.WriteLine("----------------");
            int ptr = 0;
            foreach (SLine line in points)
            {
                //Console.WriteLine(ptr.ToString()+"|"+line.aW.ToString() + " " + line.bW.ToString());
                ptr++;
            }
        }

        public void safeStorage(string path)
        {//сохранить состояние в файл (шас работает с багами)
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
        private void printLine(SLine line)
        {//вывести состояние линии
            Console.WriteLine("!{0}, {1} - {2},{3}",  line.a.X, line.a.Y, line.b.X, line.b.Y);
            Console.WriteLine("!!{0}, {1} - {2},{3}", line.aW.X, line.aW.Y, line.bW.X, line.bW.Y);
        }
        public void loadStorage(string path)
        {//загрузка состяония из файла
            //Console.WriteLine(path);
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
        private void changeTurnPoint(ref SLine line)
        {//изменить точку поворота, ее нужна менять отдельно, каждый раз забываю почему такой апендикс
            line.turnPoint.X = (int)((line.aW.X + line.bW.X) / 2 + 20);
            line.turnPoint.Y = (int)((line.aW.Y + line.bW.Y) / 2 + 35);
            return;
        }

        private void changeTurnPoint()
        {
            SLine cur = points[curLineIndex];
            cur.turnPoint.X = (int)((cur.aW.X + cur.bW.X) / 2 + 20 );
            cur.turnPoint.Y = (int)((cur.aW.Y + cur.bW.Y) / 2 + 35);
            points[curLineIndex] = cur;
        }
        public int findDirection(int lineIndex)
        {//найти угол по индексу линии
            return findDirection(points[lineIndex].aW, points[lineIndex].bW);
        }

        public int findDirection(Point a, Point b)
        {//найти направление отрезка, проходящего через 2 данные точки
            int x,y;

            
            //опередяляем направление вдоль оси Х, х=0 => совпадают с осью
            if (b.X - a.X > 0)
                x = 1;
            else if (b.X - a.X < 0)
                x = -1;
            else
                x = -1;

            if (b.Y - a.Y < 0)
                y = 1;
            else if (b.Y - a.Y > 0)
                y = -1;
            else
                y = -1;
          
            if (x == 1 && y == 1)
                return 3;                
            if (x == -1 && y == 1)
                return 4;
            if (x == -1 && y == -1)
                return 1;
            if (x == 1 && y == -1)
                return 2;
            return 0;
        }
        public double findAngel(SLine line)
        {//найти угол данной линии
            double c = d(line.a, line.b); //длина отрезка
            double a = line.a.X - line.b.X; //проекциия на Х  //поворот на 90 градусов, от того кто а, кто б
            double b = line.a.Y - line.b.Y; // проекция на У

            //return findAngel(a, b, c, findDirection(curLineIndex));


            return findAngel(line.a, line.b);


        }

        public double findAngel(Point A, Point B)
        {//находит угол отрезка проход. через 2 данные точки (функция обертка)
            double c = d(A,B);
            double b = A.Y - B.Y;
            double a = A.X - B.X;
            return findAngel(a, b, c, findDirection(A,B));
        }

        public double findAngel(double a, double b, double c, int direction)
        {//находит угол - тут  вся математика - функция ядро
            if (a == 0) a = 0.000000000001;
            if (c == 0) c = 0.000000000001;
            if (b == 0) b = 0.000000000001;
            double p = (a + b + c)/2;
            double R = (a * b * c) / (4 * Math.Sqrt(p * (p - a) * (p - b) * (p - c)));
            double sinAngel = b / (2 * R);
            double angel = Math.Asin(sinAngel) * (180 / Math.PI);
            sinAngel = angel;
            if (a == 0 || b == 0 || c == 0)
                angel = 0;

            switch (direction)
            {
                case (2):
                    angel = 90 - (-1 * angel) + 90;
                    break;
                case (3):
                    angel = -1 * angel + 180;
                    break;
                case (4):
                    angel = (90-angel) + 270;
                    break;
                case (0):

                    MessageBox.Show("wow!");
                    angel = 999;
                    break;

            }
            return angel;
        }


    }
    
    
    struct SLine
    {//структура хранения (здаровая и не поворотливая)
        public int typeObj;
        public Color color;
        public Point a, b;
        public Point aW, bW;// нудно сделать использование их везде
        public Point turnPoint;
       // public List<Matrix> affinMatrixes; //список матриц афинного преобразования
        public Matrix affinMatrix;
        public List<SLine> figures; //список тоек - для не отрезков (может сделать отрезки частью этого?)
        //public Point[] figures;

        SLine(Point a, Point b) { turnPoint = this.a = aW = a; this.b = bW = b; typeObj = 0; color = Color.DeepSkyBlue; figures = new List<SLine>(); affinMatrix = new Matrix(1,0, 0,1, 0,0); }
        //SLine(Point a, Point b, int t, Color c) { this.a = aW = a; this.b = bW = b; typeObj = t; color = c; figures = new List<SLine>(); affinMatrix = new List<Matrix>(); }
        public void popMatrix()
        {//внутрение применение афинных преобразований
            Point[] ps = new Point[2];
            ps[0] = this.a;
            ps[1] = this.b;
            this.affinMatrix.TransformPoints(ps);
            this.a = ps[0];
            this.b = ps[1];

            this.affinMatrix = new Matrix(1, 0, 0, 1, 0, 0);
            return;
        }

        public override string ToString()
        {//перегрузка метода ToString
            string str = "a:" + this.a.ToString()+" b:"+this.b.ToString()+" color:"+this.color.ToString();//, this.a;
            return str;
        }


        public double d()
        {//растояние как метод
            return Math.Sqrt(Math.Pow((this.b.X - this.a.X), 2) + Math.Pow(this.b.Y - this.a.Y, 2));
        }
    }
}