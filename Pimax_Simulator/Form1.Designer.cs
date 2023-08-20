namespace Pimax_Simulator
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
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.textBoxms = new System.Windows.Forms.TextBox();
            this.textBoxmA = new System.Windows.Forms.TextBox();
            this.textBoxKv = new System.Windows.Forms.TextBox();
            this.buttonFF = new System.Windows.Forms.Button();
            this.buttonFG = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxmAs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.textBoxER = new System.Windows.Forms.TextBox();
            this.textBoxVCC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonLuzCol = new System.Windows.Forms.Button();
            this.buttonPW = new System.Windows.Forms.Button();
            this.buttonHRST = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // textBoxms
            // 
            this.textBoxms.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxms.Location = new System.Drawing.Point(130, 84);
            this.textBoxms.Name = "textBoxms";
            this.textBoxms.Size = new System.Drawing.Size(60, 35);
            this.textBoxms.TabIndex = 14;
            this.textBoxms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxmA
            // 
            this.textBoxmA.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxmA.Location = new System.Drawing.Point(68, 84);
            this.textBoxmA.Name = "textBoxmA";
            this.textBoxmA.Size = new System.Drawing.Size(52, 35);
            this.textBoxmA.TabIndex = 13;
            this.textBoxmA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxKv
            // 
            this.textBoxKv.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxKv.Location = new System.Drawing.Point(6, 84);
            this.textBoxKv.Name = "textBoxKv";
            this.textBoxKv.Size = new System.Drawing.Size(52, 35);
            this.textBoxKv.TabIndex = 12;
            this.textBoxKv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonFF
            // 
            this.buttonFF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFF.Location = new System.Drawing.Point(323, 160);
            this.buttonFF.Name = "buttonFF";
            this.buttonFF.Size = new System.Drawing.Size(36, 36);
            this.buttonFF.TabIndex = 92;
            this.buttonFF.Text = "FF";
            this.buttonFF.UseVisualStyleBackColor = true;
            // 
            // buttonFG
            // 
            this.buttonFG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFG.Location = new System.Drawing.Point(315, 152);
            this.buttonFG.Name = "buttonFG";
            this.buttonFG.Size = new System.Drawing.Size(52, 52);
            this.buttonFG.TabIndex = 91;
            this.buttonFG.Text = "FG";
            this.buttonFG.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(18, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 20);
            this.label10.TabIndex = 93;
            this.label10.Text = "Kv";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.TabIndex = 94;
            this.label1.Text = "mA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(144, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 20);
            this.label2.TabIndex = 95;
            this.label2.Text = "ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(208, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 20);
            this.label3.TabIndex = 97;
            this.label3.Text = "mAs";
            // 
            // textBoxmAs
            // 
            this.textBoxmAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxmAs.Location = new System.Drawing.Point(200, 84);
            this.textBoxmAs.Name = "textBoxmAs";
            this.textBoxmAs.Size = new System.Drawing.Size(60, 35);
            this.textBoxmAs.TabIndex = 96;
            this.textBoxmAs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(260, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 98;
            this.label4.Text = "Foco";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 20);
            this.button1.TabIndex = 99;
            this.button1.Text = "Driver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(69, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 20);
            this.button2.TabIndex = 100;
            this.button2.Text = "Filament";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(135, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 20);
            this.button3.TabIndex = 101;
            this.button3.Text = "Stator";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // serialPort2
            // 
            this.serialPort2.BaudRate = 19200;
            this.serialPort2.PortName = "COM2";
            this.serialPort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort2_DataReceived);
            // 
            // textBoxER
            // 
            this.textBoxER.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxER.Location = new System.Drawing.Point(7, 125);
            this.textBoxER.Name = "textBoxER";
            this.textBoxER.Size = new System.Drawing.Size(254, 26);
            this.textBoxER.TabIndex = 102;
            // 
            // textBoxVCC
            // 
            this.textBoxVCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxVCC.Location = new System.Drawing.Point(184, 153);
            this.textBoxVCC.Name = "textBoxVCC";
            this.textBoxVCC.Size = new System.Drawing.Size(65, 35);
            this.textBoxVCC.TabIndex = 103;
            this.textBoxVCC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(133, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 20);
            this.label5.TabIndex = 104;
            this.label5.Text = "VCC";
            // 
            // buttonLuzCol
            // 
            this.buttonLuzCol.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLuzCol.Location = new System.Drawing.Point(200, 5);
            this.buttonLuzCol.Name = "buttonLuzCol";
            this.buttonLuzCol.Size = new System.Drawing.Size(60, 45);
            this.buttonLuzCol.TabIndex = 105;
            this.buttonLuzCol.Text = "Luz Colimador";
            this.buttonLuzCol.UseVisualStyleBackColor = true;
            this.buttonLuzCol.Click += new System.EventHandler(this.buttonLuzCol_Click);
            // 
            // buttonPW
            // 
            this.buttonPW.Location = new System.Drawing.Point(3, 4);
            this.buttonPW.Name = "buttonPW";
            this.buttonPW.Size = new System.Drawing.Size(60, 46);
            this.buttonPW.TabIndex = 106;
            this.buttonPW.Text = "SI / NO";
            this.buttonPW.UseVisualStyleBackColor = true;
            this.buttonPW.Click += new System.EventHandler(this.buttonPW_Click);
            // 
            // buttonHRST
            // 
            this.buttonHRST.Location = new System.Drawing.Point(69, 5);
            this.buttonHRST.Name = "buttonHRST";
            this.buttonHRST.Size = new System.Drawing.Size(60, 20);
            this.buttonHRST.TabIndex = 107;
            this.buttonHRST.Text = "Reset";
            this.buttonHRST.UseVisualStyleBackColor = true;
            this.buttonHRST.Click += new System.EventHandler(this.buttonHRST_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 157);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(118, 26);
            this.textBox1.TabIndex = 108;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(7, 189);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(60, 46);
            this.buttonExit.TabIndex = 109;
            this.buttonExit.Text = "App Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 110;
            this.label6.Text = "App Version 1.0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(375, 242);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonHRST);
            this.Controls.Add(this.buttonPW);
            this.Controls.Add(this.buttonLuzCol);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxVCC);
            this.Controls.Add(this.textBoxER);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxmAs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.buttonFF);
            this.Controls.Add(this.buttonFG);
            this.Controls.Add(this.textBoxms);
            this.Controls.Add(this.textBoxmA);
            this.Controls.Add(this.textBoxKv);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Pimax IF";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox textBoxms;
        private System.Windows.Forms.TextBox textBoxmA;
        private System.Windows.Forms.TextBox textBoxKv;
        private System.Windows.Forms.Button buttonFF;
        private System.Windows.Forms.Button buttonFG;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxmAs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.TextBox textBoxER;
        private System.Windows.Forms.TextBox textBoxVCC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonLuzCol;
        private System.Windows.Forms.Button buttonPW;
        private System.Windows.Forms.Button buttonHRST;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label label6;
    }
}

