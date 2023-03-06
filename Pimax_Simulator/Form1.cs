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

namespace Pimax_Simulator
{
    public partial class Form1 : Form
    {
        public static string dataOUT;
        string dataIN = "", dataIN2 = "", message = "";
        readonly string[] mA_Table = new string[8] { "50\r", "100\r", "200\r", "300\r", "400\r", "500\r", "600\r", "700\r" };
        readonly string[] ms_Table = new string[30] { "2\r", "5\r", "8\r", "10\r", "20\r", "30\r", "40\r", "50\r", "60\r", "80\r", "100\r", "120\r", "150\r", "200\r", "250\r", "300\r", "400\r", "500\r", "600\r", "800\r", "1000\r", "1200\r", "1500\r", "2000\r", "2500\r", "3000\r", "3500\r", "4000\r", "4500\r", "5000\r" };
        Boolean ACK = false;
        Boolean NACK = false;
        int kvs, mas, mss;
        float mxs;

        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        char LF = (char)10;
        char CR = (char)13;

        public Form1()
        {
            InitializeComponent();
         //   OpenSerial();
            OpenSerial2();
            kvs = 60;
            mas = 200;
            mss = 100;
            textBoxKv.Text = kvs.ToString();
            textBoxmA.Text = mas.ToString();
            textBoxms.Text = mss.ToString();
            mxs = (float)(mas * mss) / 1000;
            textBoxmAs.Text = mxs.ToString(System.Globalization.CultureInfo.InvariantCulture);
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

        public void OpenSerial()     // Serial Port para la comunicacion con el Software Vieworks
        {
            serialPort1.PortName = "COM7";
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

        public void OpenSerial2()     // Serial Port para la comunicacion con el Generador
        {
            serialPort2.PortName = "COM9";
            serialPort2.BaudRate = int.Parse("38400");  // 115200  Valid values are 110, 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, or 115200.
            serialPort2.DataBits = int.Parse("8");
            serialPort2.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
            serialPort2.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
            serialPort2.Encoding = Encoding.GetEncoding("iso-8859-1");
            // Encoding = Encoding.GetEncoding("Windows-1252");
            serialPort2.Open();
            serialPort2.DtrEnable = false;
            Thread.Sleep(50);
            serialPort2.DtrEnable = true;
            Thread.Sleep(100);
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
            ACK = false;
            NACK = false;

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
                    textBoxKv.Text = kvs.ToString();
                    mas = Convert.ToInt32(dataIN.Substring(5, 4)) / 10;
                    textBoxmA.Text = mas.ToString() + "\r";
                    mss = Convert.ToInt32(dataIN.Substring(10, 5)) / 10;
                    textBoxms.Text = mss.ToString() + "\r";
                    mxs = (float)(mas * mss) / 1000;
                    textBoxmAs.Text = mxs.ToString() + "\r";

                    if (kvs < 100) dataOUT = "KVS0" + kvs.ToString(); else dataOUT = "KVS" + kvs.ToString();
                    serialPort1.WriteLine(dataOUT + "\r");
                    serialPort2.WriteLine(dataOUT + "\r");  

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
            if (dataIN.Length > 4) msg = dataIN.Remove(0, 4); else msg = "";
            ACK = false;
            NACK = false;
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
                            break;

                        case "IBZ\r":
                            button1.BackColor = Color.Red;  // Inverter error
                            textBoxER.Text = "GAT Desconectado";
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
                            break;

                        case "CPM\r":
                            // Error Stator Boar Missing
                            textBoxER.Text = "Falta Placa Estator";
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
                    // textBox1.Text = dataIN.Remove(0, 4);
                    break;
                case "SN: ":
                    // textBoxSN.Text = dataIN.Remove(0, 4);
                    break;
                case "Kv: ":
                    textBoxKv.Text = dataIN.Remove(0, 4);
                    kvs = Int32.Parse(textBoxKv.Text);
                    break;
                case "mA: ":
                    textBoxmA.Text = dataIN.Remove(0, 4);
                    mas = Int32.Parse(textBoxmA.Text);
                    break;
                case "SKv:":
                    // textBoxSKv.Text = dataIN.Remove(0, 4);
                    break;
                case "SmA:":
                    // textBoxSmA.Text = dataIN.Remove(0, 4);
                    break;
                case "ms: ":
                    textBoxms.Text = dataIN.Remove(0, 4);
                    mss = Int32.Parse(textBoxms.Text);
                    break;
                case "TST:":
                    // textBoxTST.Text = dataIN.Remove(0, 4);
                    break;
                case "FO: ":
                    buttonFF.ForeColor = Color.Yellow;
                    string fo;
                    fo = dataIN.Remove(0, 4);
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
                    // textBoxHO.Text = dataIN.Remove(0, 4);
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
                    textBoxVCC.Text = dataIN.Remove(0, 4);
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
                        buttonPREP.BackColor = Color.LightSkyBlue;
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
                        buttonRX.BackColor = Color.Blue;
                    }
                    break;
                case "XOK:":
                    if (msg == "0\r")
                    {
                        if (buttonPREP.BackColor == Color.LightGreen) buttonRX.BackColor = Color.LightSkyBlue; else buttonRX.BackColor = Color.LightGray;
                    }
                    if (msg == "1\r")
                    {
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
    }
}
