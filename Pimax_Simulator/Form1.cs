﻿using System;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pimax_Simulator
{
    public partial class Form1 : Form
    {
        public static string dataOUT;
        string dataIN;
        readonly string[] mA_Table = new string[8] { "50\r", "100\r", "200\r", "300\r", "400\r", "500\r", "600\r", "700\r" };
        readonly string[] ms_Table = new string[30] { "2\r", "5\r", "8\r", "10\r", "20\r", "30\r", "40\r", "50\r", "60\r", "80\r", "100\r", "120\r", "150\r", "200\r", "250\r", "300\r", "400\r", "500\r", "600\r", "800\r", "1000\r", "1200\r", "1500\r", "2000\r", "2500\r", "3000\r", "3500\r", "4000\r", "4500\r", "5000\r" };
        Boolean ACK = false;
        Boolean NACK = false;
        int kvs, mas, mss;
        float mxs;

        public Form1()
        {
            InitializeComponent();
            OpenSerial();
            kvs = 60;
            mas = 500;
            mss = 100;

        }

        private void buttonPREP_Click(object sender, EventArgs e)
        {
            dataOUT = "PW1";
            serialPort1.WriteLine(dataOUT);
        }

        private void buttonRX_Click(object sender, EventArgs e)
        {

        }

        public void OpenSerial()
        {
            serialPort1.PortName = "COM3";
            serialPort1.BaudRate = int.Parse("19200");  // 115200  Valid values are 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, or 115200.
            serialPort1.DataBits = int.Parse("8");
            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
            serialPort1.Encoding = Encoding.GetEncoding("iso-8859-1");
            // Encoding = Encoding.GetEncoding("Windows-1252");
            serialPort1.Open();
            serialPort1.DtrEnable = false;
            Thread.Sleep(50);
            serialPort1.DtrEnable = true;
            Thread.Sleep(100);
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            // Proceso a medir
            // Aqui
            watch.Stop();
            // Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            dataIN = serialPort1.ReadLine();
            try
            {
                this.Invoke(new EventHandler(ShowData));
            }
            catch (Exception err)
            {
                // MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowData(object sender, EventArgs e)
        {
            string msg = dataIN;
            int mA;
            ACK = false;
            NACK = false;
            mxs = (float) (mas * mss) / 1000;
            if (dataIN == "PI\r")
            {
                
            }

            switch (msg)
            {
                case "PI\r":
                    textBoxKv.Text = kvs.ToString();
                    textBoxmA.Text = mas.ToString() + "\r";
                    textBoxms.Text = mss.ToString() + "\r";
                    textBoxmAs.Text = mxs.ToString();

                    dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MAS" + mas.ToString();
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MSS" + mss.ToString();
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MXS" + mxs.ToString();
                    serialPort1.WriteLine(dataOUT);
                    break;

                case "F?\r":
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT);
                    break;

                case "KV?\r":
                    dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT);
                    break;

                case "MA?\r":
                    dataOUT = "MAS" + mas.ToString();
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MSS" + mss.ToString();
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MXS" + mxs.ToString();
                    serialPort1.WriteLine(dataOUT);
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT);
                    break;

                case "MS?\r":
                    dataOUT = "MAS" + mas.ToString();
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MSS" + mss.ToString();
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MXS" + mxs.ToString();
                    serialPort1.WriteLine(dataOUT);
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT);
                    break;

                case "KV+\r":
                    if (kvs < 125) kvs += 1;
                    textBoxKv.Text = kvs.ToString();
                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT);
                    break;

                case "KV-\r":
                    if (kvs > 35) kvs -= 1;
                    textBoxKv.Text = kvs.ToString();
                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT);
                    break;

                case "MA+\r":
                    mA = 0;
                    for (int i = 0; i <= 7; ++i)      //    Limitado a 7 Valores Maximo disponible 8
                    {
                        if (mA_Table[i] == textBoxmA.Text) mA = i + 1;
                    }
                    if (mA <= 7)                      //    Limitado a 7 Valores Maximo disponible 8
                    {
                        textBoxmA.Text = mA_Table[mA];
                        mas = Convert.ToInt32(textBoxmA.Text);
                    }
                    dataOUT = "MAS" + textBoxmA.Text;
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MSS" + textBoxms.Text;
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MXS" + textBoxmAs.Text;
                    serialPort1.WriteLine(dataOUT);
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT);
                    break;

                case "MA-\r":
                    mA = 0;
                    for (int i = 0; i <= 7; ++i)      //    Limitado a 7 Valores Maximo disponible 8
                    {
                        if (mA_Table[i] == textBoxmA.Text) mA = i - 1;
                    }
                    if (mA >= 0)                      //    Limitado a 7 Valores Maximo disponible 8
                    {
                        textBoxmA.Text = mA_Table[mA];
                        mas = Convert.ToInt32(textBoxmA.Text);
                    }
                    dataOUT = "MAS" + textBoxmA.Text;
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MSS" + textBoxms.Text;
                    serialPort1.WriteLine(dataOUT);
                    dataOUT = "MXS" + textBoxmAs.Text;
                    serialPort1.WriteLine(dataOUT);
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT);
                    break;

                default:
                    break;
            }

            if (mas < 200)
            {
                buttonFF.BackColor = Color.Green;
                buttonFG.BackColor = Color.LightGray;
                buttonFF.Text = "F";
            } else
            {
                buttonFF.BackColor = Color.Green;
                buttonFG.BackColor = Color.Green;
                buttonFF.Text = "G";
            }
        }
    }
}