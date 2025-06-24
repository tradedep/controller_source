using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
//using System.Management;
using Microsoft.Win32;
using System.Threading;
using LightController;
using AutoWindowsSize;



namespace HW_204N
{
    public partial class _3GenerationMiniController : Form
    {
        AutoAdaptWindowsSize AutoSize;
        int flat;
        //private bool m_CH340Flag = false;//CH340驱动存在标志位
        //private string[] comNames = null;
        private int m_comBoxNums = 0;
        private string m_LastComPort = null;
        //private bool m_ComOpened = false;
       //private bool m_ComRcvOk = false;
        private int[] lights = new int[32];                    //预设32通道亮度
        private int[] puls = new int[32];                      //预设32通道脉宽
        int Now_Model = 1;                                    //当前模式

        //double value;
       // int sleepTime;

        byte[] rcvBuffer = new byte[8];
        byte[] senBuffer = new byte[8];
        

        

        public _3GenerationMiniController()
        {
            InitializeComponent();
            tbState.Text = "Serial port is not open";
            butt_state(false);
            flat = 0;
            AutoSize = new AutoAdaptWindowsSize(this);
           // sleepTime = 50;               //通讯等待时间

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes("$24")));
 
            //textBox1.Text = "";    //清空消息框
            //Array.Clear(rcvBuffer, 0, rcvBuffer.Length);
            //comboBox1_SelectedValueChanged(null,null);
        }

        #region 定时刷新串口，获取端口号
        private void timer1_Tick(object sender, EventArgs e)
        {
            string Cur_PortName = comBoxPorts.Text;

            try
            {
                string[] strName = null;
                strName = SerialPort.GetPortNames();

                if (m_comBoxNums != strName.Length)
                {
                    m_comBoxNums = strName.Length;

                    comBoxPorts.Items.Clear();
                    comBoxPorts.Text = null;
                    tbState.Text = "Serial Port is not open";

                    for (int i = 0; i < m_comBoxNums; i++)                              //判断串口刷新后状态
                    {
                        if (strName[i] == Cur_PortName) { comBoxPorts.Text = Cur_PortName; tbState.Text = "Serial Port opened successfully"; }
                        comBoxPorts.Items.Add(strName[i]);
                    }
                    comBoxPorts.Text = strName[0];
                    serialPort1.PortName = comBoxPorts.Text;
                }
                flat = 1;
            }
                
            catch (Exception ex)
            {
                if (ex.Source != null)
                { 
                }
            }
        }

        #endregion



        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string strLeftLight = null, strLeftColTem = null, strRightLight = null, strRightColTem = null;

            //string str_rcv = serialPort1.ReadExisting();

            //Array.Clear(rcvBuffer, 0, rcvBuffer.Length);
            
            //int count = serialPort1.BytesToRead;
            //serialPort1.Read(rcvBuffer, 0, count);
            //serialPort1.Read(rcvBuffer, 0, 8);
            
        }
  
        private void comBoxPorts_SelectedValueChanged(object sender, EventArgs e)
        {
            
            if (comBoxPorts.Text != "" && flat==1)
            {
                if(m_LastComPort != "" )
                {
                    serialPort1.Close();    
                }

                serialPort1.PortName = comBoxPorts.Text;

                m_LastComPort = comBoxPorts.Text;
            }
            

            try
            {
                if (!serialPort1.IsOpen && flat == 1)
                {
                    serialPort1.Open();
                    tbState.Text = "Serial Port opened successfully";
                    button5.Text = "Disconnect";
                    butt_state(true);
                    button6_Click(null, null);          //刷新函数

                }
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                {
                    tbState.Text = "Serial Port opening failed";
                    button5.Text = "Connect";
                    butt_state(false);
                    return;

                    //serialPort1.PortName = null;

                    //m_LastComPort = null;
                }
            }
        }
        //窗体关闭
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (serialPort1.PortName != null)
                {
                    serialPort1.Close();
                    tbState.Text = "Serial Port is not open";
                }
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                { 
                
                }
            }
        }


        //command 命令  port 通道  value数据值
        private void serialsend(byte command, byte port, int value)
        {
            byte[] b_value = new byte[3];              //亮度值用3位字节表示

            byte[] b_check = new byte[2];              //校验位用2位字表示

            string str1 = value.ToString("X3");        //亮度值处理,将value转化为3位的16进制字符
            b_value[0] = Convert.ToByte(str1[0]);      //将数据转化为Ascall码十进制数
            b_value[1] = Convert.ToByte(str1[1]);      //将数据转化为Ascall码十进制数
            b_value[2] = Convert.ToByte(str1[2]);      //将数据转化为Ascall码十进制数

            //MessageBox.Show(b_value[0].ToString() + " " + b_value[1].ToString() + " " + b_value[2].ToString());

            senBuffer[0] = 0x24;                                //特征字$
            //等价于senBuffer[0] =36; 
            senBuffer[1] = Convert.ToByte(command + 48);                      //命令1表示打开,2表示关闭,3表示设置,4表示读取,command+48表示将数据转化为Ascall码十进制数
            senBuffer[2] = Convert.ToByte(port + 48);                         //通道，port+48表示将数据转化为Ascall码十进制数
            senBuffer[3] = b_value[0];                         //数据高位
            senBuffer[4] = b_value[1];                         //数据中位
            senBuffer[5] = b_value[2];                         //数据低位

            senBuffer = GetCheckBits(senBuffer);            //调用验证位处理函数,处理senBuffer[]最后2个验证位
            //MessageBox.Show(senBuffer[1].ToString());

            if (serialPort1.IsOpen)
            {
                serialPort1.DiscardInBuffer();               //清除串口接收缓冲区
                serialPort1.Write(senBuffer, 0, 8);          //发送指令
                string SendString = GetByteToString(senBuffer);
                ShowMesage("Send:" + SendString);
                CmdRespond(command, port, senBuffer);              //等待响应
            }
            else
            {
                MessageBox.Show("The serial port is not open");
            }
        }

        public int OutTime = 500;    //响应超时时间
        string RecString = "";
        bool CmdRespond(byte command, byte port, byte[] senBuffer)  //响应等待函数
        {
            serialPort1.ReadTimeout = OutTime;
            bool res = false;
            RecString = "";
            string SendString = GetByteToString(senBuffer);
            if (command == 1 || command == 2 || command == 3 || command==5)
            {
                try
                {
                    int reviByte = serialPort1.ReadByte();                         //指令1，2,3等待响应一个字符
                    RecString = ((char)reviByte).ToString();
                    if (RecString != "$") MessageBox.Show("Command word 123,Incorrect return value！" + RecString);
                    else res = true;
                }
                catch (Exception a)
                {
                    MessageBox.Show(SendString + "Received response" + a.Message);
                }
            }
            if (command == 4)      //读取亮度指令响应处理
            {
                try
                {
                    for (int i = 0; i < 8; i++)
                    {
                        int reviByte = serialPort1.ReadByte();                         //指令4等待响应8个字符
                        RecString += ((char)reviByte).ToString();
                        if (i == 0 && RecString == "&") break;
                    }

                    byte[] CheckRecBuffer = GetCheckBits(GetStringToByte(RecString));
                    string CheckRecStr = GetByteToString(CheckRecBuffer);
                    if (CheckRecStr == RecString)  //
                    {
                        lights[port - 1] = GetValue(RecString);
                        res = true;
                    }
                    else MessageBox.Show(RecString + "," + CheckRecStr + "Command word 4，Checksum error！");
                }
                catch (Exception b)
                {
                    MessageBox.Show(SendString + "Received response" + b.Message);
                }
            }
            if (command == 6)      //读取脉宽指令响应处理
            {
                try
                {
                    for (int i = 0; i < 8; i++)
                    {
                        int reviByte = serialPort1.ReadByte();                         //读取脉宽指令4等待响应8个字符
                        RecString += ((char)reviByte).ToString();
                        if (i == 0 && RecString == "&") break;
                    }

                    byte[] CheckRecBuffer = GetCheckBits(GetStringToByte(RecString));
                    string CheckRecStr = GetByteToString(CheckRecBuffer);
                    if (CheckRecStr == RecString)  //
                    {
                        puls[port - 1] = GetValue(RecString);
                        res = true;
                    }
                    else MessageBox.Show(RecString + "," + CheckRecStr + "Command word6，Checksum error！");
                }
                catch (Exception b)
                {
                    MessageBox.Show(SendString + "Received response" + b.Message);
                }
            }

            ShowMesage("Receive:" + RecString);
            return res;
        }
        void ShowMesage(string Mes)                                            //通讯显示函数
        {

            string ch = "\r\n";
            Mes += ch;
            textBox1.AppendText(Mes);
        }

        byte[] GetCheckBits(byte[] buffer)                      //计算校验和,传入8位，返回8位
        {
            buffer[buffer.Length - 2] = 0;                      //任何数据与0异或都等于其本身，故先对第一个（高位）验证位赋0
            for (int i = 0; i < buffer.Length - 2; i++)
            {
                buffer[buffer.Length - 2] = Convert.ToByte(buffer[buffer.Length - 2] ^ buffer[i]);           //验证位分别与前面的每个数据累计作异或运算
            }

            buffer[buffer.Length - 1] = buffer[buffer.Length - 2];   //令buffer后2位元素（即验证位）相等
            buffer[buffer.Length - 2] >>= 4;                          //高位右移4位，得到高位验证位
            buffer[buffer.Length - 1] &= 15;                         //15二进制即0000 1111，按位与可获得低位验证位

            buffer[buffer.Length - 2] = Convert.ToByte(buffer[buffer.Length - 2].ToString("X1")[0]);   //强制转化16位进制字符，再转化为Ascall码
            buffer[buffer.Length - 1] = Convert.ToByte(buffer[buffer.Length - 1].ToString("X1")[0]);

            return buffer;
        }

        //亮度设置滚动条
        private void tbLight1_ValueChanged(object sender, EventArgs e)
        {
            lights[0] = (int)tbLight1.Value;
            numLight1.Value = lights[0];                             //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button1.Text = "ON";
            button1.BackColor = Color.LimeGreen;
        }
        private void tbLight2_ValueChanged(object sender, EventArgs e)
        {
            lights[1] = (int)tbLight2.Value;
            numLight2.Value = lights[1];                             //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button2.Text = "ON";
            button2.BackColor = Color.LimeGreen;

        }
        private void tbLight3_ValueChanged(object sender, EventArgs e)
        {
            lights[2] = (int)tbLight3.Value;
            numLight3.Value = lights[2];                               //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button3.Text = "ON";
            button3.BackColor = Color.LimeGreen;

        }
        private void tbLight4_ValueChanged(object sender, EventArgs e)
        {
            lights[3] = (int)tbLight4.Value;
            numLight4.Value = lights[3];                              //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button4.Text = "ON";
            button4.BackColor = Color.LimeGreen;

        }

        //亮度设置按键+、-h或文本输入亮度
        private void numLight1_ValueChanged(object sender, EventArgs e)
        {
            lights[0] = (int)numLight1.Value;
            tbLight1.Value = lights[0];
            serialsend(3, 1, lights[0]);      ////串口通讯发送函数，命令3，通道1，亮度light
        }
        private void numLight2_ValueChanged(object sender, EventArgs e)
        {
            lights[1] = (int)numLight2.Value;
            tbLight2.Value = lights[1];
            serialsend(3, 2, lights[1]);      ////串口通讯发送函数，命令3，通道1，亮度light
        }
        private void numLight3_ValueChanged(object sender, EventArgs e)
        {
            lights[2] = (int)numLight3.Value;
            tbLight3.Value = lights[2];
            serialsend(3, 3, lights[2]);      ////串口通讯发送函数，命令3，通道1，亮度light
        }
        private void numLight4_ValueChanged(object sender, EventArgs e)
        {
            lights[3] = (int)numLight4.Value;
            tbLight4.Value = lights[3];
            serialsend(3, 4, lights[3]);      ////串口通讯发送函数，命令3，通道1，亮度light
        }

        private void button1_Click(object sender, EventArgs e)                    //通道1开关
        {
            lights[0] = (int)numLight1.Value;
            if (button1.Text == "OFF")
            {
                button1.Text = "ON";
                button1.BackColor = Color.LimeGreen;
                serialsend(1, 1, lights[0]);      //串口通讯发送函数，命令1打开通道1，亮度light
            }
            else
            {
                button1.Text = "OFF";
                button1.BackColor = Color.Red;
                serialsend(2, 1, lights[0]);      //串口通讯发送函数，命令2关闭通道1，亮度light
            }
        }
        private void button2_Click(object sender, EventArgs e)                    //通道2开关
        {
            lights[1] = (int)numLight2.Value;
            if (button2.Text == "OFF")
            {
                button2.Text = "ON";
                button2.BackColor = Color.LimeGreen;
                serialsend(1, 2, lights[1]);      //串口通讯发送函数，命令1打开通道2，亮度light
            }
            else
            {
                button2.Text = "OFF";
                button2.BackColor = Color.Red;
                serialsend(2, 2, lights[1]);      //串口通讯发送函数，命令2关闭通道2，亮度light
            }
        }

        private void button3_Click(object sender, EventArgs e)                    //通道3开关
        {
            lights[2] = (int)numLight3.Value;
            if (button3.Text == "OFF")
            {
                button3.Text = "ON";
                button3.BackColor = Color.LimeGreen;
                serialsend(1, 3, lights[2]);      //串口通讯发送函数，命令1打开通道3，亮度light
            }
            else
            {
                button3.Text = "OFF";
                button3.BackColor = Color.Red;
                serialsend(2, 3, lights[2]);      //串口通讯发送函数，命令2关闭通道3，亮度light
            }
        }
        private void button4_Click(object sender, EventArgs e)                    //通道4开关
        {
            lights[3] = (int)numLight4.Value;
            if (button4.Text == "OFF")
            {
                button4.Text = "ON";
                button4.BackColor = Color.LimeGreen;
                serialsend(1, 4, lights[3]);      //串口通讯发送函数，命令1打开通道4，亮度light
            }
            else
            {
                button4.Text = "OFF";
                button4.BackColor = Color.Red;
                serialsend(2, 4, lights[3]);      //串口通讯发送函数，命令2关闭通道4，亮度light
            }
        }

        private void button7_Click(object sender, EventArgs e)                    //退出软件按钮
        {
            this.Close();
        }

        string GetRcvData()                         //获取接收的信息，返回字符串
        {
            string str = "Receive：";                 
            foreach (byte s in rcvBuffer)
            {
                str += s.ToString();
            }
            return str;
        }
        string GetByteToString(byte[] Buffer)                                       //解码函数，返回处理后的字符串
        {
            string str = Encoding.ASCII.GetString(Buffer);                    //解码全部发送的消息
            return str;
        }
        byte[] GetStringToByte(string str)
        {
            byte[] b = Encoding.ASCII.GetBytes(str);
            return b;
        }


        int GetValue(string RecString)                                            //解码亮度函数
        {
            int value;                                                           //解码后亮度值为16进制字符表示，转化为十进制
            if (RecString.Length < 6) return 0;

            string ValueString = RecString.Substring(3, 3);
            value = Convert.ToInt32(ValueString, 16);
            return value;
        }

        string DecodeData(byte[] Buffer,int Type)                                       //解码函数，返回处理后的字符串
        {
            string str = Encoding.ASCII.GetString(Buffer);                    //解码全部发送的消息
            return str;
        }

        string GetIntString(int date)
        {
            if (date < 100 && date > 10) return "0" + date.ToString();
            if (date < 10 && date >=0 ) return "0" +"0"+ date.ToString();
            return date.ToString();
        }

        private void button5_Click(object sender, EventArgs e)                 //串口连接开关
        {
            if (serialPort1.IsOpen)
            {
                //serialPort1.DiscardInBuffer();
                //serialPort1.DiscardOutBuffer();
                serialPort1.Close();
                button5.Text = "Connect";
                tbState.Text = "Serial Port disconnected";
                butt_state(false);            //关闭按钮
            }

            else
            {
                try
                {
                    serialPort1.PortName = comBoxPorts.Text;
                    serialPort1.Open();
                    button6_Click(null,null);          //刷新函数
                }
                catch
                {
                    tbState.Text = "Serial connection failed";
                    button5.Text = "Connect";
                    butt_state(false);
                    return;
                }
                button5.Text = "Disconnect";
                tbState.Text = "Serial port is connected";
                butt_state(true);            //开启按钮
            }
        }

        void butt_state(bool state)              //按钮状态设置
        {
            tbLight1.Enabled = state;
            tbLight2.Enabled = state;
            tbLight3.Enabled = state;
            tbLight4.Enabled = state;
            numLight1.Enabled = state;
            numLight2.Enabled = state;
            numLight3.Enabled = state;
            numLight4.Enabled = state;
            button1.Enabled = state;
            button2.Enabled = state;
            button3.Enabled = state;
            button4.Enabled = state;
            button6.Enabled = state;
            label2.Enabled = state;
            label3.Enabled = state;
            label4.Enabled = state;
            label5.Enabled = state;
            label6.Enabled = state;
            label8.Enabled = state;
            label9.Enabled = state;
            label10.Enabled = state;
            label11.Enabled = state;
            label12.Enabled = state;
            label13.Enabled = state;
            label14.Enabled = state;

        }

        private void button6_Click(object sender, EventArgs e)               //刷新按钮
        {
            InitPulse();   //刷新脉宽
            for (byte i = 1; i <= 4; i++)//刷新亮度
            {
                serialsend(4, i, 0);
                if (i == 1)
                {
                    tbLight1.Value = lights[i-1];                     //该赋值语句将触发tbLight1_MouseUp事件
                    button1.Text = "ON";
                    //button1.BackColor = Color.LimeGreen;
                    button1.BackColor = Color.LimeGreen;
 
                }
                if (i == 2)
                {
                    tbLight2.Value = lights[i - 1];                       //该赋值语句将触发tbLight2_MouseUp事件
                    button2.Text = "ON";
                    button2.BackColor = Color.LimeGreen;
              
                }
                if (i == 3)
                {
                    tbLight3.Value = lights[i - 1];                       //该赋值语句将触发tbLight3_MouseUp事件
                    button3.Text = "ON";
                    button3.BackColor = Color.LimeGreen;
               
                }
                if (i == 4)
                {
                    tbLight4.Value = lights[i - 1];                       //该赋值语句将触发tbLight4_MouseUp事件
                    button4.Text = "ON";
                    button4.BackColor = Color.LimeGreen;
                } 
                
            }
            GetModel();
        }
        void InitPulse()                                                   ////刷新模式
        {
            for (byte i = 1; i <= 4; i++)
            {
                serialsend(6, i, 0);
                switch (i)
                {
                    case 1:
                        numericUpDown1.Value = puls[i-1];
                        break;
                    case 2:
                        numericUpDown2.Value = puls[i - 1];
                        break;
                    case 3:
                        numericUpDown3.Value = puls[i - 1];
                        break;
                    case 4:
                        numericUpDown4.Value = puls[i - 1];
                        break;
                }
            }   
        }

        string ch = "\r\n";
        void ShowMesage()                                            //通讯显示函数
        {
            string sBuffer = "Send：";
            string rBuffer = "Receive：";

            string str = "";
            sBuffer += DecodeData(senBuffer,10);
            rBuffer += DecodeData(rcvBuffer,10);

            if (rBuffer[0] == 0x26) { rBuffer += "Instruction execution failed！"; }
            if (rBuffer == null || rBuffer == "") { rBuffer += "No response from the controller was received"; }
            str = sBuffer + "     " + rBuffer;
            textBox1.AppendText(str);
            textBox1.AppendText(ch);
        }

        void ShowModelMesage()                                            //通讯显示函数
        {
            string sBuffer = "Send：";
            string rBuffer = "Receive：";

            string str = "";
            sBuffer += DecodeData(senModelBuffer,16);
            rBuffer += DecodeData(rcvBuffer,16);

            if (rBuffer[0] == 0x26) { rBuffer += "Instruction execution failed"; }
            if (rBuffer == null || rBuffer == "") { rBuffer += "No response from the controller was received"; }
            str = sBuffer + "     " + rBuffer;
            textBox1.AppendText(str);
            textBox1.AppendText(ch);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        byte[] senModelBuffer = new byte[8];
        void SetModel(string Model)
        {
            string str1 = "";
            if (Model == "S1") { str1 = "$S100076"; Now_Model = 1; }
            if (Model == "S2") {str1 = "$S200075";; Now_Model = 2; }
            if (Model == "S3") {str1 = "$S300074";; Now_Model = 3; }

            senModelBuffer = System.Text.Encoding.ASCII.GetBytes(str1);   //string转ASCII byte[]:
            Array.Clear(rcvBuffer, 0, rcvBuffer.Length);      //清空接收缓存区
            serialPort1.Write(senModelBuffer, 0, 8);          //发送指令
            CmdRespondModel(str1);
        }
        bool CmdRespondModel(string SendString)
        {
            serialPort1.ReadTimeout = OutTime;
            bool res = false;
            RecString = "";
            if (SendString.Substring(1, 2) == "SS" || SendString.Substring(1, 2) == "ss")    //读取模式指令接收响应
            {
                for (int i = 0; i < 8; i++)
                {
                    int reviByte = serialPort1.ReadByte();                         //指令4等待响应8个字符
                    RecString += ((char)reviByte).ToString();
                    if (i == 0 && RecString == "&") break;
                }

                byte[] CheckRecBuffer = GetCheckBits(GetStringToByte(RecString));   //接收到的字符重检验
                string CheckRecStr = GetByteToString(CheckRecBuffer);
                if (CheckRecStr == RecString)                                       //校验相等     
                {
                    switch (RecString[2])
                    {
                        case '1':
                            Now_Model = 1;                      //常亮模式
                            break;
                        case '2':
                            Now_Model = 2;                      //模式1
                            break;
                        case '3':
                            Now_Model = 3;                      //模式2
                            break;
                        default:
                            MessageBox.Show("Error in reading mode：" + RecString[2]);
                            break;
                    }
                    res = true;
                }
                else MessageBox.Show(RecString + "," + CheckRecStr + "Error in reading mode，Checksum error！");
            }
            else                                                                             //设置模式指令接收响应
            {
                try
                {
                    int reviByte = serialPort1.ReadByte();                         //指令S等待响应一个字符
                    RecString = ((char)reviByte).ToString();
                    if (RecString != "$") MessageBox.Show("Command word S,Checksum error！" + RecString);
                    else res = true;
                }
                catch (Exception a)
                {
                    MessageBox.Show(SendString + "Received response" + a.Message);
                }
            }
            ShowMesage("Receive:" + RecString);
            return res;
        }

        void GetModel()
        {
            string str1 = "$SS00014";        //读取模式
            if (serialPort1.IsOpen)
            {
                senModelBuffer = System.Text.Encoding.ASCII.GetBytes(str1);   //string转ASCII byte[]:
                Array.Clear(rcvBuffer, 0, rcvBuffer.Length);      //清空接收缓存区
                serialPort1.Write(senModelBuffer, 0, 8);          //发送指令
                CmdRespondModel(str1);
                ModelVisable();
            } 
        }
        private void button9_Click(object sender, EventArgs e)
        {
            SetModel("S1");
            ModelVisable();
        }

       /* void SetLED(int i)
        {
            if (i == 1)
            {
              //  label24.BackColor = System.Drawing.Color.White;
              //  label25.BackColor = System.Drawing.Color.Red;
              //  label26.BackColor = System.Drawing.Color.Blue;
            }
            if (i == 2)
            {
                label24.BackColor = System.Drawing.Color.White;
                label25.BackColor = System.Drawing.Color.Red;
                label26.BackColor = System.Drawing.Color.White;
            }
            if (i == 3)
            {
                label24.BackColor = System.Drawing.Color.White;
                label25.BackColor = System.Drawing.Color.White;
                label26.BackColor = System.Drawing.Color.Blue;
            }
        }*/
        private void tbLight3_Scroll(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            SetModel("S2");
            ModelVisable();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            SetModel("S3");
            ModelVisable();
        }

        void ModelVisable()
        {
           // SetLED(Now_Model);
            bool Is_visable = true;
            if (Now_Model != 3) Is_visable = false;
            numericUpDown1.Enabled = Is_visable;
            numericUpDown2.Enabled = Is_visable;
            numericUpDown3.Enabled = Is_visable;
            numericUpDown4.Enabled = Is_visable;
            label16.Enabled = Is_visable;
            label15.Enabled = Is_visable;
            label17.Enabled = Is_visable;
            label18.Enabled = Is_visable;
            label19.Enabled = Is_visable;
            label20.Enabled = Is_visable;
            label21.Enabled = Is_visable;
            label22.Enabled = Is_visable;
            label23.Enabled = Is_visable;



        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int Pulse = (int)numericUpDown1.Value;
            serialsend(5, 1, Pulse);      ////串口通讯发送函数，命令3，通道1，亮度light
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int Pulse = (int)numericUpDown2.Value;
            serialsend(5, 2, Pulse);      ////串口通讯发送函数，命令3，通道1，亮度light
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            int Pulse = (int)numericUpDown3.Value;
            serialsend(5, 3, Pulse);      ////串口通讯发送函数，命令3，通道1，亮度light
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            int Pulse = (int)numericUpDown4.Value;
            serialsend(5, 4, Pulse);      ////串口通讯发送函数，命令3，通道1，亮度light
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            int labl_value1 = 255;
            if ((string)comboBox1.SelectedItem == "999 Level") labl_value1 = 999;

            label6.Text = labl_value1.ToString();
            label8.Text = labl_value1.ToString();
            label10.Text = labl_value1.ToString();
            label13.Text = labl_value1.ToString();

            tbLight1.Maximum = labl_value1;
            tbLight2.Maximum = labl_value1;
            tbLight3.Maximum = labl_value1;
            tbLight4.Maximum = labl_value1;

            numLight1.Maximum = labl_value1;
            numLight2.Maximum = labl_value1;
            numLight3.Maximum = labl_value1;
            numLight4.Maximum = labl_value1;
        }

        private void _3代迷你控制器_SizeChanged(object sender, EventArgs e)
        {
            if (AutoSize!=null) AutoSize.FormSizeChanged();
        }
    }
}



