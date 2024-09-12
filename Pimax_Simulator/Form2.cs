using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pimax_Simulator
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.TopMost = true;
            // set the password char to *
            textBox1.PasswordChar = '*';
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (textBox1.Text == "aspor")
            {
                Process.Start("notepad.exe", "C:\\TechDX\\LogIFDUE.txt");
            }
            this.Close();
        }
    }
}
