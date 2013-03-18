using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace kg_polygon
{
    //состояния: в первую, во вторую, в центр)
    enum captures { TAKE_PT1, TAKE_PT2, TAKE_CENTR, TAKE_NONE };
    //режим рисования: рисования, перемешение, удаление
    enum modes { MODE_DROW, MODE_MOVE, MODE_DELETE };

    public partial class Form1 : Form
    {
        //состояние программы
        status state = new status();
        Graphics canvas; 
        public Form1()
        {
            InitializeComponent();
            canvas=pictureBox1.CreateGraphics(); //присваиваем канвасу уазатель, чем он
            state.curModes = (int)modes.MODE_DROW; //режим работы по умолчанию
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void Form1_MouseDown(object sender, MouseEventArgs e){}
        private void Form1_MouseUp(object sender, MouseEventArgs e){}
        private void Form1_MouseMove(object sender, MouseEventArgs e){}

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (state.curModes == (int)modes.MODE_DROW) {
                Console.WriteLine("NETTTTTTTTTTTTTTTTTTTTT!");
                state.isDragging = true;
                state.curPoint = e.Location;
                state.curLine.a = e.Location;
            }
            else if (state.curModes == (int)modes.MODE_MOVE)
            {
                state.testD(e.Location);
                state.getLine(e.Location);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (state.curModes == (int)modes.MODE_DROW) {
                state.isDragging = false;
                state.curLine.b = e.Location;
                state.points.Add(state.curLine);
            }
            else if (state.curModes == (int)modes.MODE_MOVE)
            {
                state.getLine(e.Location);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (state.curModes == (int)modes.MODE_DROW) {
                Pen l1 = new Pen(Color.Blue, 2.0f);//цвет линии
                Pen l2 = new Pen(Color.White, 2.0f);//фон
                state.pointsDebug();
                
                if (state.isDragging)
                {
                    canvas.Clear(Color.White);
                    foreach (SLine point in state.points)
                    {
                        canvas.DrawLine(l2, point.a, point.b);
                        canvas.DrawLine(l1, point.a, point.b);
                    }

                    canvas.DrawLine(l2, state.curLine.a, state.curLine.b);
                    canvas.DrawLine(l1, state.curLine.a, e.Location);


                }
                else
                {
                    state.curLine.b = e.Location;
                }

            }
            else if (state.curModes == (int)modes.MODE_MOVE)
            {

            }
        }

        private void bEdit_Click(object sender, EventArgs e){ state.curModes = (int)modes.MODE_MOVE;}

        private void bDrow_Click(object sender, EventArgs e)
        {
            state.curModes = (int)modes.MODE_DROW;
        }

        private void bDel_Click(object sender, EventArgs e)
        {
            state.curModes = (int)modes.MODE_DELETE;
        }
    }





    struct SLine
    {
        public Point a, b;
        SLine(Point a, Point b) { this.a = a; this.b = b; }
    }

    
    class status{
        public int curModes;
        public int curCaptures;
        public bool isDragging;
        public Point curPoint;
        public SLine curLine;

        public ArrayList points = new ArrayList();
        //доделать
        protected double d(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow((b.X - a.X),2) + Math.Pow(b.Y-a.Y,2));
            
        }
        public void testD(Point e){
            Point a=e;
            Point b=e;
            Point c=e;
            
            c.X = 5;
            c.Y = 0;

            a.X = 0;
            a.Y = 0;

            b.X = 10;
            b.Y = 0;
            Console.WriteLine(d(a, c).ToString() + " " + d(b, c).ToString() + " " + d(a, b).ToString());

        }
        public Point getLine(Point midPoint)
        {
            int dx = (int) midPoint.X;
            int dy = (int) midPoint.Y;
            foreach (SLine line in points)
            {
               // Console.WriteLine(d(line.a, midPoint) + d(midPoint, line.b).ToString() + " " + d(line.a, line.b).ToString());

                if (d(line.a, midPoint) + d(midPoint, line.b) - d(line.a, line.b) < 1)
                {
                    return line;
                }

                    
               
            }
            return midPoint;
        }

        public void pointsDebug()
        {
            Console.WriteLine("----------------");
            foreach (SLine line in points){
                Console.WriteLine(line.a.ToString()+" "+line.b.ToString());
            }
        }
        
    
    }



}
