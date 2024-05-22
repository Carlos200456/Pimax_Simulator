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
using System.IO;
using System.Xml;
using System.Media;
using System.Runtime.InteropServices;

namespace Pimax_Simulator
{
    public partial class Form1 : Form
    {
        public static string dataOUT, path;
        string SW_Version = "3.0\r";        // =======> Version de software para compatibilidad
        string dataIN = "", dataIN2 = "", message = "", textKVP, textKVN, textmAReal, textRmA, LastER, textSFI, textSRE, textSCC, textSIC, textSUC, textUPW;
        string Serial1PortName, Serial1BaudRate, Serial1DataBits, Serial1StopBits, Serial1Parity, Serial2PortName, Serial2BaudRate, Serial2DataBits, Serial2StopBits, Serial2Parity;
        readonly string[] mA_Table = new string[8] { "50\r", "100\r", "200\r", "300\r", "400\r", "500\r", "600\r", "700\r" };
        readonly string[] ms_Table = new string[30] { "2\r", "5\r", "8\r", "10\r", "20\r", "30\r", "40\r", "50\r", "60\r", "80\r", "100\r", "120\r", "150\r", "200\r", "250\r", "300\r", "400\r", "500\r", "600\r", "800\r", "1000\r", "1200\r", "1500\r", "2000\r", "2500\r", "3000\r", "3500\r", "4000\r", "4500\r", "5000\r" };
        Boolean ACK = false;
        Boolean NACK = false;
        Boolean AutoON = true;
        Boolean setPrep = false;
        Boolean SW_Ready = false;
        int kvs, mas, mss, Counter;
        float mxs;

        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        char LF = (char)10;
        char CR = (char)13;

        System.Windows.Forms.Timer t = null;

        Logger logger = new Logger("C:\\TechDX\\LogIFDUE.txt");    // Ruta del archivo de log

        // Class-level field
        private SoundPlayer[] soundPlayers;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, uint dwDuration);

