using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using shareData;


namespace kg_polygon
{
    //состояния: в первую, во вторую, в центр)
    enum captures { TAKE_PT1, TAKE_PT2, TAKE_CENTR, TAKE_NONE };
    //режим рисования: рисования, перемешение, удаление
    enum modes { MODE_DROW, MODE_MOVE, MODE_DELETE };

    public partial class Form1 : Form
    {
        //состояние программы
        editor state = new editor();

        //Timer t = new Timer();
        //t.Tick+= new System.EventHandler(this, t_Tick);
        //void t_Tick(object sender, EventArgs e) { }
        public Form1()
        {
            
            InitializeComponent();
            state.initial(pictureBox1);
            //canvas=pictureBox1.CreateGraphics(); //присваиваем канвасу уазатель, чем он
            state.curModes = (int)modes.MODE_DROW; //режим работы по умолчанию
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void Form1_MouseDown(object sender, MouseEventArgs e){}
        private void Form1_MouseUp(object sender, MouseEventArgs e){}
        private void Form1_MouseMove(object sender, MouseEventArgs e){}

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (state.curModes == (int)modes.MODE_DROW) {
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
            state.DrawingFigure(pictureBox1, e);

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


}
