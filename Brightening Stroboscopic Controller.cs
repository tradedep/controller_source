using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Microsoft.Win32;
using System.Threading;
using AutoWindowsSize;


namespace HW_204N
{
    public partial class _BrighteningStroboscopicController : Form
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
        private int[] LED_PulseWidths = new int[32];                    //预设32通道脉宽
        private int[] LED_Cycle = new int[32];                    //预设32通道周期
        private int[] LED_Delay = new int[32];                    //预设32通道延时
        private int[] LED_Model = new int[32];                    //预设32通道触发模式


        /*
         *命令字 ＝ 1，2，3，4，5，6，7，8。定义分别为：
        *1：设置对应通道频闪脉宽
        *2：读出对应通道频闪脉宽
        *3：设置内触发周期
        *4：读出内触发周期
        *5：设置对应通道触发延迟时间
        *6：读出对应通道触发延迟时间
        *7：设置触发模式
        *8：读出当前触发模式
         */

        byte[] rcvBuffer = new byte[8];
        byte[] senBuffer = new byte[8];

        public _BrighteningStroboscopicController()
        {
            InitializeComponent();
            tbState.Text = "Serial port is not open";
            butt_state(false);
            flat = 0;

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";    //清空消息框
            AutoSize = new AutoAdaptWindowsSize(this);
            Array.Clear(rcvBuffer, 0, rcvBuffer.Length);
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
                    button5.Text = "Start Connection";
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
            if (command == 1|| command == 3 || command == 5 || command == 7)  //设置指令响应
            {
                try
                {
                    int reviByte = serialPort1.ReadByte();                         //指令1,3,5,7等待响应一个字符
                    RecString = ((char)reviByte).ToString();
                    if (RecString != "$") MessageBox.Show("Command word 1357,Incorrect return value！" + RecString);
                    else res = true;
                }
                catch (Exception a)
                {
                    MessageBox.Show(SendString + "Received response" + a.Message);
                }
                
            }
            if (command == 2 || command == 4 || command == 6 || command == 8)      //读取指令响应
            {
                try
                {
                    for (int i = 0; i < 8; i++)
                    {
                        int reviByte = serialPort1.ReadByte();                         //指令2，4，6，8等待响应8个字符
                        RecString += ((char)reviByte).ToString();
                        if (i == 0 && RecString == "&") break;  
                    }
                  
                    byte[] CheckRecBuffer = GetCheckBits(GetStringToByte(RecString));
                    string CheckRecStr = GetByteToString(CheckRecBuffer);
                    if (CheckRecStr == RecString)  //
                    {
                        if (command == 2) LED_PulseWidths[port - 1] = GetValue(RecString);
                        if (command == 4) LED_Cycle[port - 1] = GetValue(RecString);
                        if (command == 6) LED_Delay[port - 1] = GetValue(RecString);
                        if (command == 8) LED_Model[port - 1] = GetValue(RecString);      //0内触发，1外触发
                        res = true;
                    }
                    else MessageBox.Show(RecString + "," + CheckRecStr + "Command" + command.ToString() + "Checksum error！");
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

        
        //通道1脉宽设置按键+、-h或文本输入亮度
        private void numLight1_ValueChanged(object sender, EventArgs e)
        {
            LED_PulseWidths[0] = (int)numLight1.Value;
            tbLight1.Value = LED_PulseWidths[0];
            serialsend(1, 1, LED_PulseWidths[0]);      ////串口通讯发送函数，命令1，通道1，脉宽LED_PulseWidths[0]
        }
        private void numLight2_ValueChanged(object sender, EventArgs e)
        {
            LED_Cycle[0] = (int)numLight2.Value;
            tbLight2.Value = LED_Cycle[0];
            serialsend(3, 1, LED_Cycle[0]);      ////串口通讯发送函数，命令3，通道1，周期LED_Cycle[0]
        }
        private void numLight3_ValueChanged(object sender, EventArgs e)
        {
            LED_Delay[0] = (int)numLight3.Value;
            tbLight3.Value = LED_Delay[0];
            serialsend(5, 1, LED_Delay[0]);      ////串口通讯发送函数，命令5，通道1，延时LED_Delay[0]
        }
        //通道2脉宽设置按键+、-h或文本输入亮度
        private void numLight2_1_ValueChanged(object sender, EventArgs e)
        {
            LED_PulseWidths[1] = (int)numLight2_1.Value;
            trackBar2_1.Value = LED_PulseWidths[1];
            serialsend(1, 2, LED_PulseWidths[1]);      ////串口通讯发送函数，命令1，通道1，脉宽LED_PulseWidths[1]
        }
        private void numLight2_2_ValueChanged(object sender, EventArgs e)
        {
            LED_Cycle[1] = (int)numLight2_2.Value;
            trackBar2_2.Value = LED_Cycle[1];
            serialsend(3, 2, LED_Cycle[1]);      ////串口通讯发送函数，命令3，通道1，周期LED_Cycle[1]
        }
        private void numLight2_3_ValueChanged(object sender, EventArgs e)
        {
            LED_Delay[1] = (int)numLight2_3.Value;
            trackBar2_3.Value = LED_Delay[1];
            serialsend(5, 2, LED_Delay[1]);      ////串口通讯发送函数，命令5，通道1，延时LED_Delay[1]
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
                button5.Text = "Start Connection";
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
                    button5.Text = "Start Connection";
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
            groupBox1.Enabled = state;
            groupBox3.Enabled = state;

        }

        private void button6_Click(object sender, EventArgs e)               //刷新按钮
        {

            for (byte i=1; i <= 2;i++)
            { 
                serialsend(2, i, 0);
                serialsend(4, i, 0);
                serialsend(6, i, 0);
                serialsend(8, i, 0);
                if (i == 1)
                {
                    tbLight1.Value = LED_PulseWidths[i-1];                  //该赋值语句将触发tbLight1_MouseUp事件
                    tbLight2.Value = LED_Cycle[i - 1];                      //该赋值语句将触发tbLight1_MouseUp事件
                    tbLight3.Value = LED_Delay[i - 1];                      //该赋值语句将触发tbLight1_MouseUp事件
                    if (LED_Model[0] == 0) radioButton2.Checked = true;
                    else radioButton1.Checked = true;

                    numLight1.Value = LED_PulseWidths[i - 1];
                    numLight2.Value = LED_Cycle[i - 1];
                    numLight3.Value = LED_Delay[i - 1];  
                }
                if (i == 2)
                {
                    trackBar2_1.Value = LED_PulseWidths[i - 1];                  //该赋值语句将触发tbLight1_MouseUp事件
                    trackBar2_2.Value = LED_Cycle[i - 1];                      //该赋值语句将触发tbLight1_MouseUp事件
                    trackBar2_3.Value = LED_Delay[i - 1];                      //该赋值语句将触发tbLight1_MouseUp事件
                    if (LED_Model[1] == 0) radioButton4.Checked = true;
                    else radioButton3.Checked = true;

                    numLight2_1.Value = LED_PulseWidths[i - 1];
                    numLight2_2.Value = LED_Cycle[i - 1];
                    numLight2_3.Value = LED_Delay[i - 1]; 
                }
            } 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        //触发选择
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                LED_Model[0] = 0;  //通道1内触发
                serialsend(7, 1, LED_Model[0]);      ////串口通讯发送函数，命令7，通道1
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                LED_Model[0] = 1;  //通道1外触发
                serialsend(7, 1, LED_Model[0]);      ////串口通讯发送函数，命令7，通道1
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                LED_Model[1] = 0;  //通道2内触发
                serialsend(7, 2, LED_Model[1]);      ////串口通讯发送函数，命令7，通道2
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                LED_Model[1] = 1;  //通道2内触发
                serialsend(7, 2, LED_Model[1]);      ////串口通讯发送函数，命令7，通道2
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            AutoSize.FormSizeChanged();
        }

        private void tbLight1_MouseUp(object sender, MouseEventArgs e)
        {
            LED_PulseWidths[0] = (int)tbLight1.Value;
            numLight1.Value = LED_PulseWidths[0];                             //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
        }

        private void tbLight2_MouseUp(object sender, MouseEventArgs e)
        {
            LED_Cycle[0] = (int)tbLight2.Value;
            numLight2.Value = LED_Cycle[0];
        }
        private void tbLight3_MouseUp(object sender, MouseEventArgs e)
        {
            LED_Delay[0] = (int)tbLight3.Value;
            numLight3.Value = LED_Delay[0];
        }

        private void trackBar2_1_MouseUp(object sender, MouseEventArgs e)
        {
            LED_PulseWidths[1] = (int)trackBar2_1.Value;
            numLight2_1.Value = LED_PulseWidths[1];
        }

        private void trackBar2_2_MouseUp(object sender, MouseEventArgs e)
        {
            LED_Cycle[1] = (int)trackBar2_2.Value;
            numLight2_2.Value = LED_Cycle[1];
        }

        private void trackBar2_3_MouseUp(object sender, MouseEventArgs e)
        {
            LED_Delay[1] = (int)trackBar2_3.Value;
            numLight2_3.Value = LED_Delay[1];
        }
    }
}