        public Form1()
        {
            InitializeComponent();
            path = AppDomain.CurrentDomain.BaseDirectory;
            SetImage(path + "About.png");
            // Create an isntance of XmlTextReader and call Read method to read the file  
            XmlTextReader configReader = new XmlTextReader("C:\\TechDX\\ConfigIF.xml");
            
            LastER = "";

            try
            {
                configReader.Read();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Load your sound files into the array
            soundPlayers = new SoundPlayer[]
            {
                new SoundPlayer(path + "Sounds\\XRay_0.wav"),
                new SoundPlayer(path + "Sounds\\XRay_1.wav"),
                new SoundPlayer(path + "Sounds\\XRay_2.wav"),
                new SoundPlayer(path + "Sounds\\XRay_3.wav"),
                new SoundPlayer(path + "Sounds\\XRay_4.wav"),
                new SoundPlayer(path + "Sounds\\XRay_5.wav"),
                new SoundPlayer(path + "Sounds\\XRay_6.wav"),
                new SoundPlayer(path + "Sounds\\XRay_7.wav"),
                new SoundPlayer(path + "Sounds\\XRay_8.wav"),
            //    new SoundPlayer(path + "Sounds\\XRay_9.wav"),
            };
            // Beep(1200, 200);
        }

        // Rutina para colocar una imagen en el form
        private void SetImage(string path)
        {
            try
            {
                Image img = Image.FromFile(path);
                this.BackgroundImage = img;
                this.BackgroundImageLayout = ImageLayout.Stretch;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            dataOUT = "HS";
            serialPort2.WriteLine(dataOUT);
            if (WaitForACK())
            {
                buttonPW.BackColor = Color.LightGreen;
                ACK = false;
                LastER = "";
            }
            else
            {
                buttonPW.BackColor = Color.Red;
                ACK = false;
                if (LastER != "Hand Shake Error")
                {
                    logger.LogError("Hand Shake Error");
                    LastER = "Hand Shake Error";
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            // dataOUT = "ER05";
            // serialPort1.WriteLine(dataOUT + "\r");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // dataOUT = "ER03";
            // serialPort1.WriteLine(dataOUT + "\r");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // dataOUT = "ER04";
            // serialPort1.WriteLine(dataOUT + "\r");
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
                    if (SW_Ready)
                    {
                        dataOUT = "PW1";
                        serialPort1.WriteLine(dataOUT);
                        this.Size = new Size(282, 98);
                        this.Left = 680;
                        this.Top = 1016;
                        this.ControlBox = false;
                        this.Text = "";
                        logger.LogInfo("Turn On by Operator");
                        AutoON = true;
                    }
                    else
                    {
                        MessageBox.Show("Error de Software, Versiones incompatibles de Generador y GUI");
                    }
                }
                else
                {
                    // dataOUT = "PW0";
                    // serialPort2.WriteLine(dataOUT);
                    // logger.LogInfo("Turn Off by Operator");
                    // AutoON = false;
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
                GUI_Sound(1);
            }

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            logger.LogInfo("Salida de la Aplicación por el operador");
            // Call Form1_FormClosing
            Form_FormClosing(sender, new FormClosingEventArgs(CloseReason.UserClosing, false));
            Application.Exit();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (serialPort1.IsOpen || serialPort2.IsOpen)
            {
                e.Cancel = true; //cancel the fom closing
                Thread CloseDown = new Thread(new ThreadStart(CloseSerialOnExit)); //close port in new thread to avoid hang
                CloseDown.Start(); //close port in new thread to avoid hang
            }
        }

        private void CloseSerialOnExit()
        {
            try
            {
                serialPort1.Close(); //close the serial port
                serialPort2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //catch any serial port closing error messages
            }
            this.Invoke(new EventHandler(NowClose)); //now close back in the main thread
        }

        private void NowClose(object sender, EventArgs e)
        {
            this.Close(); //now close the form
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
            if (dataIN2.Contains("ACK"))
            {
                ACK = true;
                // GUI_Sound(1);
            }
            if (dataIN2.Contains("NACK"))
            {
                NACK = true;
                GUI_Sound(6);
            }
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
                    if (buttonPW.BackColor == Color.LightGreen) serialPort1.WriteLine(dataOUT + "\r");
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
                    if (kvs < 125) kvs += 1; else GUI_Sound(6);
                    // textBoxKv.Text = kvs.ToString();
                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "KV" + kvs.ToString();
                    serialPort2.WriteLine(dataOUT);   // Send data to Generator
                    // GUI_Sound(1);
                    break;

                case "KV-\r":
                    if (kvs > 35) kvs -= 1; else GUI_Sound(6);
                    // textBoxKv.Text = kvs.ToString();
                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    dataOUT = "KV" + kvs.ToString();
                    serialPort2.WriteLine(dataOUT);    // Send data to Generator
                    // GUI_Sound(1);
                    break;

                case "MA+\r":
                    mA = 0;
                    for (int i = 0; i <= 6; ++i)      //    Limitado a 7 Valores Maximo disponible 8
                    {
                        if (mA_Table[i] == textBoxmA.Text) mA = i + 1;
                    }
                    if (mA <= 6)                      //    Limitado a 7 Valores Maximo disponible 8
                    {
                        textBoxmA.Text = mA_Table[mA];
                        dataOUT = "MA" + textBoxmA.Text;
                        serialPort2.WriteLine(dataOUT);   // Send data to Generator
                        mas = Convert.ToInt32(textBoxmA.Text);
                        mxs = (float)(mas * mss) / 1000;
                        textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        GUI_Sound(1);
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
                        dataOUT = "MA" + textBoxmA.Text;
                        serialPort2.WriteLine(dataOUT);   // Send data to Generator
                        mas = Convert.ToInt32(textBoxmA.Text);
                        mxs = (float)(mas * mss) / 1000;
                        textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        GUI_Sound(1);
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
                        GUI_Sound(1);
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
                        GUI_Sound(1);
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
                        GUI_Sound(6);
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
                                LoggearError();
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
                            dataOUT = "ER04";
                            serialPort1.WriteLine(dataOUT + "\r");
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
                            textBoxER.Text = "Verificar Relay Preparacion";
                            LoggearError();
                            break;

                        case "ESF0\r":
                            textBoxER.Text = "Falla de Relay Foco Fino";
                            LoggearError();
                            break;

                        case "ESF1\r":
                            textBoxER.Text = "Falla de Relay Foco Grueso";
                            LoggearError();
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
                case "SW: ":
                    textBoxSW.Text = "Version " + dataIN2.Remove(0, 4);
                    if (dataIN2.Remove(0, 4) != SW_Version)
                    {
                        MessageBox.Show("Error de Software, Versiones incompatibles de Generador y GUI");
                        SW_Ready = false;
                    }
                    SW_Ready = true;
                    break;
                case "Kv: ":
                    if (textBoxKv.Text != dataIN2.Remove(0, 4))
                    {
                        textBoxKv.Text = dataIN2.Remove(0, 4);
                        GUI_Sound(1);
                    }
                    kvs = Int32.Parse(textBoxKv.Text);
                    break;
                case "mA: ":
                    if (textBoxmA.Text != dataIN2.Remove(0, 4))
                    {
                        textBoxmA.Text = dataIN2.Remove(0, 4);
                        dataOUT = "MAS" + textBoxmA.Text;
                        serialPort1.WriteLine(dataOUT + "\r");
                        try
                        {
                            textBoxmAs.Text = ((float)Convert.ToInt32(textBoxmA.Text) * Convert.ToInt32(textBoxms.Text) / 1000).ToString();
                        }
                        catch (Exception err)
                        {
                            // MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        dataOUT = "MXS" + textBoxmAs.Text;
                        // If dataOUT have only one character after the . add a zero
                        if (dataOUT.Substring(dataOUT.Length - 2, 1) == ".") dataOUT = dataOUT + "0";
                        serialPort1.WriteLine(dataOUT + "\r");
                        if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                        serialPort1.WriteLine(dataOUT + "\r");
                    }
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
                    if (textBoxms.Text != dataIN2.Remove(0, 4))
                    {
                        textBoxms.Text = dataIN2.Remove(0, 4);
                        dataOUT = "MSS" + textBoxms.Text;
                        serialPort1.WriteLine(dataOUT + "\r");
                        try
                        {
                            textBoxmAs.Text = ((float)Convert.ToInt32(textBoxmA.Text) * Convert.ToInt32(textBoxms.Text) / 1000).ToString();
                        }
                        catch (Exception err)
                        {
                            // MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        dataOUT = "MXS" + textBoxmAs.Text;
                        // If dataOUT have only one character after the . add a zero
                        if (dataOUT.Substring(dataOUT.Length - 2, 1) == ".") dataOUT = dataOUT + "0";
                        serialPort1.WriteLine(dataOUT + "\r");
                        if (mas < 200) dataOUT = "FS"; else dataOUT = "FL";
                        serialPort1.WriteLine(dataOUT + "\r");
                    }
                    textBoxms.Text = dataIN2.Remove(0, 4);
                    mss = Int32.Parse(textBoxms.Text);
                    break;
                case "Kv+:":
                    textKVP = dataIN2.Remove(0, 4);
                    break;
                case "Kv-:":
                    textKVN = dataIN2.Remove(0, 4);
                    break;
                case "RmA:":
                    textRmA = dataIN2.Remove(0, 4);
                    try
                    {
                        decimal mARead = Convert.ToDecimal(textRmA);
                        int mASet = Convert.ToInt32(textBoxmA.Text);
                        int mAReal = (int)(mARead * mASet) / 1000;
                        textmAReal = mAReal.ToString();
                    }
                    catch
                    {
                        textmAReal = "???";
                    }
                    break;
                case "SFI:":
                    textSFI = dataIN.Remove(0, 4);
                    break;
                case "SRE:":
                    textSRE = dataIN.Remove(0, 4);
                    break;
                case "SCC:":
                    textSCC = dataIN.Remove(0, 4);
                    break;
                case "SIC:":
                    textSIC = dataIN.Remove(0, 4);
                    break;
                case "SUC:":
                    textSUC = dataIN.Remove(0, 4);
                    break;
//                case "UPW:":
//                    textUPW = dataIN.Remove(0, 4);
//                    break;
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
                        dataOUT = "DB0";
                        serialPort2.WriteLine(dataOUT);   // Send data to Generator to turn off Calibration
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
                        if (setPrep)
                        {
                            dataOUT = "PRO";
                            serialPort1.WriteLine(dataOUT + "\r");
                            setPrep = false;
                        }
                    }
                    if (msg == "1\r")
                    {
                        button1.BackColor = Color.LightGreen;
                        button2.BackColor = Color.LightGreen;
                        button3.BackColor = Color.LightGreen;
                        // GUI_Sound(2);
                    }
                    if (msg == "2\r")
                    {
                        dataOUT = "RIN1880";                      // Enviar al Software VXvue el Preparacion OK
                        serialPort1.WriteLine(dataOUT + "\r");
                        Thread.Sleep(300);
                        dataOUT = "PRI";
                        serialPort1.WriteLine(dataOUT + "\r");
                        setPrep = true;
                        GUI_Sound(1);
                    }
                    break;
                case "XOK:":
                    if (msg == "0\r")
                    {
                        // buttonPrep
                    }
                    if (msg == "1\r")
                    {
                        dataOUT = "XRII";
                        serialPort1.WriteLine(dataOUT + "\r");
                        dataOUT = "XROI";
                        serialPort1.WriteLine(dataOUT + "\r");
                        dataOUT = "PRO";
                        serialPort1.WriteLine(dataOUT + "\r");
                    }
                    break;
                case "EEP:":
                    // ConfigSize = Convert.ToInt32(dataIN.Remove(0, 4));
                    // ConfigReady = true;
                    break;

                case "TC1:":
                    // textBoxTC1.Text = dataIN.Remove(0, 4) + "ºC";
                    break;

                case "LOG:":
                    // GUI_Sound(4);
                    Beep(1600, 400);
                    logger.LogInfo("VCC:" + textBoxVCC.Text.Substring(0, textBoxVCC.Text.Length - 1) +
                                   " Kv:" + textBoxKv.Text.Substring(0, textBoxKv.Text.Length - 1) +
                                   " mA:" + textBoxmA.Text.Substring(0, textBoxmA.Text.Length - 1) +
                                   " ms:" + textBoxms.Text.Substring(0, textBoxms.Text.Length - 1) +
                                   " Kv+:" + textKVP.Substring(0, textKVP.Length - 1) +
                                   " Kv-:" + textKVN.Substring(0, textKVN.Length - 1) +
                                   " mA:" + textmAReal);

                    // logger.LogWarning("Este es un mensaje de advertencia.");
                    // logger.LogError("Este es un mensaje de error.");
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

        private void LoggearError()
        {
            if (LastER != textBoxER.Text)
            {
                try
                {
                    logger.LogError(textBoxER.Text);
                    logger.LogWarning("VCC:" + textBoxVCC.Text.Substring(0, textBoxVCC.Text.Length - 1) +
                                " ,Sense Ref:" + textSRE.Substring(0, textSRE.Length - 1) +
                                " ,Sense Fil:" + textSFI.Substring(0, textSFI.Length - 1) +
                                " ,Sense CC:" + textSCC.Substring(0, textSCC.Length - 1) +
                                " ,U Cap:" + textSUC.Substring(0, textSUC.Length - 1) +
                                " ,I Com:" + textSIC.Substring(0, textSIC.Length - 1) +
                                " ,U Power:" + textUPW.Substring(0, textUPW.Length - 1));
                    LastER = textBoxER.Text;
                }
                catch (Exception err)
                {
                    // MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        public void GUI_Sound(int soundIndex)
        {
            if (soundIndex >= 0 && soundIndex < soundPlayers.Length)
            {
                try
                {
                    soundPlayers[soundIndex].Play();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error playing sound: {ex.Message}");
                }
            }
        }
    }

    // Add a public class logger to the project
    public class Logger
    {
        private string logFilePath;

        public Logger(string logFilePath)
        {
            this.logFilePath = logFilePath;
            // Si el archivo no existe, crea uno nuevo; si existe, lo abre en modo append (agregar al final).
            File.AppendAllText(logFilePath, $"{ DateTime.Now} [INFO]  === Inicio de Log Interface Pimax VxView ===" + Environment.NewLine);
        }

        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogWarning(string message)
        {
            WriteLog("WARNING", message);
        }

        public void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        private void WriteLog(string logLevel, string message)
        {
            string logEntry = $"{DateTime.Now} [{logLevel}] - {message}{Environment.NewLine}";
            File.AppendAllText(logFilePath, logEntry);
        }
    }
}
