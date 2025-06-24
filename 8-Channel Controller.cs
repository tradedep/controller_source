using System;
using System.Collections.Generic;
using System.ComponentModel;
using AutoWindowsSize;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using LightController;


namespace HW_204N
{
    public partial class _8ChannelController : Form
    {
        int flat;
        AutoAdaptWindowsSize AutoSize;
        //private bool m_CH340Flag = false;//CH340驱动存在标志位
        //private string[] comNames = null;
        private int m_comBoxNums = 0;
        private string m_LastComPort = null;
        //private bool m_ComOpened = false;
       //private bool m_ComRcvOk = false;
        private int[] lights = new int[32];                    //预设32通道亮度

        //double value;
       // int sleepTime;

        byte[] rcvBuffer = new byte[8];
        byte[] senBuffer = new byte[8];

        public _8ChannelController()
        {
            InitializeComponent();
            tbState.Text = "Serial Port is not open";
            butt_state(false);
            flat = 0;
            //sleepTime = 50;               //通讯等待时间

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";    //清空消息框
            Array.Clear(rcvBuffer, 0, rcvBuffer.Length);
            AutoSize = new AutoAdaptWindowsSize(this);
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
                    button105.Text = "Disconnect";
                    butt_state(true);
                    button6_Click(null, null);          //刷新函数

                }
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                {
                    tbState.Text = "Serial Port opening failed";
                    button105.Text = "Connect";
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
        private void serialsend(byte command,byte port,int value)
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
                CmdRespond(command, port,senBuffer);              //等待响应
            }
            else
            {
                MessageBox.Show("The serial port is not open");
            }
        }

        public int OutTime = 500;    //响应超时时间
        string RecString = "";
        bool CmdRespond(byte command, byte port,byte[] senBuffer)  //响应等待函数
        {
            serialPort1.ReadTimeout = OutTime;
            bool res = false;
            RecString = "";
            string SendString = GetByteToString(senBuffer);
            if (command == 1 || command == 2 || command == 3)
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
            ShowMesage("Receive:" + RecString);
            return res;
        }

        void ShowMesage(string Mes)                                            //通讯显示函数
        {
            
            string ch = "\r\n";
            Mes += ch;
            //sBuffer += DecodeData(senBuffer);
            //rBuffer += DecodeData(rcvBuffer);
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
            if (button1.Text=="OFF")
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
            byte value1 = ValueChangeTo10(RecString[3].ToString()[0]);                 //进制转换函数
            byte value2 = ValueChangeTo10(RecString[4].ToString()[0]);
            byte value3 = ValueChangeTo10(RecString[5].ToString()[0]);
            value = value1 * 16 * 16 + value2 * 16 + value3;
            return value;
        }

