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
            panel1 = new Panel();
            buttonReset = new Button();
            groupBox1 = new GroupBox();
            buttonInsert = new Button();
            numericUpDown2 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            buttonLoop = new Button();
            labelY = new Label();
            labelX = new Label();
            buttonStop = new Button();
            buttonDraw = new Button();
            groupBox3 = new GroupBox();
            comboBox2 = new ComboBox();
            label4 = new Label();
            comboBox1 = new ComboBox();
            label3 = new Label();
            richTextBox1 = new RichTextBox();
            buttonSubdivide = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            comboBox3 = new ComboBox();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(buttonReset);
            panel1.Location = new Point(416, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(877, 591);
            panel1.TabIndex = 0;
            panel1.Click += panel1_Click;
            panel1.Paint += panel1_Paint;
            panel1.MouseMove += panel1_MouseMove;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(797, 563);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(75, 23);
            buttonReset.TabIndex = 0;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonInsert);
            groupBox1.Controls.Add(numericUpDown2);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(32, 209);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(367, 169);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Manual insertion";
            // 
            // buttonInsert
            // 
            buttonInsert.Location = new Point(136, 117);
            buttonInsert.Name = "buttonInsert";
            buttonInsert.Size = new Size(75, 23);
            buttonInsert.TabIndex = 4;
            buttonInsert.Text = "Insert";
            buttonInsert.UseVisualStyleBackColor = true;
            buttonInsert.Click += buttonInsert_Click;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(136, 63);
            numericUpDown2.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(120, 23);
            numericUpDown2.TabIndex = 3;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(136, 33);
            numericUpDown1.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 65);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 1;
            label2.Text = "Y coordinate:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 35);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 0;
            label1.Text = "X coordinate:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(buttonLoop);
            groupBox2.Controls.Add(labelY);
            groupBox2.Controls.Add(labelX);
            groupBox2.Controls.Add(buttonStop);
            groupBox2.Controls.Add(buttonDraw);
            groupBox2.Location = new Point(32, 32);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(367, 152);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Graphical drawing";
            // 
            // buttonLoop
            // 
            buttonLoop.Location = new Point(181, 85);
            buttonLoop.Name = "buttonLoop";
            buttonLoop.Size = new Size(75, 23);
            buttonLoop.TabIndex = 4;
            buttonLoop.Text = "Close loop";
            buttonLoop.UseVisualStyleBackColor = true;
            buttonLoop.Click += buttonLoop_Click;
            // 
            // labelY
            // 
            labelY.AutoSize = true;
            labelY.Location = new Point(170, 59);
            labelY.Name = "labelY";
            labelY.Size = new Size(17, 15);
            labelY.TabIndex = 3;
            labelY.Text = "Y:";
            // 
            // labelX
            // 
            labelX.AutoSize = true;
            labelX.Location = new Point(170, 34);
            labelX.Name = "labelX";
            labelX.Size = new Size(17, 15);
            labelX.TabIndex = 2;
            labelX.Text = "X:";
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(28, 85);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(113, 23);
            buttonStop.TabIndex = 1;
            buttonStop.Text = "Finish drawing";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // buttonDraw
            // 
            buttonDraw.Location = new Point(28, 34);
            buttonDraw.Name = "buttonDraw";
            buttonDraw.Size = new Size(113, 23);
            buttonDraw.TabIndex = 0;
            buttonDraw.Text = "Start drawing";
            buttonDraw.UseVisualStyleBackColor = true;
            buttonDraw.Click += buttonDraw_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(comboBox2);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(comboBox1);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(richTextBox1);
            groupBox3.Controls.Add(buttonSubdivide);
            groupBox3.Location = new Point(32, 396);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(367, 236);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Subdivision";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(130, 34);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(219, 23);
            comboBox2.TabIndex = 5;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 37);
            label4.Name = "label4";
            label4.Size = new Size(102, 15);
            label4.TabIndex = 4;
            label4.Text = "Element to divide:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Rectangular", "Axis-aligned", "Convex", "Irregular" });
            comboBox1.Location = new Point(130, 66);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(219, 23);
            comboBox1.TabIndex = 3;
            comboBox1.Text = "Rectangular";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 69);
            label3.Name = "label3";
            label3.Size = new Size(108, 15);
            label3.TabIndex = 2;
            label3.Text = "Output shape type:";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(16, 127);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(333, 91);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "Output messages:";
            // 
            // buttonSubdivide
            // 
            buttonSubdivide.Location = new Point(104, 98);
            buttonSubdivide.Name = "buttonSubdivide";
            buttonSubdivide.Size = new Size(152, 23);
            buttonSubdivide.TabIndex = 0;
            buttonSubdivide.Text = "Begin subdivision";
            buttonSubdivide.UseVisualStyleBackColor = true;
            buttonSubdivide.Click += buttonSubdivide_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 636);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1320, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.ForeColor = Color.Red;
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "Import data", "Export data" });
            comboBox3.Location = new Point(1164, 12);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(129, 23);
            comboBox3.TabIndex = 1;
            comboBox3.Text = "Import/export data";
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1320, 658);
            Controls.Add(comboBox3);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "CogoTestBed";
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private ComboBox comboBox1;
        private Label label3;
        private ComboBox comboBox2;
        private Label label4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ComboBox comboBox3;
    }
}