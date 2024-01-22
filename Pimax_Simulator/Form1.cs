using System;
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
using System.Reflection;
using System.Xml;

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

        StringBuilder sb = new StringBuilder();
        char LF = (char)10;
        char CR = (char)13;

        public Form1()
        {
            InitializeComponent();
            // Create an isntance of XmlTextReader and call Read method to read the file  
            XmlTextReader configReader = new XmlTextReader("C:\\TechDX\\ConfigIF_Demo.xml");

            try
            {
                configReader.Read();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // If the node has value  
            while (configReader.Read())
            {
                if (configReader.NodeType == XmlNodeType.Element && configReader.Name == "SerialPort1")
                {
                    string s1 = configReader.ReadElementContentAsString();
                    serialPort1.PortName = getBetween(s1, "name=", 4);
                    serialPort1.BaudRate = int.Parse(getBetween(s1, "baudrate=", 5));
                    serialPort1.DataBits = int.Parse(getBetween(s1, "databits=", 1));
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), getBetween(s1, "stopbits=", 3));
                    serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), getBetween(s1, "parity=", 4));
                }
            }

            OpenSerial();
            kvs = 60;
            mas = 200;
            mss = 30;
            textBoxKv.Text = kvs.ToString() + "\r";
            textBoxmA.Text = mas.ToString() + "\r";
            textBoxms.Text = mss.ToString() + "\r";
            mxs = (float)(mas * mss) / 1000;
            textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\r";
            this.TopMost = true;
            // Thread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
        }

        private void buttonPREP_Click(object sender, EventArgs e)
        {
            dataOUT = "RIN1880";
            serialPort1.WriteLine(dataOUT + "\r");
            Thread.Sleep(300);
            dataOUT = "PRI"; 
            serialPort1.WriteLine(dataOUT + "\r");
        }

        private void buttonRX_Click(object sender, EventArgs e)
        {
            dataOUT = "XRII";
            serialPort1.WriteLine(dataOUT + "\r");
            dataOUT = "XROI";
            serialPort1.WriteLine(dataOUT + "\r");
            Thread.Sleep(500);
            dataOUT = "PRO";
            serialPort1.WriteLine(dataOUT + "\r");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataOUT = "ER05";
            serialPort1.WriteLine(dataOUT + "\r");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataOUT = "ER03";
            serialPort1.WriteLine(dataOUT + "\r");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataOUT = "ER04";
            serialPort1.WriteLine(dataOUT + "\r");
        }

        public void OpenSerial()
        {
            //   serialPort1.PortName = "COM1";
            //   serialPort1.BaudRate = int.Parse("19200");  // 115200  Valid values are 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, or 115200.
            //   serialPort1.DataBits = int.Parse("8");
            //   serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
            //   serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
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
            // var watch = new System.Diagnostics.Stopwatch();
            // watch.Start();
            // Proceso a medir
            // Aqui
            // watch.Stop();
            // Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            // dataIN = serialPort1.ReadLine();

            string Data = serialPort1.ReadExisting();

            foreach (char c in Data)
            {
                if (c == LF)
                {
                    sb.Append(c);
                    sb.Clear(); 
                }
                else
                {
                    if (c == CR)
                    {
                        sb.Append(c);

                        dataIN = sb.ToString();
                        sb.Clear();

                        //do something with your response 'CurrentLine'
                        try
                        {
                            this.Invoke(new EventHandler(ShowData));
                        }
                        catch (Exception err)
                        {
                            // MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }
        }

        private void ShowData(object sender, EventArgs e)
        {
            string msg = dataIN;
            int mA;
            ACK = false;
            NACK = false;

            if (dataIN.Substring(0, 1) == "V") msg = "VIT\r";
            
            switch (msg)
            {
                case "PI\r":
                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    Thread.Sleep(100);
                    dataOUT = "MAS" + mas.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    Thread.Sleep(100);
                    dataOUT = "MSS" + mss.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    Thread.Sleep(100);
                    mxs = (float)(mas * mss) / 1000;
                    dataOUT = "MXS" + mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    serialPort1.WriteLine(dataOUT + "\r");
                    Thread.Sleep(100);
                    dataOUT = "PI";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "VIT\r":
                    kvs = Convert.ToInt32(dataIN.Substring(1,3));
                    textBoxKv.Text = kvs.ToString();
                    mas = Convert.ToInt32(dataIN.Substring(5,4)) / 10;
                    textBoxmA.Text = mas.ToString() + "\r";
                    mss = Convert.ToInt32(dataIN.Substring(10,5)) / 10;
                    textBoxms.Text = mss.ToString() + "\r";
                    mxs = (float)(mas * mss) / 1000;
                    textBoxmAs.Text = mxs.ToString() + "\r";

                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MAS" + mas.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MSS" + mss.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (kvs == 60)
                    {
                        // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
                        CopyImage("C:\\TechDX\\Images_Demo\\Demo_Image_00.img");
                    }
                    if (kvs == 61)
                    {
                        // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
                        CopyImage("C:\\TechDX\\Images_Demo\\Demo_Image_01.img");
                    }
                    if (kvs == 62)
                    {
                        // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
                        CopyImage("C:\\TechDX\\Images_Demo\\Demo_Image_02.img");
                    }
                    if (kvs == 63)
                    {
                        // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
                        CopyImage("C:\\TechDX\\Images_Demo\\Demo_Image_03.img");
                    }
                    if (kvs == 64)
                    {
                        // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
                        CopyImage("C:\\TechDX\\Images_Demo\\Demo_Image_04.img");
                    }
                    if (kvs == 65)
                    {
                        // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
                        CopyImage("C:\\TechDX\\Images_Demo\\Demo_Image_05.img");
                    }
                    if (kvs == 66)
                    {
                        // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
                        CopyImage("C:\\TechDX\\Images_Demo\\Demo_Image_06.img");
                    }
                    if (kvs == 67)
                    {
                        // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
                        CopyImage("C:\\TechDX\\Images_Demo\\Demo_Image_07.img");
                    }
                    if (kvs == 68)
                    {
                        // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
                        CopyImage("C:\\TechDX\\Images_Demo\\Demo_Image_08.img");
                    }
                    if (kvs == 69)
                    {
                        // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
                        CopyImage("C:\\TechDX\\Images_Demo\\Demo_Image_09.img");
                    }
                    break;

                case "F?\r":
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "KV?\r":
                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "MA?\r":
                    dataOUT = "MAS" + mas.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MSS" + mss.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "MS?\r":
                    dataOUT = "MSS" + mss.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "KV+\r":
                    if (kvs < 125) kvs += 1;
                    textBoxKv.Text = kvs.ToString();
                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "KV-\r":
                    if (kvs > 35) kvs -= 1;
                    textBoxKv.Text = kvs.ToString();
                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
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
                        mxs = (float)(mas * mss) / 1000;
                        textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    dataOUT = "MAS" + textBoxmA.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + textBoxmAs.Text;
                    // If dataOUT have only one character after the . add a zero
                    if (dataOUT.Substring(dataOUT.Length - 2, 1) == ".") dataOUT = dataOUT + "0";
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
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
                        mxs = (float)(mas * mss) / 1000;
                        textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    dataOUT = "MAS" + textBoxmA.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + textBoxmAs.Text;
                    // If dataOUT have only one character after the . add a zero
                    if (dataOUT.Substring(dataOUT.Length - 2, 1) == ".") dataOUT = dataOUT + "0";
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "MS+\r":
                    mA = 0;
                    for (int i = 0; i <= 29; ++i)      //    Valores Maximo disponible 30
                    {
                        if (ms_Table[i] == textBoxms.Text) mA = i + 1;
                    }
                    if (mA <= 29)                     
                    {
                        textBoxms.Text = ms_Table[mA];
                        mss = Convert.ToInt32(textBoxms.Text);
                        mxs = (float)(mas * mss) / 1000;
                        textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    dataOUT = "MSS" + textBoxms.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + textBoxmAs.Text;
                    // If dataOUT have only one character after the . add a zero
                    if (dataOUT.Substring(dataOUT.Length - 2, 1) == ".") dataOUT = dataOUT + "0";
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "MS-\r":
                    mA = 0;
                    for (int i = 0; i <= 29; ++i)      //    Valores Maximo disponible 30
                    {
                        if (ms_Table[i] == textBoxms.Text) mA = i - 1;
                    }
                    if (mA >= 0)
                    {
                        textBoxms.Text = ms_Table[mA];
                        mss = Convert.ToInt32(textBoxms.Text);
                        mxs = (float)(mas * mss) / 1000;
                        textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    dataOUT = "MSS" + textBoxms.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + textBoxmAs.Text;
                    // If dataOUT have only one character after the . add a zero
                    if (dataOUT.Substring(dataOUT.Length - 2, 1) == ".") dataOUT = dataOUT + "0";
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "FS\r":
                    textBoxmA.Text = mA_Table[1];
                    mas = Convert.ToInt32(mA_Table[1]);
                    mxs = (float)(mas * mss) / 1000;
                    textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    dataOUT = "MAS" + textBoxmA.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + textBoxmAs.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "FL\r":
                    textBoxmA.Text = mA_Table[2];
                    mas = Convert.ToInt32(mA_Table[2]);
                    mxs = (float)(mas * mss) / 1000;
                    textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    dataOUT = "MAS" + textBoxmA.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + textBoxmAs.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
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

        void CopyImage(string Image)
        {
            // Copy file in C:\TechDX\Images_Demo to F:\Database\DemoImage
            System.IO.File.Delete("F:\\Database\\DemoImage\\Default.img");
            System.IO.File.Copy(Image, "F:\\Database\\DemoImage\\Default.img");
        }

        public static string getBetween(string strSource, string strStart, int largo)
        {
            if (strSource.Contains(strStart))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = Start + largo;
                return strSource.Substring((Start + 1), End - Start);
            }

            return "";
        }
    }
}