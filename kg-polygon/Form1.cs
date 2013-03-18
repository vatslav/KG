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

    public partial class Form1 : Form
    {
        //состояние программы
        status state;
        Graphics canvas; 
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }





    struct SLine
    {
        public Point a, b;
        SLine(Point a, Point b) { this.a = a; this.b = b; }
    }

    
    class status{
        //состояния: в первую, во вторую, в центр)
        public enum captures { TAKE_PT1, TAKE_PT2, TAKE_CENTR, TAKE_NONE };
        //режим рисования: рисования, перемешение, удаление
        public enum modes { MODE_DROW, MODE_MOVE, MODE_DELETE };
        Point curPoint;
        SLine curLine;

        public ArrayList points = new ArrayList();
        //доделать
        public Point getLine(Point point) { return point; }
        
    
    }



}
