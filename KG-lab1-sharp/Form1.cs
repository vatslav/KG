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
            //lines.las
            lines.lastLine.a = lines.lastPoint;
            //lines.lastLine.a = e.Location;

           // lines.lastLine.a;
            //lines.points.AddLast(lines.lastLine);

            

            
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
            
            lines.lastLine.b = e.Location;

            //lines.points.RemoveLast();
            lines.points.AddLast(lines.lastLine);


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
                    g.DrawLine(l2, point.a, point.b);
                    g.DrawLine(l1, point.a, point.b);
                } 


                lines.pointDebug();
                Console.WriteLine("--------------------");
                 g.DrawLine(l2, lines.lastLine.a, lines.lastLine.b);
                 g.DrawLine(l1, lines.lastLine.a, e.Location);

               // 

                //lines.tmpLine.b = e.Location;

   
            }
            else
            {
                lines.lastLine.b = e.Location;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            down = true;
           // pictureBox1.Invalidate();
        }
    }
///----------------------------------------------------------------------------==============================
   class figure
   {

       //Тип данных "линия" для начала

       protected static Point LastPoint;
       public Point lastPoint { get { return LastPoint; } set { LastPoint = value; } }


       protected static SLine LastLine = new SLine();
       public SLine lastLine { get { return LastLine; } set { LastLine = value; } }

       //общий для всех классов список объекто
       //protected static ArrayList points = new ArrayList();
       protected static ArrayList Points = new ArrayList(); //оригинальное хранилище
       public arrayListProxy points = new arrayListProxy(ref Points); //создаем экземпляр прокси-класса
       //public static ArrayList points = new ArrayList();
       //public static List<int> test1 = new List<int>();




       public figure()
       {
          // points.
       }
       public void pointDebug()
       {
           foreach (SLine point in points)
           {
               Console.WriteLine(point.a.ToString() + " " + point.b.ToString());
           }
       }
       //public void add(SLine line)
       //{
       //    points.AddLast(line);
       //}
       //public void removeLast()
       //{
       //    points.RemoveLast();
       //}
       //public void removeFirt() { points.RemoveFirst(); }
       //public void addFirst(SLine line) { points.AddFirst(line); }
       //public void ad(int n, SLine line) { int a; }// points.AddAfter(n, line); }

       //public System.Collections.Generic.IEnumerator<figure.SLine> pointsIter()
       //{
       //    return points.GetEnumerator();
       //}

   }
   class SLine
   {
       Point A, B;
       public Point a { get { return A; } set { A = value; } }
       public Point b { get { return B; } set { B = value; } }
   }
    //struct SLine
    //{
    //   public Point a, b;
    //   SLine(Point a) { this.a = a; }
    //    SLine(Point a, Point b) { this.a = a; this.b = b; }
    //    }


   class arrayListProxy
   {
       public ArrayList arr = new ArrayList();
       public arrayListProxy(ref ArrayList arr) { this.arr = arr; }
       public void Add<T>(T obj) { arr.Add(obj); }
       public dynamic Get(int index) { return arr[index]; }
       public int Count() { return arr.Count; }
       public void Remove(object obj) { arr.Remove(obj); }
       public IEnumerator GetEnumerator() { return arr.GetEnumerator(); } //нужно сделать приведение типов


       internal void AddLast<T>(T sLine) {Add(sLine); }
       
           //throw new NotImplementedException();
       
   }

   class polygon : figure
   {

   }




}
