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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.rPolygon = new System.Windows.Forms.RadioButton();
            this.rLine = new System.Windows.Forms.RadioButton();
            this.consoleEntry = new System.Windows.Forms.TextBox();
            this.consoleWindow = new System.Windows.Forms.RichTextBox();
            this.bLoad = new System.Windows.Forms.Button();
            this.bSafe = new System.Windows.Forms.Button();
            this.bDel = new System.Windows.Forms.Button();
            this.bEdit = new System.Windows.Forms.Button();
            this.bDrow = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rPolygon
            // 
            this.rPolygon.AutoSize = true;
            this.rPolygon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rPolygon.Location = new System.Drawing.Point(12, 64);
            this.rPolygon.Name = "rPolygon";
            this.rPolygon.Size = new System.Drawing.Size(133, 24);
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
            this.rLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rLine.Location = new System.Drawing.Point(12, 34);
            this.rLine.Name = "rLine";
            this.rLine.Size = new System.Drawing.Size(75, 24);
            this.rLine.TabIndex = 7;
            this.rLine.TabStop = true;
            this.rLine.Text = "Линия";
            this.rLine.UseVisualStyleBackColor = true;
            this.rLine.CheckedChanged += new System.EventHandler(this.rLine_CheckedChanged);
            // 
            // consoleEntry
            // 
            this.consoleEntry.BackColor = System.Drawing.SystemColors.MenuText;
            this.consoleEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.consoleEntry.ForeColor = System.Drawing.SystemColors.Window;
            this.consoleEntry.Location = new System.Drawing.Point(6, 467);
            this.consoleEntry.Name = "consoleEntry";
            this.consoleEntry.Size = new System.Drawing.Size(363, 26);
            this.consoleEntry.TabIndex = 8;
            this.consoleEntry.WordWrap = false;
            this.consoleEntry.TextChanged += new System.EventHandler(this.consoleEntry_TextChanged);
            this.consoleEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.consoleEntry_KeyDown);
            // 
            // consoleWindow
            // 
            this.consoleWindow.AutoWordSelection = true;
            this.consoleWindow.BackColor = System.Drawing.SystemColors.MenuText;
            this.consoleWindow.Cursor = System.Windows.Forms.Cursors.Default;
            this.consoleWindow.EnableAutoDragDrop = true;
            this.consoleWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.consoleWindow.ForeColor = System.Drawing.SystemColors.Window;
            this.consoleWindow.Location = new System.Drawing.Point(6, 25);
            this.consoleWindow.Name = "consoleWindow";
            this.consoleWindow.ReadOnly = true;
            this.consoleWindow.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.consoleWindow.Size = new System.Drawing.Size(363, 436);
            this.consoleWindow.TabIndex = 9;
            this.consoleWindow.Text = "";
            this.consoleWindow.TextChanged += new System.EventHandler(this.consoleWindow_TextChanged);
            // 
            // bLoad
            // 
            this.bLoad.BackgroundImage = global::kg_polygon.Properties.Resources.human_gnome_fs_directory_accept;
            this.bLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bLoad.Location = new System.Drawing.Point(544, 25);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(130, 130);
            this.bLoad.TabIndex = 5;
            this.bLoad.Text = "Открыть";
            this.bLoad.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bSafe
            // 
            this.bSafe.BackgroundImage = global::kg_polygon.Properties.Resources.human_add_folder_to_archive;
            this.bSafe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bSafe.Location = new System.Drawing.Point(408, 26);
            this.bSafe.Name = "bSafe";
            this.bSafe.Size = new System.Drawing.Size(129, 129);
            this.bSafe.TabIndex = 4;
            this.bSafe.Text = "Сохранить";
            this.bSafe.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bSafe.UseVisualStyleBackColor = true;
            this.bSafe.Click += new System.EventHandler(this.bSafe_Click);
            // 
            // bDel
            // 
            this.bDel.BackgroundImage = global::kg_polygon.Properties.Resources.human_trashcan_full_new;
            this.bDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bDel.Location = new System.Drawing.Point(273, 25);
            this.bDel.Name = "bDel";
            this.bDel.Size = new System.Drawing.Size(129, 129);
            this.bDel.TabIndex = 3;
            this.bDel.Text = "Удалить";
            this.bDel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bDel.UseVisualStyleBackColor = true;
            this.bDel.EnabledChanged += new System.EventHandler(this.bDel_EnabledChanged);
            this.bDel.Click += new System.EventHandler(this.bDel_Click);
            // 
            // bEdit
            // 
            this.bEdit.BackgroundImage = global::kg_polygon.Properties.Resources.x_office_drawing_template;
            this.bEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bEdit.Location = new System.Drawing.Point(137, 25);
            this.bEdit.Name = "bEdit";
            this.bEdit.Size = new System.Drawing.Size(130, 130);
            this.bEdit.TabIndex = 2;
            this.bEdit.Text = "Изменить";
            this.bEdit.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bEdit.UseVisualStyleBackColor = true;
            this.bEdit.Click += new System.EventHandler(this.bEdit_Click);
            // 
            // bDrow
            // 
            this.bDrow.BackgroundImage = global::kg_polygon.Properties.Resources.old_openofficeorg_draw;
            this.bDrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bDrow.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bDrow.Location = new System.Drawing.Point(6, 25);
            this.bDrow.Name = "bDrow";
            this.bDrow.Size = new System.Drawing.Size(125, 129);
            this.bDrow.TabIndex = 1;
            this.bDrow.Text = "Добавить";
            this.bDrow.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bDrow.UseVisualStyleBackColor = true;
            this.bDrow.Click += new System.EventHandler(this.bDrow_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox1.Location = new System.Drawing.Point(7, 167);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(820, 340);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bDrow);
            this.groupBox1.Controls.Add(this.bEdit);
            this.groupBox1.Controls.Add(this.bDel);
            this.groupBox1.Controls.Add(this.bSafe);
            this.groupBox1.Controls.Add(this.bLoad);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(837, 520);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Область рисования";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.consoleWindow);
            this.groupBox2.Controls.Add(this.consoleEntry);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(855, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(375, 506);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Консоль";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rLine);
            this.groupBox3.Controls.Add(this.rPolygon);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(680, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(151, 131);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Тип фигуры";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 544);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Векторный редактор 2.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TextBox consoleEntry;
        private System.Windows.Forms.RichTextBox consoleWindow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

