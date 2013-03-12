using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Collections;


namespace KG_lab1_sharp
{        
    //состояния: в первую, во вторую, в центр)
    public enum captures { TAKE_PT1, TAKE_PT2, TAKE_CENTR, TAKE_NONE };
    //режим рисования: рисования, перемешение, удаление
    public enum modes { MODE_DROW, MODE_MOVE, MODE_DELETE };

    public struct SLine
    {
        public Point p1, p2;
    } 
 
   public partial class Form1 : Form
   {
       public bool down = false;
       public bool drawEnds = false;

       figure lines = new figure();

        public Form1()
        {
            InitializeComponent();
            

        }

        private void pictureBox1_Click(object sender, EventArgs e){ }
        //при нажатии ЛКМ (еще не отпустили)
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {//запоминаем, координаты, которые были при нажатии

            down = true;
            drawEnds = true;
            lines.lastPoint = e.Location;
            lines.tmpLine.p1 = lines.lastPoint;
            lines.points.AddLast(lines.tmpLine);

            

            
        }
        ///при включении программы
        private void Form1_Load(object sender, EventArgs e)
        {//тут нужно создавать экземпляры классов (остальное правельно в их конструктарах)  
            
        }
        //при нажатии ЛКМ ( отпустили)
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {//нужно ли их каждый раз создовать заново?

            down = false;
            drawEnds = false;
            
            lines.tmpLine.p2 = e.Location;

            //lines.points.RemoveLast();
            lines.points.AddLast(lines.tmpLine);


        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            
            
            Pen l1 = new Pen(Color.Blue);//цвет линии
            Pen l2 = new Pen(Color.White);//фон

            if (drawEnds)
            {
                g.Clear(Color.White);
                //pictureBox1.Update();
                ;
                foreach (SLine point in lines.points)
                {
                    g.DrawLine(l2, point.p1, point.p2);
                    g.DrawLine(l1, point.p1, point.p2);
                } 


                lines.pointDebug();
                Console.WriteLine("--------------------");
                 g.DrawLine(l2, lines.tmpLine.p1, lines.tmpLine.p2);
                 g.DrawLine(l1, lines.tmpLine.p1, e.Location);

               // 

                //lines.tmpLine.p2 = e.Location;

   
            }
            else
            {
                lines.tmpLine.p2 = e.Location;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            down = true;
           // pictureBox1.Invalidate();
        }
    }

   class figure
   {

       //Тип данных "линия" для начала

       public Point lastPoint;
       public SLine tmpLine;
       public LinkedList<SLine> points = new LinkedList<SLine>();
       //public static ArrayList points = new ArrayList();
       //public static List<int> test1 = new List<int>();
       

       //void rem(){points.RemoveLast();
       public figure()
       {
       }
       public void pointDebug()
       {
           foreach (SLine point in points)
           {
               Console.WriteLine(point.p1.ToString() + " " + point.p2.ToString());
           }
       }
       public void add(SLine line)
       {
           points.AddLast(line);
       }
       public void removeLast()
       {
           points.RemoveLast();
       }
       public void removeFirt() { points.RemoveFirst(); }
       public void addFirst(SLine line) { points.AddFirst(line); }
       public void ad(int n, SLine line) { int a; }// points.AddAfter(n, line); }
       //public System.Collections.Generic.IEnumerator<figure.SLine> pointsIter()
       //{
       //    return points.GetEnumerator();
       //}

   }

   class polygon : figure
   {

   }




}