        byte ValueChangeTo10(char ch)   //16进制转10进制，个位转换函数
        {
            byte result=16;             //失败返回16，成功返回0-15
            switch (ch)
            {
                case '0':
                    result= 0;
                    break;
                case '1':
                    result= 1;
                    break;
                case '2':
                    result= 2;
                    break;
                case '3':
                    result= 3;
                    break;

                case '4':
                    result= 4;
                    break;
                 case '5':
                    result= 5;
                    break;
                case '6':
                    result= 6;
                    break;
                case '7':
                    result= 7;
                    break;

                case '8':
                    result= 8;
                    break;
                case '9':
                    result= 9;
                    break;
                case 'A':
                    result= 10;
                    break;
                case 'B':
                    result= 11;
                    break;

                case 'C':
                    result= 12;
                    break;
                 case 'D':
                    result= 13;
                    break;
                case 'E':
                    result= 14;
                    break;
                case 'F':
                    result= 15;
                    break;
             }

            return result;

        }
        private void button5_Click(object sender, EventArgs e)                 //串口连接开关
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                button105.Text = "Connect";
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
                    button105.Text = "Connect";
                    butt_state(false);
                    //return;
                }
                button105.Text = "Disconnect";
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
            tbLight5.Enabled = state;
            tbLight6.Enabled = state;
            tbLight7.Enabled = state;
            tbLight8.Enabled = state;
            numLight1.Enabled = state;
            numLight2.Enabled = state;
            numLight3.Enabled = state;
            numLight4.Enabled = state;
            numLight5.Enabled = state;
            numLight6.Enabled = state;
            numLight7.Enabled = state;
            numLight8.Enabled = state;
            button1.Enabled = state;
            button2.Enabled = state;
            button3.Enabled = state;
            button4.Enabled = state;
            button5.Enabled = state;
            button6.Enabled = state;
            button7.Enabled = state;
            button8.Enabled = state;
            button106.Enabled = state;
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
            label15.Enabled = state;
            label16.Enabled = state;
            label17.Enabled = state;
            label18.Enabled = state;
            label19.Enabled = state;
            label20.Enabled = state;
            label2.Enabled = state;
            label21.Enabled = state;
            label22.Enabled = state;

            label26.Enabled = state;
            label25.Enabled = state;
            label24.Enabled = state;
            label23.Enabled = state;



        }

        private void button6_Click(object sender, EventArgs e)               //刷新按钮
        {
            
            for (byte i=1; i <= 8;i++)
            { 
                serialsend(4, i, 0);
                if (i == 1)
                {
                    tbLight1.Value = lights[i-1];                      //该赋值语句将触发tbLight1_MouseUp事件
                    button1.Text = "ON";
                    //button1.BackColor = Color.LimeGreen;
                    button1.BackColor = Color.LimeGreen;
 
                }
                if (i == 2)
                {
                    tbLight2.Value = lights[i - 1];                      //该赋值语句将触发tbLight2_MouseUp事件
                    button2.Text = "ON";
                    button2.BackColor = Color.LimeGreen;
              
                }
                if (i == 3)
                {
                    tbLight3.Value = lights[i - 1];                      //该赋值语句将触发tbLight3_MouseUp事件
                    button3.Text = "ON";
                    button3.BackColor = Color.LimeGreen;
               
                }
                if (i == 4)
                {
                    tbLight4.Value = lights[i - 1];                      //该赋值语句将触发tbLight4_MouseUp事件
                    button4.Text = "ON";
                    button4.BackColor = Color.LimeGreen;
                }
                if (i == 5)
                {
                    tbLight5.Value = lights[i - 1];                      //该赋值语句将触发tbLight4_MouseUp事件
                    button5.Text = "ON";
                    button5.BackColor = Color.LimeGreen;
                }
                if (i == 6)
                {
                    tbLight6.Value = lights[i - 1];                      //该赋值语句将触发tbLight4_MouseUp事件
                    button6.Text = "ON";
                    button6.BackColor = Color.LimeGreen;
                }
                if (i == 7)
                {
                    tbLight7.Value = lights[i - 1];                      //该赋值语句将触发tbLight4_MouseUp事件
                    button7.Text = "ON";
                    button7.BackColor = Color.LimeGreen;
                }
                if (i == 8)
                {
                    tbLight8.Value = lights[i - 1];                      //该赋值语句将触发tbLight4_MouseUp事件
                    button8.Text = "ON";
                    button8.BackColor = Color.LimeGreen;
                } 
            } 
        }

        

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void tbLight5_ValueChanged(object sender, EventArgs e)
        {
            lights[4] = (int)tbLight5.Value;
            numLight5.Value = lights[4];                              //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button5.Text = "ON";
            button5.BackColor = Color.LimeGreen;
        }

        private void tbLight6_ValueChanged(object sender, EventArgs e)
        {
            lights[5] = (int)tbLight6.Value;
            numLight6.Value = lights[5];                              //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button6.Text = "ON";
            button6.BackColor = Color.LimeGreen;
        }

        private void tbLight7_ValueChanged(object sender, EventArgs e)
        {
            lights[6] = (int)tbLight7.Value;
            numLight7.Value = lights[6];                              //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button7.Text = "ON";
            button7.BackColor = Color.LimeGreen;
        }

        private void tbLight8_ValueChanged(object sender, EventArgs e)
        {
            lights[7] = (int)tbLight8.Value;
            numLight8.Value = lights[7];                              //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button8.Text = "ON";
            button8.BackColor = Color.LimeGreen;
        }

        private void numLight5_ValueChanged(object sender, EventArgs e)
        {
            byte i = 5;
            lights[i-1] = (int)numLight5.Value;
            tbLight5.Value = lights[i-1];
            serialsend(3, i, lights[i-1]);      ////串口通讯发送函数，命令3，通道1，亮度light
        }

        private void numLight6_ValueChanged(object sender, EventArgs e)
        {
            byte i = 6;
            lights[i - 1] = (int)numLight6.Value;
            tbLight6.Value = lights[i - 1];
            serialsend(3, i, lights[i - 1]);      ////串口通讯发送函数，命令3，通道1，亮度light
        }

        private void numLight7_ValueChanged(object sender, EventArgs e)
        {
            byte i = 7;
            lights[i - 1] = (int)numLight7.Value;
            tbLight7.Value = lights[i - 1];
            serialsend(3, i, lights[i - 1]);      ////串口通讯发送函数，命令3，通道1，亮度light
        }

        private void numLight8_ValueChanged(object sender, EventArgs e)
        {
            byte i = 8;
            lights[i - 1] = (int)numLight8.Value;
            tbLight8.Value = lights[i - 1];
            serialsend(3, i, lights[i - 1]);      ////串口通讯发送函数，命令3，通道1，亮度light
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            byte i = 5;
            lights[i-1] = (int)numLight5.Value;
            if (button5.Text == "OFF")
            {
                button5.Text = "ON";
                button5.BackColor = Color.LimeGreen;
                serialsend(1, i, lights[i-1]);      //串口通讯发送函数，命令1打开通道4，亮度light

            }
            else
            {
                button5.Text = "OFF";
                button5.BackColor = Color.Red;
                serialsend(2, i, lights[i-1]);      //串口通讯发送函数，命令2关闭通道4，亮度light
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            byte i = 6;
            lights[i - 1] = (int)numLight6.Value;
            if (button6.Text == "OFF")
            {
                button6.Text = "ON";
                button6.BackColor = Color.LimeGreen;
                serialsend(1, i, lights[i - 1]);      //串口通讯发送函数，命令1打开通道4，亮度light

            }
            else
            {
                button6.Text = "OFF";
                button6.BackColor = Color.Red;
                serialsend(2, i, lights[i - 1]);      //串口通讯发送函数，命令2关闭通道4，亮度light
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            byte i = 7;
            lights[i - 1] = (int)numLight7.Value;
            if (button7.Text == "OFF")
            {
                button7.Text = "ON";
                button7.BackColor = Color.LimeGreen;
                serialsend(1, i, lights[i - 1]);      //串口通讯发送函数，命令1打开通道4，亮度light

            }
            else
            {
                button7.Text = "OFF";
                button7.BackColor = Color.Red;
                serialsend(2, i, lights[i - 1]);      //串口通讯发送函数，命令2关闭通道4，亮度light
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            byte i = 8;
            lights[i - 1] = (int)numLight8.Value;
            if (button8.Text == "OFF")
            {
                button8.Text = "ON";
                button8.BackColor = Color.LimeGreen;
                serialsend(1, i, lights[i - 1]);      //串口通讯发送函数，命令1打开通道4，亮度light

            }
            else
            {
                button8.Text = "OFF";
                button8.BackColor = Color.Red;
                serialsend(2, i, lights[i - 1]);      //串口通讯发送函数，命令2关闭通道4，亮度light
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            AutoSize.FormSizeChanged();
        }
    }
}



