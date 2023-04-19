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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Diagnostics;
using System.Xml;

namespace Pimax_Simulator
{
    public partial class Form1 : Form
    {
        public static string dataOUT;
        string dataIN = "", dataIN2 = "", message = "";
        string Serial1PortName, Serial1BaudRate, Serial1DataBits, Serial1StopBits, Serial1Parity, Serial2PortName, Serial2BaudRate, Serial2DataBits, Serial2StopBits, Serial2Parity;
        readonly string[] mA_Table = new string[8] { "50\r", "100\r", "200\r", "300\r", "400\r", "500\r", "600\r", "700\r" };
        readonly string[] ms_Table = new string[30] { "2\r", "5\r", "8\r", "10\r", "20\r", "30\r", "40\r", "50\r", "60\r", "80\r", "100\r", "120\r", "150\r", "200\r", "250\r", "300\r", "400\r", "500\r", "600\r", "800\r", "1000\r", "1200\r", "1500\r", "2000\r", "2500\r", "3000\r", "3500\r", "4000\r", "4500\r", "5000\r" };
        Boolean ACK = false;
        Boolean NACK = false;
        Boolean AutoON = true;
        Boolean AppExit = false;
        int kvs, mas, mss, Counter;
        float mxs;

        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        char LF = (char)10;
        char CR = (char)13;

        System.Windows.Forms.Timer t = null;

        public Form1()
        {
            InitializeComponent();
            // Create an isntance of XmlTextReader and call Read method to read the file  
            XmlTextReader configReader = new XmlTextReader("C:\\TechDX\\ConfigIF.xml");

            try
            {
                configReader.Read();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
            // If the node has value  
            while (configReader.Read())
            {
                if (configReader.NodeType == XmlNodeType.Element && configReader.Name == "SerialPort1")
                {
                    string s1 = configReader.ReadElementContentAsString();
                    Serial1PortName = getBetween(s1, "name=", 4);
                    Serial1BaudRate = getBetween(s1, "baudrate=", 5);
                    Serial1DataBits = getBetween(s1, "databits=", 1);
                    Serial1StopBits = getBetween(s1, "stopbits=", 3);
                    Serial1Parity = getBetween(s1, "parity=", 4);
                }
                if (configReader.NodeType == XmlNodeType.Element && configReader.Name == "SerialPort2")
                {
                    string s1 = configReader.ReadElementContentAsString();
                    Serial2PortName = getBetween(s1, "name=", 4);
                    Serial2BaudRate = getBetween(s1, "baudrate=", 5);
                    Serial2DataBits = getBetween(s1, "databits=", 1);
                    Serial2StopBits = getBetween(s1, "stopbits=", 3);
                    Serial2Parity = getBetween(s1, "parity=", 4);
                }
            }

            OpenSerial();
            OpenSerial2();
            this.TopMost = true;
        }

        private void buttonPREP_Click(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                if (buttonPREP.BackColor == Color.LightSkyBlue)
                {
                    dataOUT = "PR1";
                }
                else
                {
                    dataOUT = "PR0";
                }
                serialPort2.WriteLine(dataOUT);
            }

        }

        private void buttonRXOn_MouseDw(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                dataOUT = "RX1";
                serialPort2.WriteLine(dataOUT);
                buttonRX.BackColor = Color.Yellow;
                textBoxKv.BackColor = Color.Yellow;
                textBoxmA.BackColor = Color.Yellow;
                textBoxms.BackColor = Color.Yellow;
                textBoxmAs.BackColor = Color.Yellow;
                textBoxKv.Refresh();
                textBoxmA.Refresh();
                textBoxms.Refresh();
                textBoxmAs.Refresh();
                buttonRX.Refresh();
            }
        }

