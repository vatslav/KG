﻿namespace kg_polygon
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 103);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(655, 262);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 377);
            this.Controls.Add(this.bDel);
            this.Controls.Add(this.bEdit);
            this.Controls.Add(this.bDrow);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button bDrow;
        private System.Windows.Forms.Button bEdit;
        private System.Windows.Forms.Button bDel;
    }
}
