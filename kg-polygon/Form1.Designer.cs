namespace kg_polygon
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bDrow = new System.Windows.Forms.Button();
            this.bEdit = new System.Windows.Forms.Button();
            this.bDel = new System.Windows.Forms.Button();
            this.bSafe = new System.Windows.Forms.Button();
            this.bLoad = new System.Windows.Forms.Button();
            this.rPolygon = new System.Windows.Forms.RadioButton();
            this.rLine = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 103);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(925, 313);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // bDrow
            // 
            this.bDrow.Location = new System.Drawing.Point(13, 24);
            this.bDrow.Name = "bDrow";
            this.bDrow.Size = new System.Drawing.Size(116, 73);
            this.bDrow.TabIndex = 1;
            this.bDrow.Text = "Drow";
            this.bDrow.UseVisualStyleBackColor = true;
            this.bDrow.Click += new System.EventHandler(this.bDrow_Click);
            // 
            // bEdit
            // 
            this.bEdit.Location = new System.Drawing.Point(135, 24);
            this.bEdit.Name = "bEdit";
            this.bEdit.Size = new System.Drawing.Size(116, 73);
            this.bEdit.TabIndex = 2;
            this.bEdit.Text = "Edit";
            this.bEdit.UseVisualStyleBackColor = true;
            this.bEdit.Click += new System.EventHandler(this.bEdit_Click);
            // 
            // bDel
            // 
            this.bDel.Location = new System.Drawing.Point(258, 24);
            this.bDel.Name = "bDel";
            this.bDel.Size = new System.Drawing.Size(111, 73);
            this.bDel.TabIndex = 3;
            this.bDel.Text = "Delet";
            this.bDel.UseVisualStyleBackColor = true;
            this.bDel.Click += new System.EventHandler(this.bDel_Click);
            // 
            // bSafe
            // 
            this.bSafe.Location = new System.Drawing.Point(376, 24);
            this.bSafe.Name = "bSafe";
            this.bSafe.Size = new System.Drawing.Size(117, 73);
            this.bSafe.TabIndex = 4;
            this.bSafe.Text = "Сохранить";
            this.bSafe.UseVisualStyleBackColor = true;
            this.bSafe.Click += new System.EventHandler(this.bSafe_Click);
            // 
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(499, 24);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(126, 73);
            this.bLoad.TabIndex = 5;
            this.bLoad.Text = "Загрузить";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // rPolygon
            // 
            this.rPolygon.AutoSize = true;
            this.rPolygon.Location = new System.Drawing.Point(632, 79);
            this.rPolygon.Name = "rPolygon";
            this.rPolygon.Size = new System.Drawing.Size(97, 17);
            this.rPolygon.TabIndex = 6;
            this.rPolygon.TabStop = true;
            this.rPolygon.Text = "Многоуголник";
            this.rPolygon.UseVisualStyleBackColor = true;
            this.rPolygon.CheckedChanged += new System.EventHandler(this.rPolygon_CheckedChanged);
            // 
            // rLine
            // 
            this.rLine.AllowDrop = true;
            this.rLine.AutoSize = true;
            this.rLine.Location = new System.Drawing.Point(632, 56);
            this.rLine.Name = "rLine";
            this.rLine.Size = new System.Drawing.Size(57, 17);
            this.rLine.TabIndex = 7;
            this.rLine.TabStop = true;
            this.rLine.Text = "Линия";
            this.rLine.UseVisualStyleBackColor = true;
            this.rLine.CheckedChanged += new System.EventHandler(this.rLine_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 428);
            this.Controls.Add(this.rLine);
            this.Controls.Add(this.rPolygon);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.bSafe);
            this.Controls.Add(this.bDel);
            this.Controls.Add(this.bEdit);
            this.Controls.Add(this.bDrow);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button bDrow;
        private System.Windows.Forms.Button bEdit;
        private System.Windows.Forms.Button bDel;
        private System.Windows.Forms.Button bSafe;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.RadioButton rPolygon;
        private System.Windows.Forms.RadioButton rLine;
    }
}

