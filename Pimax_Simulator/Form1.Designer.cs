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
            this.SuspendLayout();
            // 
            // buttonPREP
            // 
            this.buttonPREP.Location = new System.Drawing.Point(631, 31);
            this.buttonPREP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPREP.Name = "buttonPREP";
            this.buttonPREP.Size = new System.Drawing.Size(137, 94);
            this.buttonPREP.TabIndex = 0;
            this.buttonPREP.Text = "PREP";
            this.buttonPREP.UseVisualStyleBackColor = true;
            this.buttonPREP.Click += new System.EventHandler(this.buttonPREP_Click);
            // 
            // buttonRX
            // 
            this.buttonRX.Location = new System.Drawing.Point(631, 130);
            this.buttonRX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonRX.Name = "buttonRX";
            this.buttonRX.Size = new System.Drawing.Size(137, 94);
            this.buttonRX.TabIndex = 0;
            this.buttonRX.Text = "Rayos";
            this.buttonRX.UseVisualStyleBackColor = true;
            this.buttonRX.Click += new System.EventHandler(this.buttonRX_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 360);
            this.Controls.Add(this.buttonRX);
            this.Controls.Add(this.buttonPREP);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPREP;
        private System.Windows.Forms.Button buttonRX;
        private System.IO.Ports.SerialPort serialPort1;
    }
}

