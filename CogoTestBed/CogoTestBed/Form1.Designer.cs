namespace CogoTestBed
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonReset = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonLoop = new System.Windows.Forms.Button();
            this.labelY = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonSubdivide = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonReset);
            this.panel1.Location = new System.Drawing.Point(420, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 603);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(772, 561);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 0;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonInsert);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(32, 209);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 169);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Manual insertion";
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(136, 117);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(75, 23);
            this.buttonInsert.TabIndex = 4;
            this.buttonInsert.Text = "Insert";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(136, 63);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown2.TabIndex = 3;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(136, 33);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y coordinate:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "X coordinate:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonLoop);
            this.groupBox2.Controls.Add(this.labelY);
            this.groupBox2.Controls.Add(this.labelX);
            this.groupBox2.Controls.Add(this.buttonStop);
            this.groupBox2.Controls.Add(this.buttonDraw);
            this.groupBox2.Location = new System.Drawing.Point(32, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(367, 152);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Graphical drawing";
            // 
            // buttonLoop
            // 
            this.buttonLoop.Location = new System.Drawing.Point(170, 88);
            this.buttonLoop.Name = "buttonLoop";
            this.buttonLoop.Size = new System.Drawing.Size(75, 23);
            this.buttonLoop.TabIndex = 4;
            this.buttonLoop.Text = "Close loop";
            this.buttonLoop.UseVisualStyleBackColor = true;
            this.buttonLoop.Click += new System.EventHandler(this.buttonLoop_Click);
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(170, 59);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(17, 15);
            this.labelY.TabIndex = 3;
            this.labelY.Text = "Y:";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(170, 34);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(17, 15);
            this.labelX.TabIndex = 2;
            this.labelX.Text = "X:";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(28, 85);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(113, 23);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Finish drawing";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonDraw
            // 
            this.buttonDraw.Location = new System.Drawing.Point(28, 34);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(113, 23);
            this.buttonDraw.TabIndex = 0;
            this.buttonDraw.Text = "Start drawing";
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.richTextBox1);
            this.groupBox3.Controls.Add(this.buttonSubdivide);
            this.groupBox3.Location = new System.Drawing.Point(32, 396);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(367, 236);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Subdivision";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(16, 83);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(333, 135);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "Output messages:";
            // 
            // buttonSubdivide
            // 
            this.buttonSubdivide.Location = new System.Drawing.Point(104, 47);
            this.buttonSubdivide.Name = "buttonSubdivide";
            this.buttonSubdivide.Size = new System.Drawing.Size(152, 23);
            this.buttonSubdivide.TabIndex = 0;
            this.buttonSubdivide.Text = "Begin subdivision";
            this.buttonSubdivide.UseVisualStyleBackColor = true;
            this.buttonSubdivide.Click += new System.EventHandler(this.buttonSubdivide_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1320, 658);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "CogoTestBed";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button buttonReset;
        private GroupBox groupBox1;
        private Button buttonInsert;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private Button buttonStop;
        private Button buttonDraw;
        private GroupBox groupBox3;
        private RichTextBox richTextBox1;
        private Button buttonSubdivide;
        private Label labelY;
        private Label labelX;
        private Button buttonLoop;
    }
}