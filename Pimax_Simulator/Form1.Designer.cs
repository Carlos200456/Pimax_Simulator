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
            this.buttonPREP = new System.Windows.Forms.Button();
            this.buttonRX = new System.Windows.Forms.Button();
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
            this.SuspendLayout();
            // 
            // buttonPREP
            // 
            this.buttonPREP.Location = new System.Drawing.Point(298, 8);
            this.buttonPREP.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPREP.Name = "buttonPREP";
            this.buttonPREP.Size = new System.Drawing.Size(103, 76);
            this.buttonPREP.TabIndex = 0;
            this.buttonPREP.Text = "PREP";
            this.buttonPREP.UseVisualStyleBackColor = true;
            this.buttonPREP.Click += new System.EventHandler(this.buttonPREP_Click);
            // 
            // buttonRX
            // 
            this.buttonRX.Location = new System.Drawing.Point(298, 89);
            this.buttonRX.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRX.Name = "buttonRX";
            this.buttonRX.Size = new System.Drawing.Size(103, 76);
            this.buttonRX.TabIndex = 0;
            this.buttonRX.Text = "Rayos";
            this.buttonRX.UseVisualStyleBackColor = true;
            this.buttonRX.Click += new System.EventHandler(this.buttonRX_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // textBoxms
            // 
            this.textBoxms.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxms.Location = new System.Drawing.Point(151, 36);
            this.textBoxms.Name = "textBoxms";
            this.textBoxms.Size = new System.Drawing.Size(60, 35);
            this.textBoxms.TabIndex = 14;
            this.textBoxms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxmA
            // 
            this.textBoxmA.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxmA.Location = new System.Drawing.Point(89, 36);
            this.textBoxmA.Name = "textBoxmA";
            this.textBoxmA.Size = new System.Drawing.Size(52, 35);
            this.textBoxmA.TabIndex = 13;
            this.textBoxmA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxKv
            // 
            this.textBoxKv.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxKv.Location = new System.Drawing.Point(27, 36);
            this.textBoxKv.Name = "textBoxKv";
            this.textBoxKv.Size = new System.Drawing.Size(52, 35);
            this.textBoxKv.TabIndex = 12;
            this.textBoxKv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonFF
            // 
            this.buttonFF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFF.Location = new System.Drawing.Point(97, 108);
            this.buttonFF.Name = "buttonFF";
            this.buttonFF.Size = new System.Drawing.Size(36, 36);
            this.buttonFF.TabIndex = 92;
            this.buttonFF.Text = "FF";
            this.buttonFF.UseVisualStyleBackColor = true;
            // 
            // buttonFG
            // 
            this.buttonFG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFG.Location = new System.Drawing.Point(89, 100);
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
            this.label10.Location = new System.Drawing.Point(39, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 20);
            this.label10.TabIndex = 93;
            this.label10.Text = "Kv";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.TabIndex = 94;
            this.label1.Text = "mA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(165, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 20);
            this.label2.TabIndex = 95;
            this.label2.Text = "ms";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(229, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 20);
            this.label3.TabIndex = 97;
            this.label3.Text = "mAs";
            // 
            // textBoxmAs
            // 
            this.textBoxmAs.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxmAs.Location = new System.Drawing.Point(221, 36);
            this.textBoxmAs.Name = "textBoxmAs";
            this.textBoxmAs.Size = new System.Drawing.Size(60, 35);
            this.textBoxmAs.TabIndex = 96;
            this.textBoxmAs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(91, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 98;
            this.label4.Text = "Foco";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 175);
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
            this.Controls.Add(this.buttonRX);
            this.Controls.Add(this.buttonPREP);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPREP;
        private System.Windows.Forms.Button buttonRX;
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
    }
}

