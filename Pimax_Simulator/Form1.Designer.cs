﻿namespace Pimax_Simulator
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.textBoxSW = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonRX = new System.Windows.Forms.Button();
            this.buttonPrep = new System.Windows.Forms.Button();
            this.buttonKY = new System.Windows.Forms.Button();
            this.buttonTV = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // textBoxms
            // 
            this.textBoxms.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxms.Location = new System.Drawing.Point(134, 194);
            this.textBoxms.Name = "textBoxms";
            this.textBoxms.Size = new System.Drawing.Size(60, 35);
            this.textBoxms.TabIndex = 14;
            this.textBoxms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxmA
            // 
            this.textBoxmA.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxmA.Location = new System.Drawing.Point(72, 194);
            this.textBoxmA.Name = "textBoxmA";
            this.textBoxmA.Size = new System.Drawing.Size(52, 35);
            this.textBoxmA.TabIndex = 13;
            this.textBoxmA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxKv
            // 
            this.textBoxKv.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxKv.Location = new System.Drawing.Point(10, 194);
            this.textBoxKv.Name = "textBoxKv";
            this.textBoxKv.Size = new System.Drawing.Size(52, 35);
            this.textBoxKv.TabIndex = 12;
            this.textBoxKv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonFF
            // 
            this.buttonFF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFF.Location = new System.Drawing.Point(276, 193);
            this.buttonFF.Name = "buttonFF";
            this.buttonFF.Size = new System.Drawing.Size(36, 36);
            this.buttonFF.TabIndex = 92;
            this.buttonFF.Text = "FF";
            this.buttonFF.UseVisualStyleBackColor = true;
            // 
            // buttonFG
            // 
            this.buttonFG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFG.Location = new System.Drawing.Point(268, 185);
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
            this.label10.Location = new System.Drawing.Point(22, 166);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 20);
            this.label10.TabIndex = 93;
            this.label10.Text = "Kv";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.TabIndex = 94;
            this.label1.Text = "mA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(148, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 20);
            this.label2.TabIndex = 95;
            this.label2.Text = "ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(212, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 20);
            this.label3.TabIndex = 97;
            this.label3.Text = "mAs";
            // 
            // textBoxmAs
            // 
            this.textBoxmAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxmAs.Location = new System.Drawing.Point(204, 194);
            this.textBoxmAs.Name = "textBoxmAs";
            this.textBoxmAs.Size = new System.Drawing.Size(60, 35);
            this.textBoxmAs.TabIndex = 96;
            this.textBoxmAs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(273, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 16);
            this.label4.TabIndex = 98;
            this.label4.Text = "Foco";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(44, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 20);
            this.button1.TabIndex = 99;
            this.button1.Text = "Driver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(110, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 20);
            this.button2.TabIndex = 100;
            this.button2.Text = "Filament";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(176, 56);
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
            this.textBoxER.Location = new System.Drawing.Point(6, 82);
            this.textBoxER.Name = "textBoxER";
            this.textBoxER.Size = new System.Drawing.Size(258, 26);
            this.textBoxER.TabIndex = 102;
            // 
            // textBoxVCC
            // 
            this.textBoxVCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxVCC.Location = new System.Drawing.Point(158, 240);
            this.textBoxVCC.Name = "textBoxVCC";
            this.textBoxVCC.Size = new System.Drawing.Size(65, 35);
            this.textBoxVCC.TabIndex = 103;
            this.textBoxVCC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(131, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 104;
            this.label5.Text = "VCC";
            // 
            // buttonLuzCol
            // 
            this.buttonLuzCol.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLuzCol.Location = new System.Drawing.Point(60, 5);
            this.buttonLuzCol.Name = "buttonLuzCol";
            this.buttonLuzCol.Size = new System.Drawing.Size(44, 45);
            this.buttonLuzCol.TabIndex = 105;
            this.buttonLuzCol.Text = "Colim";
            this.buttonLuzCol.UseVisualStyleBackColor = true;
            this.buttonLuzCol.Click += new System.EventHandler(this.buttonLuzCol_Click);
            // 
            // buttonPW
            // 
            this.buttonPW.Location = new System.Drawing.Point(6, 5);
            this.buttonPW.Name = "buttonPW";
            this.buttonPW.Size = new System.Drawing.Size(44, 45);
            this.buttonPW.TabIndex = 106;
            this.buttonPW.Text = "Enc.";
            this.buttonPW.UseVisualStyleBackColor = true;
            this.buttonPW.Click += new System.EventHandler(this.buttonPW_Click);
            // 
            // buttonHRST
            // 
            this.buttonHRST.Location = new System.Drawing.Point(266, 56);
            this.buttonHRST.Name = "buttonHRST";
            this.buttonHRST.Size = new System.Drawing.Size(60, 52);
            this.buttonHRST.TabIndex = 107;
            this.buttonHRST.Text = "Reset";
            this.buttonHRST.UseVisualStyleBackColor = true;
            this.buttonHRST.Click += new System.EventHandler(this.buttonHRST_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(10, 244);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(118, 26);
            this.textBox1.TabIndex = 108;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(11, 276);
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
            this.label6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(124, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 20);
            this.label6.TabIndex = 110;
            this.label6.Text = "Interface VXvue";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // textBoxSW
            // 
            this.textBoxSW.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSW.Location = new System.Drawing.Point(203, 133);
            this.textBoxSW.Name = "textBoxSW";
            this.textBoxSW.Size = new System.Drawing.Size(103, 26);
            this.textBoxSW.TabIndex = 111;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(213, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 20);
            this.label7.TabIndex = 112;
            this.label7.Text = "DUE SW:";
            // 
            // buttonRX
            // 
            this.buttonRX.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRX.Location = new System.Drawing.Point(206, 5);
            this.buttonRX.Name = "buttonRX";
            this.buttonRX.Size = new System.Drawing.Size(44, 45);
            this.buttonRX.TabIndex = 114;
            this.buttonRX.UseVisualStyleBackColor = true;
            this.buttonRX.Click += new System.EventHandler(this.buttonRX_Click);
            // 
            // buttonPrep
            // 
            this.buttonPrep.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrep.Location = new System.Drawing.Point(141, 5);
            this.buttonPrep.Name = "buttonPrep";
            this.buttonPrep.Size = new System.Drawing.Size(44, 45);
            this.buttonPrep.TabIndex = 113;
            this.buttonPrep.UseVisualStyleBackColor = true;
            this.buttonPrep.Click += new System.EventHandler(this.buttonPrep_Click);
            // 
            // buttonKY
            // 
            this.buttonKY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonKY.Location = new System.Drawing.Point(285, 5);
            this.buttonKY.Name = "buttonKY";
            this.buttonKY.Size = new System.Drawing.Size(44, 45);
            this.buttonKY.TabIndex = 115;
            this.buttonKY.UseVisualStyleBackColor = true;
            this.buttonKY.Click += new System.EventHandler(this.buttonKY_Click);
            // 
            // buttonTV
            // 
            this.buttonTV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTV.Location = new System.Drawing.Point(6, 114);
            this.buttonTV.Name = "buttonTV";
            this.buttonTV.Size = new System.Drawing.Size(74, 45);
            this.buttonTV.TabIndex = 116;
            this.buttonTV.Text = "Team Viewer";
            this.buttonTV.UseVisualStyleBackColor = true;
            this.buttonTV.Click += new System.EventHandler(this.buttonTV_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(85, 114);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 45);
            this.button4.TabIndex = 117;
            this.button4.Text = "Servicio Tecnico";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(334, 331);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.buttonTV);
            this.Controls.Add(this.buttonKY);
            this.Controls.Add(this.buttonRX);
            this.Controls.Add(this.buttonPrep);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxSW);
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
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Pimax IF Version 3.1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.TextBox textBoxSW;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonPrep;
        private System.Windows.Forms.Button buttonRX;
        private System.Windows.Forms.Button buttonKY;
        private System.Windows.Forms.Button buttonTV;
        private System.Windows.Forms.Button button4;
    }
}