        private void buttonRXOn_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                dataOUT = "RX0";
                serialPort2.WriteLine(dataOUT);
                buttonRX.BackColor = Color.LightGray;
            }
            buttonRX.BackColor = Color.LightGray;
            textBoxKv.BackColor = Color.White;
            textBoxmA.BackColor = Color.White;
            textBoxms.BackColor = Color.White;
            textBoxmAs.BackColor = Color.White;
            textBoxKv.Refresh();
            textBoxmA.Refresh();
            textBoxms.Refresh();
            textBoxmAs.Refresh();
            buttonRX.Refresh();
            dataOUT = "PRO";
            serialPort1.WriteLine(dataOUT + "\r");
        }

        private void StartTimer()
        {
            t = new System.Windows.Forms.Timer();
            t.Interval = 2000;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
        }

        void t_Tick(object sender, EventArgs e)
        {
            Counter += 1;
            if (Counter == 10)
            {
                buttonHRST_Click(sender, e);
            }
            if (Counter == 10)
            {
                if (AppExit)
                {
                    t.Enabled = false;
                    Application.Exit();
                }
                    
                Counter = 0;
            }
            dataOUT = "HS";
            serialPort2.WriteLine(dataOUT);
            if (WaitForACK())
            {
                buttonPW.BackColor = Color.LightGreen;
                ACK = false;
            }
            else
            {
                buttonPW.BackColor = Color.Red;
                ACK = false;
            }
        }

        Boolean WaitForACK()
        {
            int start_time, elapsed_time;
            start_time = DateTime.Now.Second;
            while (!ACK && !NACK)
            {
                elapsed_time = DateTime.Now.Second - start_time;
                if ((elapsed_time >= 2) || (elapsed_time < 0))
                {
                    NACK = true;
                    // textBoxER.Text = "Timeout en Comunicacion";
                    return false;
                }
            }
            if (ACK) return true; else return false;
        }


        private void buttonRX_Click(object sender, EventArgs e)
        {
         /*   dataOUT = "XRII";
            serialPort1.WriteLine(dataOUT + "\r");
            dataOUT = "XROI";
            serialPort1.WriteLine(dataOUT + "\r");
            Thread.Sleep(500);
            dataOUT = "PRO";
            serialPort1.WriteLine(dataOUT + "\r"); */
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

        public void OpenSerial()     // Serial Port para la comunicacion con el Software Vieworks
        {
            serialPort1.PortName = Serial1PortName;
            serialPort1.BaudRate = int.Parse(Serial1BaudRate);  // 115200  Valid values are 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, or 115200.
            serialPort1.DataBits = int.Parse(Serial1DataBits);
            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Serial1StopBits);
            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), Serial1Parity);
            serialPort1.Encoding = Encoding.GetEncoding("iso-8859-1");
            // Encoding = Encoding.GetEncoding("Windows-1252");
            serialPort1.Open();
            serialPort1.DtrEnable = false;
            Thread.Sleep(50);
            serialPort1.DtrEnable = true;
            Thread.Sleep(100);
        }

        public void OpenSerial2()     // Serial Port para la comunicacion con el Generador
        {
            serialPort2.PortName = Serial2PortName;
            serialPort2.BaudRate = int.Parse(Serial2BaudRate);  // 115200  Valid values are 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, or 115200.
            serialPort2.DataBits = int.Parse(Serial2DataBits);
            serialPort2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Serial2StopBits);
            serialPort2.Parity = (Parity)Enum.Parse(typeof(Parity), Serial2Parity);
            serialPort2.Encoding = Encoding.GetEncoding("iso-8859-1");
            // Encoding = Encoding.GetEncoding("Windows-1252");
            serialPort2.Open();
            serialPort2.DtrEnable = false;
            Thread.Sleep(50);
            serialPort2.DtrEnable = true;
            Thread.Sleep(800);
            StartTimer();
        }

        private void buttonPW_Click(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                if (buttonPW.BackColor == Color.LightSkyBlue)
                {
                    dataOUT = "PW1";
                    serialPort2.WriteLine(dataOUT);
                    this.Size = new Size(282, 98);
                    this.Left = 680;
                    this.Top = 1016;
                    this.ControlBox = false;
                    this.Text = "";
                    AutoON = true;
                }
                else
                {
                    dataOUT = "PW0";
                    serialPort2.WriteLine(dataOUT);
                    AutoON = false;
                }
            }

        }

        private void buttonHRST_Click(object sender, EventArgs e)
        {
            serialPort2.DtrEnable = false;
            Thread.Sleep(50);
            serialPort2.DtrEnable = true;
            Thread.Sleep(100);
            serialPort2.DtrEnable = false;
            button1.BackColor = Color.LightGray;
            button2.BackColor = Color.LightGray;
            button3.BackColor = Color.LightGray;
            buttonPW.BackColor = Color.LightGray;
            buttonLuzCol.BackColor = Color.LightGray;
            buttonPREP.BackColor = Color.LightGray;
            buttonRX.BackColor = Color.LightGray;
            buttonFF.BackColor = Color.LightGray;
            buttonFG.BackColor = Color.LightGray;
            textBoxER.Text = "";
            textBox1.Text = "";
            textBoxKv.Text = "";
            textBoxmA.Text = "";
            textBoxms.Text = "";
            textBoxmAs.Text = "";
            textBoxVCC.Text = "";
            Refresh();
        }

        private void buttonLuzCol_Click(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                if (buttonLuzCol.BackColor == Color.LightGray) dataOUT = "CL1"; else dataOUT = "CL0";
                serialPort2.WriteLine(dataOUT);
            }

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            AppExit = true;
            // Application.Exit();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)    // Data received from Software Vieworks
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

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)    // Data received from Generator
        { 
            dataIN2 = serialPort2.ReadLine();
            if (dataIN2.Length > 4) message = dataIN2.Remove(4); else message = "";
            if (dataIN2.Contains("ACK")) ACK = true;
            if (dataIN2.Contains("NACK")) NACK = true;
            Counter = 0;
            try
            {
                this.Invoke(new EventHandler(ShowData2));
            }
            catch (Exception err)
            {
                // MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowData(object sender, EventArgs e)
        {
            string msg = dataIN;
            int mA, ms;

            if (dataIN.Substring(0, 1) == "V") msg = "VIT\r";

            switch (msg)
            {
                case "PI\r":
                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MAS" + mas.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MSS" + mss.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    mxs = (float)(mas * mss) / 1000;
                    dataOUT = "MXS" + mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "PI";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "VIT\r":
                    kvs = Convert.ToInt32(dataIN.Substring(1, 3));
                    // textBoxKv.Text = kvs.ToString();
                    mas = Convert.ToInt32(dataIN.Substring(5, 4)) / 10;
                    // textBoxmA.Text = mas.ToString() + "\r";
                    mss = Convert.ToInt32(dataIN.Substring(10, 5)) / 10;
                    // textBoxms.Text = mss.ToString() + "\r";
                    mxs = (float)(mas * mss) / 1000;
                    textBoxmAs.Text = mxs.ToString() + "\r";

                    dataOUT = "KV" + kvs.ToString(); 
                    serialPort2.WriteLine(dataOUT);
                    Thread.Sleep(200);
                    dataOUT = "MA" + mas.ToString();
                    serialPort2.WriteLine(dataOUT);
                    Thread.Sleep(200);
                    dataOUT = "MS" + mss.ToString();
                    serialPort2.WriteLine(dataOUT);

                    // if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    // serialPort1.WriteLine(dataOUT + "\r");
                    Thread.Sleep(200);
                    dataOUT = "MAS" + mas.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MSS" + mss.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
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
                    dataOUT = "KV" + kvs.ToString();
                    serialPort2.WriteLine(dataOUT);   // Send data to Generator
                    break;

                case "KV-\r":
                    if (kvs > 35) kvs -= 1;
                    textBoxKv.Text = kvs.ToString();
                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "KV" + kvs.ToString();
                    serialPort2.WriteLine(dataOUT);    // Send data to Generator
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
                        dataOUT = "MA" + textBoxmA.Text;
                        serialPort2.WriteLine(dataOUT);   // Send data to Generator
                        mas = Convert.ToInt32(textBoxmA.Text);
                        mxs = (float)(mas * mss) / 1000;
                        textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    dataOUT = "MAS" + textBoxmA.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + textBoxmAs.Text;
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
                        dataOUT = "MA" + textBoxmA.Text;
                        serialPort2.WriteLine(dataOUT);   // Send data to Generator
                        mas = Convert.ToInt32(textBoxmA.Text);
                        mxs = (float)(mas * mss) / 1000;
                        textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    dataOUT = "MAS" + textBoxmA.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + textBoxmAs.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "MS+\r":
                    ms = 0;
                    for (int i = 0; i <= 29; ++i)      //    Valores Maximo disponible 30
                    {
                        if (ms_Table[i] == textBoxms.Text) ms = i + 1;
                    }
                    if (ms <= 29)
                    {
                        textBoxms.Text = ms_Table[ms];
                        dataOUT = "MS" + textBoxms.Text;
                        serialPort2.WriteLine(dataOUT);   // Send data to Generator
                        mss = Convert.ToInt32(textBoxms.Text);
                        mxs = (float)(mas * mss) / 1000;
                        textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    dataOUT = "MSS" + textBoxms.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + textBoxmAs.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "MS-\r":
                    ms = 0;
                    for (int i = 0; i <= 29; ++i)      //    Valores Maximo disponible 30
                    {
                        if (ms_Table[i] == textBoxms.Text) ms = i - 1;
                    }
                    if (ms >= 0)
                    {
                        textBoxms.Text = ms_Table[ms];
                        dataOUT = "MS" + textBoxms.Text;
                        serialPort2.WriteLine(dataOUT);   // Send data to Generator
                        mss = Convert.ToInt32(textBoxms.Text);
                        mxs = (float)(mas * mss) / 1000;
                        textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    }
                    dataOUT = "MSS" + textBoxms.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "MXS" + textBoxmAs.Text;
                    serialPort1.WriteLine(dataOUT + "\r");
                    if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                    serialPort1.WriteLine(dataOUT + "\r");
                    break;

                case "FS\r":
                    textBoxmA.Text = mA_Table[1];
                    dataOUT = "MA" + textBoxmA.Text;
                    serialPort2.WriteLine(dataOUT);   // Send data to Generator
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
                    dataOUT = "MA" + textBoxmA.Text;
                    serialPort2.WriteLine(dataOUT);   // Send data to Generator
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
            }
            else
            {
                buttonFF.BackColor = Color.Green;
                buttonFG.BackColor = Color.Green;
                buttonFF.Text = "G";
            }
        }

        private void ShowData2(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            string msg;
            if (dataIN2.Length > 4) msg = dataIN2.Remove(0, 4); else msg = "";
            // ACK = false;
            // NACK = false;
            switch (message)
            {
                case "ER: ":
                    if (msg != "\r")
                    {
                        textBoxER.Text = "";

                    }
                    switch (msg)
                    {
                        case "LHB\r":
                             textBoxER.Text = "Falla de Lampara Testigo Calefaccion";
                            break;

                        case "CAP\r":
                            textBoxER.Text = "Falla de Estator (UCap)";
                            break;

                        case "COM\r":
                            textBoxER.Text = "Falla de Estator (ICom)";
                            break;

                        case "IBE\r":
                            button1.BackColor = Color.Red;
                            textBoxER.Text = "Falla de Inversor";
                            dataOUT = "ER05";
                            serialPort1.WriteLine(dataOUT + "\r");
                            break;

                        case "IBZ\r":
                            button1.BackColor = Color.Red;  // Inverter error
                            textBoxER.Text = "GAT Desconectado";
                            dataOUT = "ER05";
                            serialPort1.WriteLine(dataOUT + "\r");
                            break;

                        case "FIL\r":
                            button2.BackColor = Color.Red;
                            textBoxER.Text = "Falla de Filamento";
                            if (buttonFF.Text == "F")
                            {
                                textBoxER.Text += " Fino";
                                buttonFF.BackColor = Color.Red;  // Small Filament error
                            }
                            else
                            {
                                textBoxER.Text += " Grueso";
                                buttonFG.BackColor = Color.Red;  // Large Filament error                 
                            }
                            button2.BackColor = Color.Red;
                            dataOUT = "ER03";
                            serialPort1.WriteLine(dataOUT + "\r");
                            break;

                        case "FCC\r":
                            if (buttonFF.Text == "F")
                            {
                                textBoxER.Text = "Filamento Fino en Corto Circuito";
                                buttonFF.BackColor = Color.Red;  // Small Filament CC error
                            }
                            if (buttonFF.Text == "G")
                            {
                                textBoxER.Text = "Filamento Grueso en Corto Circuito";
                                buttonFG.BackColor = Color.Red;  // Large Filament CC error
                            }
                            button2.BackColor = Color.Red;
                            dataOUT = "ER03";
                            serialPort1.WriteLine(dataOUT + "\r");
                            break;

                        case "TMP\r":
                            button3.BackColor = Color.Red;        // Temperatura Tubo
                            textBoxER.Text = "Temperatura de Tubo Exedida";
                            break;

                        case "EEE\r":
                            textBoxER.Text = "Falla de Memoria EEPROM";
                            break;

                        case "SYM\r":
                            textBoxER.Text = "Simulador Activado";
                            break;

                        case "UPW\r":
                            button1.BackColor = Color.Red;
                            textBoxER.Text = "Baja Tension en UPower";
                            dataOUT = "ER05";
                            serialPort1.WriteLine(dataOUT + "\r");
                            break;

                        case "CPM\r":
                            // Error Stator Boar Missing
                            textBoxER.Text = "Falta Placa Estator";
                            button3.BackColor = Color.Red;
                            dataOUT = "ER04";
                            serialPort1.WriteLine(dataOUT + "\r");
                            break;

                        case "FPE1\r":
                            // Fin Prep Board Missing o Relay Pegado
                            textBoxER.Text = "Falla de Relay Preparacion";
                            break;

                        default:
                            if (msg != "\r") textBoxER.Text = msg;
                            break;
                    }
                    // buttonCal.BackColor = Color.RosyBrown;
                    break;
                case "ET: ":
                    textBox1.Text = dataIN2.Remove(0, 4);
                    break;
                case "SN: ":
                    // textBoxSN.Text = dataIN2.Remove(0, 4);
                    break;
                case "Kv: ":
                    textBoxKv.Text = dataIN2.Remove(0, 4);
                    kvs = Int32.Parse(textBoxKv.Text);
                    break;
                case "mA: ":
                    textBoxmA.Text = dataIN2.Remove(0, 4);
                    mas = Int32.Parse(textBoxmA.Text);
                    break;
                case "SKv:":
                    // textBoxSKv.Text = dataIN2.Remove(0, 4);
                    break;
                case "SmA:":
                    // textBoxSmA.Text = dataIN2.Remove(0, 4);
                    break;
                case "ms: ":
                    textBoxms.Text = dataIN2.Remove(0, 4);
                    mss = Int32.Parse(textBoxms.Text);
                    break;
                case "TST:":
                    // textBoxTST.Text = dataIN2.Remove(0, 4);
                    break;
                case "FO: ":
                    buttonFF.ForeColor = Color.Yellow;
                    string fo;
                    fo = dataIN2.Remove(0, 4);
                    if (fo == "FF\r")
                    {
                            buttonFF.BackColor = Color.Green;
                            buttonFG.BackColor = Color.LightGray;
                            buttonFF.Text = "F";
                   }
                    if (fo == "FG\r")
                    {
                            buttonFF.BackColor = Color.Green;
                            buttonFG.BackColor = Color.Green;
                            buttonFF.Text = "G";
                    }
                    break;
                case "HO: ":
                    // textBoxHO.Text = dataIN2.Remove(0, 4);
                    break;
                case "CAL:":
                    if (msg == "1\r")
                    {
                        // buttonCal.BackColor = Color.Red;
                        // buttonWC.BackColor = Color.LightSkyBlue;
                        // this.Size = new Size(800, 600);
                        // this.Left = 700;
                        // this.Top = 500;
                    }
                    else
                    {
                        // buttonCal.BackColor = Color.LightSkyBlue;
                        // buttonWC.BackColor = Color.LightGray;
                        // this.Size = new Size(800, 335);
                        // this.Left = 870;
                        // this.Top = 930;
                    }
                    break;

                    case "VCC:":
                    textBoxVCC.Text = dataIN2.Remove(0, 4);
                    if (textBoxVCC.Text != "")
                    {
                        try
                        {
                            float Test = float.Parse(textBoxVCC.Text);
                            if (Test < 15.0 || Test > 15.6) textBoxVCC.BackColor = Color.Red;
                            else
                            {
                                if (Test < 15.2 || Test > 15.4) textBoxVCC.BackColor = Color.Yellow;
                                else
                                {
                                    if (Test > 15.19 || Test < 15.39) textBoxVCC.BackColor = Color.LightGreen;
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            //   MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case "UPW:":
                    // textBoxUPW.Text = dataIN.Remove(0, 4);
                    break;
                case "FT: ":
                    if (msg == "0\r")
                    {
                     //   buttonSFP.BackColor = Color.LightGray;
                     //   buttonSFP.Text = "No Fluoro";
                     //   buttonFLOn.BackColor = Color.LightGray;
                    }
                    if (msg == "1\r")
                    {
                     //   buttonSFP.BackColor = Color.LightYellow;
                     //   buttonSFP.Text = "Fluoro C";
                    }
                    if (msg == "2\r")
                    {
                     //   buttonSFP.BackColor = Color.LightGreen;
                     //   buttonSFP.Text = "Fluoro P";
                    }
                    break;
                case "RD: ":
                    if ((buttonPW.BackColor == Color.LightSkyBlue) && AutoON)
                    {
                        buttonPW_Click(sender, e);
                    }

                    if (msg == "0\r")
                    {
                     //   buttonSPot1.BackColor = Color.LightYellow;
                     //   buttonSPot1.Text = "No Potter";
                    }
                    if (msg == "1\r")
                    {
                     //   buttonSPot1.BackColor = Color.LightGreen;
                     //   buttonSPot1.Text = "Potter 1";
                    }
                    if (msg == "2\r")
                    {
                     //   buttonSPot1.BackColor = Color.LightGreen;
                     //   buttonSPot1.Text = "Potter 2";
                    }
                    if (msg == "3\r")
                    {
                     //   buttonSPot1.BackColor = Color.LightGreen;
                     //   buttonSPot1.Text = "CINE";
                    }
                    if (msg == "4\r")
                    {
                     //   buttonSPot1.BackColor = Color.Red;
                     //   buttonSPot1.Text = "Service";
                    }
                    break;
                case "CL: ":
                    if (msg == "1\r") buttonLuzCol.BackColor = Color.LightYellow; else buttonLuzCol.BackColor = Color.LightGray;
                    break;
                case "POK:":
                    if (msg == "0\r")
                    {
                        if(buttonPW.BackColor == Color.LightGreen) buttonPREP.BackColor = Color.LightSkyBlue;
                        buttonRX.BackColor = Color.LightGray;
                        textBoxKv.BackColor = Color.White;
                        textBoxmA.BackColor = Color.White;
                        textBoxms.BackColor = Color.White;
                        textBoxmAs.BackColor = Color.White;
                        textBoxKv.Refresh();
                        textBoxmA.Refresh();
                        textBoxms.Refresh();
                        textBoxmAs.Refresh();
                    }
                    if (msg == "1\r")
                    {
                        buttonPREP.BackColor = Color.LightYellow;
                        button1.BackColor = Color.LightGreen;
                        button2.BackColor = Color.LightGreen;
                        button3.BackColor = Color.LightGreen;
                    }
                    if (msg == "2\r")
                    {
                        buttonPREP.BackColor = Color.LightGreen;
                        buttonPREP.Refresh();
                        dataOUT = "RIN1880";                      // Enviar al Software VXvue el Preparacion OK
                        serialPort1.WriteLine(dataOUT + "\r");
                        Thread.Sleep(300);
                        dataOUT = "PRI";
                        serialPort1.WriteLine(dataOUT + "\r");
                        buttonRX.BackColor = Color.Blue;
                        buttonRX.Refresh();
                    }
                    break;
                case "XOK:":
                    if (msg == "0\r")
                    {
                        if (buttonPREP.BackColor == Color.LightGreen) buttonRX.BackColor = Color.LightSkyBlue; else buttonRX.BackColor = Color.LightGray;
                    }
                    if (msg == "1\r")
                    {
                        dataOUT = "XRII";
                        serialPort1.WriteLine(dataOUT + "\r");
                        dataOUT = "XROI";
                        serialPort1.WriteLine(dataOUT + "\r");
                        buttonRX.BackColor = Color.Yellow;
                        textBoxKv.BackColor = Color.Yellow;
                        textBoxmA.BackColor = Color.Yellow;
                        textBoxms.BackColor = Color.Yellow;
                        textBoxmAs.BackColor = Color.Yellow;
                        buttonRX.Refresh();
                        textBoxKv.Refresh();
                        textBoxmA.Refresh();
                        textBoxms.Refresh();
                        textBoxmAs.Refresh();
                    }
                    break;
                case "EEP:":
                    // ConfigSize = Convert.ToInt32(dataIN.Remove(0, 4));
                    // ConfigReady = true;
                    break;

                case "TC1:":
                    // textBoxTC1.Text = dataIN.Remove(0, 4) + "ºC";
                    break;

                default:
                    break;
            }

            if ((textBox1.Text == "OFF\r") || (textBox1.Text == "ERROR\r") || (textBox1.Text == "\r"))
            {
                buttonPW.BackColor = Color.LightSkyBlue;
            }

            if ((textBox1.Text == "IDLE\r")) // || (textBox1.Text == "ERROR\r") || (textBox1.Text == "\r")
            {
                buttonPW.BackColor = Color.LightGreen;
            }

            if ((textBoxmA.Text != "") && (textBoxms.Text != ""))
            {
                try
                {
                    textBoxmAs.Text = ((float)Convert.ToInt32(textBoxmA.Text) * Convert.ToInt32(textBoxms.Text) / 1000).ToString();
                }
                catch (Exception err)
                {
                    // MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (AppExit)
            {
                t.Enabled = false;
                Application.Exit();
            }
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
