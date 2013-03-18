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
            canvas=pictureBox1.CreateGraphics();
        }

        private void pictureBox1_Click(object sender, EventArgs e){}

        private void Form1_MouseDown(object sender, MouseEventArgs e){}

        private void Form1_MouseUp(object sender, MouseEventArgs e){}

        private void Form1_MouseMove(object sender, MouseEventArgs e){}

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            state.isDragging = true;
            state.curPoint = e.Location;
            state.curLine.a = e.Location;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            state.isDragging = false;
            state.curLine.b = e.Location;
            state.points.Add(state.curLine);

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen l1 = new Pen(Color.Blue);//цвет линии
            Pen l2 = new Pen(Color.White);//фон
            state.pointsDebug();
            Console.WriteLine("drging=" + state.isDragging.ToString());
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
        public Point getLine(Point point) { return point; }
        public void pointsDebug(){
            Console.WriteLine("----------------");
            foreach (SLine line in points){
                Console.WriteLine(line.a.ToString()+" "+line.b.ToString());
            }
        }
        
    
    }



}
