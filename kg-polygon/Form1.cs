using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
//using System.Timers;
using shareData;


namespace kg_polygon
{
    //состояния: в первую, во вторую, в центр)
    enum captures { TAKE_PT1, TAKE_PT2, TAKE_CENTR, TAKE_NONE, TAKE_TURN };
    //режим рисования: рисования, перемешение, удаление
    enum modes { MODE_DROW, MODE_MOVE, MODE_DELETE };
    
    enum penType { line, poligon };

    public partial class Form1 : Form
    {
        //состояние программы
        editor state = new editor();
        //console konsole;

        
        //Timer t = new Timer();
        //t.Tick+= new System.EventHandler(this, t_Tick);
        //void t_Tick(object sender, EventArgs e) { }
        //void tickF() { state.DrawingFigure(null, null); }
        public Form1()
        {
             
            InitializeComponent();
            state.initial(pictureBox1);
            state.initial(consoleWindow, consoleEntry);
            //canvas=pictureBox1.CreateGraphics(); //присваиваем канвасу уазатель, чем он
            state.curModes = (int)modes.MODE_DROW; //режим работы по умолчанию
            rLine.Checked = true;
            
            //konsole = new(RichTextBox consoleWindow, TextBox consoleEntry);
            

           
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void Form1_MouseDown(object sender, MouseEventArgs e){}
        private void Form1_MouseUp(object sender, MouseEventArgs e){}
        private void Form1_MouseMove(object sender, MouseEventArgs e){}

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
            state.drawingDown(e);

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            state.drawingUp(e);
            state.pointsDebug();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            state.drawingSciene(pictureBox1, e);

        }

        private void bEdit_Click(object sender, EventArgs e){ state.curModes = (int)modes.MODE_MOVE;}

        private void bDrow_Click(object sender, EventArgs e)
        {
            state.curModes = (int)modes.MODE_DROW;
        }

        private void bDel_Click(object sender, EventArgs e)
        {
            state.curModes = (int)modes.MODE_DELETE;
            state.drawingScieneOnly();
        }

        private void bSafe_Click(object sender, EventArgs e)
        {
            SaveFileDialog askSave = new SaveFileDialog();
            askSave.Filter = "VectorFugireFiles|*.vff";
            askSave.Title = "Выберети файл - векторное хранилища для сохранения данных прогруммы";
            askSave.ShowDialog();
            //File = File.OpenText(filename);
            //StreamWriter sw = File.CreateText(filename)
            
            if (askSave.FileName != "")
             state.safeStorage(askSave.FileName);

            
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            int tempMode = state.curModes;
            state.curModes = (int) modes.MODE_DROW;
            try
            {
                OpenFileDialog askLoad = new OpenFileDialog();
                askLoad.Filter = "VectorFugireFiles|*.vff";
                askLoad.Title = "Выберети файл - векторное хранилища для загрузки в прогрумму";
                askLoad.ShowDialog();
                if (askLoad.FileName != "")
                    state.loadStorage(askLoad.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            state.curModes = tempMode;


            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Width = Width - 5;
            pictureBox1.Height = Height - 5;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void rLine_CheckedChanged(object sender, EventArgs e)
        {
            state.pen = (int)penType.line;
        }

        private void rPolygon_CheckedChanged(object sender, EventArgs e)
        {
            state.pen = (int)penType.poligon;
        }

        private void bDel_EnabledChanged(object sender, EventArgs e)
        {
            state.drawingScieneOnly();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            state.changeZoom(me);
        }

        private void button1_Click(object sender, EventArgs e)
        {
          state.handScale(consoleEntry.Text);
        }

        private void consoleWindow_TextChanged(object sender, EventArgs e)
        {
            consoleWindow.SelectionStart = consoleWindow.Text.Length;
            consoleWindow.ScrollToCaret();
        }

        private void consoleEntry_TextChanged(object sender, EventArgs e)
        {

        }

        private void consoleEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                state.konsole.execComands();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }





}