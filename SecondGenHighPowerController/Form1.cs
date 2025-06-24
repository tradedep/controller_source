using System;
using System.Collections.Generic;
using System.ComponentModel;
using AutoWindowsSize;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Microsoft.Win32;
using System.Threading;
using LightController;
using System.Diagnostics;
using System.Net.Sockets;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HW_204N
{
    public partial class Form1 : Form
    {
        //int flat;
        AutoAdaptWindowsSize AutoSize;
        //private bool m_CH340Flag = false;//CH340驱动存在标志位
        //private string[] comNames = null;
        //private int m_comBoxNums = 0;

        private int[] lights = new int[32];                   //预设32通道亮度
        private int[] Power = new int[32];                    //预设32通道功率
        private int[] Pulse = new int[32];                    //预设32通道脉宽
        private int Polarity = 1;                             //极性，1-高电平触发，2-低电平触发，3-上升沿，4-下降沿
        private int Detection = 0;                            //自动侦测功能，关闭，1开启
        int Now_Model = 1;                                    //当前模式
                
        byte[] rcvBuffer = new byte[8];
        byte[] senBuffer = new byte[8];

        private CommMode e_commmode;
        private CHA1919Manger m_ch1919Manger;
        private TcpClient client;
        private bool isReceiving = true;
        private Thread thread = null;
        private object lockobj = new object();
        private ManualResetEvent m_manualResetEvent = new ManualResetEvent(false);
        List<byte> bytesRead;

        public Form1()
        {
            InitializeComponent();
            AutoSize = new AutoAdaptWindowsSize(this);
            tbState.Text = "网口未打开";
            butt_state(false);
            //flat = 0;
            Control.CheckForIllegalCrossThreadCalls = false;
            rdb_socket.Checked = true;
            e_commmode = CommMode.网口;
            m_ch1919Manger = new CHA1919Manger();
            client = new TcpClient();
        }

        private void Receive()
        {
            isReceiving = true;
            while (isReceiving)
            {
                try
                {
                    if (client.Connected)
                    {
                        byte[] buffer = new byte[32];
                        NetworkStream stream = client.GetStream();

                        int num = stream.Read(buffer, 0, buffer.Length);
                        if (num != 0)
                        {
                            bytesRead = buffer.ToList().Skip(0).Take(num).ToList();
                            m_manualResetEvent.Set();
                        }
                    }
                }
                catch (ThreadAbortException)
                {

                }
                catch (SocketException)
                {

                }
                catch (Exception)
                {

                }
            }
        }

        private int SocketRead()
        {
            byte b = bytesRead[0];
            bytesRead.RemoveAt(0);
            return Convert.ToInt32(b);
        }

        private void SocketWrite(byte[] bytes)
        {
            if (client.Connected)
            {
                NetworkStream stream = client.GetStream();
                m_manualResetEvent.Reset();
                // m_manualResetEvent.Set();
                stream.Write(bytes, 0, bytes.Length);
                m_manualResetEvent.WaitOne(250);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";    //清空消息框
            Array.Clear(rcvBuffer, 0, rcvBuffer.Length);
            rdb_port.Checked = true;
            rdb_socket.Checked = false;
        }

        #region 定时刷新串口，获取端口号
        private void timer1_Tick(object sender, EventArgs e)
        {
            //string Cur_PortName = comBoxPorts.Text;

            //try
            //{
            //    string[] strName = null;
            //    strName = SerialPort.GetPortNames();

            //    if (m_comBoxNums != strName.Length)
            //    {
            //        m_comBoxNums = strName.Length;

            //        comBoxPorts.Items.Clear();
            //        comBoxPorts.Text = null;
            //        tbState.Text = "串口未打开";

            //        for (int i = 0; i < m_comBoxNums; i++)                              //判断串口刷新后状态
            //        {
            //            if (strName[i] == Cur_PortName) { comBoxPorts.Text = Cur_PortName; tbState.Text = "串口打开成功"; }
            //            comBoxPorts.Items.Add(strName[i]);
            //        }
            //        comBoxPorts.Text = strName[0];
            //        serialPort1.PortName = comBoxPorts.Text;
            //    }
            //    flat = 1;
            //}

            //catch (Exception ex)
            //{
            //    if (ex.Source != null)
            //    {
            //    }
            //}
        }

        #endregion



        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
        }

        private void comBoxPorts_SelectedValueChanged(object sender, EventArgs e)
        {
            if (e_commmode == CommMode.网口)
            {

                try
                {
                    m_ch1919Manger.curInfo = m_ch1919Manger.localIpInfoLst.FirstOrDefault(p => p.mac.Trim() == comBoxPorts.Text.Trim());
                    if (m_ch1919Manger.curInfo == null)
                    {
                        return;
                    }
                    if (client != null && client.Client != null && client.Connected)
                    {
                        isReceiving = false;
                        client.Close();
                        thread.Abort();
                        button5.Text = "启动连接";
                        tbState.Text = "网口已断开";
                        butt_state(false);            //关闭按钮
                    }
                    byte[] bytes = HexStringToByte(m_ch1919Manger.curInfo.mac);
                    m_ch1919Manger.ReadConfig(bytes);

                    //string ip =
                    //m_ch1919Manger.curDeviceConfig.bDevIP[0].ToString("000") + "." +
                    //m_ch1919Manger.curDeviceConfig.bDevIP[1].ToString("000") + "." +
                    //m_ch1919Manger.curDeviceConfig.bDevIP[2].ToString("000") + "." +
                    //m_ch1919Manger.curDeviceConfig.bDevIP[3].ToString("000");
                    //mreTimeOut.Reset();
                    //client = new TcpClient();
                    //client.BeginConnect(IPAddress.Parse(ip), Convert.ToInt32(m_ch1919Manger.curProtConfig2.wNetPort) , ConnectCallback, null) ;
                    //mreTimeOut.WaitOne(3000);
                    //if (client.Connected)
                    //{
                    //    button5.Text = "断开连接";
                    //    tbState.Text = "网口已连接";
                    //    butt_state(true);            //开启按钮
                    //    thread = new Thread(Receive);
                    //    thread.Start();
                    //    button6_Click(null, null);          //刷新函数
                    //}
                }
                catch
                {
                    tbState.Text = "网口连接失败";
                    button5.Text = "启动连接";
                    butt_state(false);
                    return;
                }
            }
            else
            {

                try
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Close();
                        button5.Text = "启动连接";
                        tbState.Text = "串口已断开";
                        butt_state(false);            //关闭按钮
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Source != null)
                    {
                        tbState.Text = "串口打开失败";
                        button5.Text = "启动连接";
                        butt_state(false);
                        return;
                    }
                }
            }


        }
        //窗体关闭
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (client != null && client.Client != null)
                {
                    client.Client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    thread.Abort();
                    isReceiving = false;
                }

                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    tbState.Text = "串口未打开";
                }
                m_ch1919Manger.DisPose();
            }
            catch (Exception ex)
            {
                try
                { m_ch1919Manger.DisPose(); }
                catch (Exception)
                {

                }

                if (ex.Source != null)
                {

                }
            }
            finally
            {

            }

        }

        public void SendStringCMD(string CMDstr)         //16进制指令控制，长度要求为8
        {
            if (serialPort1.IsOpen)
            {
                Array.Clear(rcvBuffer, 0, rcvBuffer.Length);     //清空接收缓存区
                byte[] returnBytes = ToBytesFromHexString(CMDstr);
                serialPort1.Write(returnBytes, 0, 8);               //发送指令
                CmdRespond(Convert.ToByte(CMDstr[1]), Convert.ToByte(CMDstr[2]), returnBytes);              //等待响应
            }
            else
            {
                MessageBox.Show("串口没有打开");
            }
        }
        //private byte[] HexStringToByte(string hs)
        //{
        //    string strTemp = "";
        //    byte[] b = new byte[hs.Length / 2];
        //    for (int i = 0; i < hs.Length / 2; i++)
        //    {
        //        strTemp = hs.Substring(i * 2, 2);
        //        b[i] = Convert.ToByte(strTemp, 16);
        //    }
        //    //按照指定编码将字节数组变为字符串
        //    return b;
        //}

        public byte[] ToBytesFromHexString(string hexString)
        {
            //以 ' ' 分割字符串，并去掉空字符
            string[] chars = hexString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                returnBytes[i] = (byte)(int)chars[i][0];
            }
            return returnBytes;
        }

        private void CmdSend(string StrCommand)  //功能码指令控制          //模式指令接口
        {
            byte[] senModelBuffer = System.Text.Encoding.ASCII.GetBytes(StrCommand);   //string转ASCII byte[]:
            switch (e_commmode)
            {
                case CommMode.串口:
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.DiscardInBuffer();               //清除串口接收缓冲区
                        serialPort1.Write(senModelBuffer, 0, 8);          //发送指令
                        CmdRespond2(StrCommand);
                    }
                    else ShowMesage("串口未打开 " + StrCommand);
                    
                    break;
                case CommMode.网口:
                    if (client.Connected)
                    {
                        SocketWrite(senModelBuffer);          //发送指令
                        CmdRespond2(StrCommand);
                    }
                    else ShowMesage("网口未打开 " + StrCommand);
                    break;
            }
        }

        bool CmdRespond2(string StrCommand)
        {
            serialPort1.ReadTimeout = OutTime;
            bool res = false;
            RecString = "";
            if (StrCommand.Substring(1, 2) == "SS" || StrCommand.Substring(1, 2) == "ss")    //读取模式指令接收响应
            {
                for (int i = 0; i < 8; i++)
                {
                    int reviByte;
                    if (e_commmode == CommMode.网口)
                    {
                        reviByte = SocketRead();
                    }
                    else
                    {
                        serialPort1.ReadTimeout = OutTime;
                        reviByte = serialPort1.ReadByte();
                    }
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
                            MessageBox.Show("读取模式SS返回模式有误：" + RecString[2]);
                            break;
                    }
                    res = true;
                }
                else MessageBox.Show(RecString + "," + CheckRecStr + "读取模式指令接收响应SS，校验和有误！");
            }
            else                                                                             //设置模式指令接收响应
            {
                try
                {
                    int reviByte;                         //指令S等待响应一个字符
                    if (e_commmode == CommMode.网口)
                    {
                        reviByte = SocketRead();
                    }
                    else
                    {
                        serialPort1.ReadTimeout = OutTime;
                        reviByte = serialPort1.ReadByte();
                    }
                    RecString = ((char)reviByte).ToString();
                    if (RecString != "$") MessageBox.Show("命令字S,返回值有误！" + RecString);
                    else res = true;
                }
                catch (Exception a)
                {
                    MessageBox.Show(StrCommand + "接收响应" + a.Message);
                }
            }
            ShowMesage("接收:" + RecString);
            return res;
        }

        //command 命令  port 通道  value数据值
        private void CmdSend(string command, byte port, int value)  //功能码指令控制
        {
            byte Command_int = (byte)(int)command[0];

            byte[] b_value = new byte[3];              //亮度值用3位字节表示

            byte[] b_check = new byte[2];              //校验位用2位字表示

            string str1 = value.ToString("X3");        //亮度值处理,将value转化为3位的16进制字符
            b_value[0] = Convert.ToByte(str1[0]);      //将数据转化为Ascall码十进制数
            b_value[1] = Convert.ToByte(str1[1]);      //将数据转化为Ascall码十进制数
            b_value[2] = Convert.ToByte(str1[2]);      //将数据转化为Ascall码十进制数

            //MessageBox.Show(b_value[0].ToString() + " " + b_value[1].ToString() + " " + b_value[2].ToString());

            senBuffer[0] = 0x24;                                //特征字$
            //等价于senBuffer[0] =36; 
            senBuffer[1] = Command_int;                      //命令1表示打开,2表示关闭,3表示设置,4表示读取,command+48表示将数据转化为Ascall码十进制数
            senBuffer[2] = Convert.ToByte(port + 48);                         //通道，port+48表示将数据转化为Ascall码十进制数
            senBuffer[3] = b_value[0];                         //数据高位
            senBuffer[4] = b_value[1];                         //数据中位
            senBuffer[5] = b_value[2];                         //数据低位

            senBuffer = GetCheckBits(senBuffer);            //调用验证位处理函数,处理senBuffer[]最后2个验证位

            switch (e_commmode)
            {
                case CommMode.串口:
                    {
                        if (serialPort1.IsOpen)
                        {
                            serialPort1.DiscardInBuffer();               //清除串口接收缓冲区
                            serialPort1.Write(senBuffer, 0, 8);          //发送指令
                            string SendString = GetByteToString(senBuffer);
                            ShowMesage("发送:" + SendString);
                            CmdRespond(Command_int, port, senBuffer);              //等待响应
                        }
                        else
                        {
                            MessageBox.Show("串口没有打开");
                        }
                        break;
                    }

                case CommMode.网口:
                    {
                        if (client.Connected)
                        {

                            SocketWrite(senBuffer);          //发送指令
                            string SendString = GetByteToString(senBuffer);
                            ShowMesage("发送:" + SendString);
                            CmdRespond(Command_int, port, senBuffer);              //等待响应
                        }
                        else
                        {
                            MessageBox.Show("网口没有打开");
                        }
                        break;
                    }



                default:
                    { break; }
            }
        }

        public int OutTime = 500;    //响应超时时间
        string RecString = "";
        bool CmdRespond(byte command, byte port, byte[] senBuffer)  //响应等待函数
        {

            bool res = false;
            RecString = "";
            string SendString = GetByteToString(senBuffer);
            if (command == '1' || command == '2' || command == '3' || command == '5' || command == '7' || command == '9' || command == 'A' || command == 'a')
            {
                try
                {
                    int reviByte = 0;
                    if (e_commmode == CommMode.网口)
                    {
                        reviByte = SocketRead();
                    }
                    else
                    {
                        serialPort1.ReadTimeout = OutTime;
                        reviByte = serialPort1.ReadByte();
                    }
                    //指令1，2,3等待响应一个字符
                    RecString = ((char)reviByte).ToString();
                    if (RecString != "$")
                        MessageBox.Show("命令字" + ((char)command).ToString() + ",返回值有误！" + RecString);
                    else res = true;
                }
                catch (Exception a)
                {
                    MessageBox.Show(SendString + "接收响应" + a.Message);
                }
            }
            if (command == '0' || command == '4' || command == '6' || command == '8' || command == 'B' || command == 'b')      //读取指令响应处理
            {
                try
                {
                    for (int i = 0; i < 8; i++)
                    {
                        int reviByte = 0;
                        if (e_commmode == CommMode.网口)
                        {
                            reviByte = SocketRead();
                        }
                        else
                        {
                            serialPort1.ReadTimeout = OutTime;
                            reviByte = serialPort1.ReadByte();
                        }
                        //指令4等待响应8个字符
                        RecString += ((char)reviByte).ToString();
                        if (i == 0 && RecString == "&") break;
                    }
                    byte[] CheckRecBuffer = GetCheckBits(GetStringToByte(RecString));
                    string CheckRecStr = GetByteToString(CheckRecBuffer);
                    if (CheckRecStr == RecString)  //
                    {
                        res = true;
                        if (command == '4') lights[port - 1] = GetValue(RecString);
                        if (command == '6') Pulse[port - 1] = GetValue(RecString);             //指令6，查询脉宽
                        if (command == '8') Polarity = GetValue(RecString);
                        //if (command == '0') Pulse[port - 1] = GetValue(RecString);
                        if (command == 'B' || command == 'b') Detection = GetValue(RecString);
                    }
                    else MessageBox.Show(RecString + "," + CheckRecStr + "__命令字" + command.ToString() + ",返回值有误！");
                }
                catch (Exception b)
                {
                    MessageBox.Show(SendString + "接收响应" + b.Message);
                }
            }

            ShowMesage("接收:" + RecString);
            return res;
        }

        void ShowMesage(string Mes)                                            //通讯显示函数
        {

            Mes = "【" + DateTime.Now.ToString("HH:mm:ss") + "】" + Mes;
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
            button1.Text = "打开";
            button1.BackColor = Color.LimeGreen;
        }
        private void tbLight2_ValueChanged(object sender, EventArgs e)
        {
            lights[1] = (int)tbLight2.Value;
            numLight2.Value = lights[1];                             //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button2.Text = "打开";
            button2.BackColor = Color.LimeGreen;

        }
        private void tbLight3_ValueChanged(object sender, EventArgs e)
        {
            lights[2] = (int)tbLight3.Value;
            numLight3.Value = lights[2];                               //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button3.Text = "打开";
            button3.BackColor = Color.LimeGreen;

        }
        private void tbLight4_ValueChanged(object sender, EventArgs e)
        {
            lights[3] = (int)tbLight4.Value;
            numLight4.Value = lights[3];                              //该赋值语句将触发numLight1_ValueChanged（）文本改变事件
            button4.Text = "打开";
            button4.BackColor = Color.LimeGreen;

        }

        //亮度设置按键+、-h或文本输入亮度
        private void numLight1_ValueChanged(object sender, EventArgs e)
        {
            lights[0] = (int)numLight1.Value;
            tbLight1.Value = lights[0];
            CmdSend("3", 1, lights[0]);      ////串口通讯发送函数，命令3，通道1，亮度light
            Array.Clear(senBuffer, 0, senBuffer.Length);     //清空发送缓存区
            Array.Clear(rcvBuffer, 0, rcvBuffer.Length);     //清空接收缓存区
        }
        private void numLight2_ValueChanged(object sender, EventArgs e)
        {
            lights[1] = (int)numLight2.Value;
            tbLight2.Value = lights[1];
            CmdSend("3", 2, lights[1]);      ////串口通讯发送函数，命令3，通道1，亮度light
            Array.Clear(senBuffer, 0, senBuffer.Length);     //清空发送缓存区
            Array.Clear(rcvBuffer, 0, rcvBuffer.Length);     //清空接收缓存区
        }
        private void numLight3_ValueChanged(object sender, EventArgs e)
        {
            lights[2] = (int)numLight3.Value;
            tbLight3.Value = lights[2];
            CmdSend("3", 3, lights[2]);      ////串口通讯发送函数，命令3，通道1，亮度light
            Array.Clear(senBuffer, 0, senBuffer.Length);     //清空发送缓存区
            Array.Clear(rcvBuffer, 0, rcvBuffer.Length);     //清空接收缓存区
        }
        private void numLight4_ValueChanged(object sender, EventArgs e)
        {
            lights[3] = (int)numLight4.Value;
            tbLight4.Value = lights[3];
            CmdSend("3", 4, lights[3]);      ////串口通讯发送函数，命令3，通道1，亮度light
            Array.Clear(senBuffer, 0, senBuffer.Length);     //清空发送缓存区
            Array.Clear(rcvBuffer, 0, rcvBuffer.Length);     //清空接收缓存区
        }

        private void button1_Click(object sender, EventArgs e)                    //通道1开关
        {
            lights[0] = (int)numLight1.Value;
            if (button1.Text == "关闭")
            {
                button1.Text = "打开";
                button1.BackColor = Color.LimeGreen;
                CmdSend("1", 1, lights[0]);      //串口通讯发送函数，命令1打开通道1，亮度light
            }
            else
            {
                button1.Text = "关闭";
                button1.BackColor = Color.Red;
                CmdSend("2", 1, lights[0]);      //串口通讯发送函数，命令2关闭通道1，亮度light
            }
        }
        private void button2_Click(object sender, EventArgs e)                    //通道2开关
        {
            lights[1] = (int)numLight2.Value;
            if (button2.Text == "关闭")
            {
                button2.Text = "打开";
                button2.BackColor = Color.LimeGreen;
                CmdSend("1", 2, lights[1]);      //串口通讯发送函数，命令1打开通道2，亮度light
            }
            else
            {
                button2.Text = "关闭";
                button2.BackColor = Color.Red;
                CmdSend("2", 2, lights[1]);      //串口通讯发送函数，命令2关闭通道2，亮度light
            }
        }
        private void button3_Click(object sender, EventArgs e)                    //通道3开关
        {
            lights[2] = (int)numLight3.Value;
            if (button3.Text == "关闭")
            {
                button3.Text = "打开";
                button3.BackColor = Color.LimeGreen;
                CmdSend("1", 3, lights[2]);      //串口通讯发送函数，命令1打开通道3，亮度light
                Array.Clear(rcvBuffer, 0, rcvBuffer.Length);     //清空接收缓存区
            }
            else
            {
                button3.Text = "关闭";
                button3.BackColor = Color.Red;
                CmdSend("2", 3, lights[2]);      //串口通讯发送函数，命令2关闭通道3，亮度light
                Array.Clear(rcvBuffer, 0, rcvBuffer.Length);     //清空接收缓存区
            }
        }
        private void button4_Click(object sender, EventArgs e)                    //通道4开关
        {
            lights[3] = (int)numLight4.Value;
            if (button4.Text == "关闭")
            {
                button4.Text = "打开";
                button4.BackColor = Color.LimeGreen;
                CmdSend("1", 4, lights[3]);      //串口通讯发送函数，命令1打开通道4，亮度light
                Array.Clear(rcvBuffer, 0, rcvBuffer.Length);     //清空接收缓存区
            }
            else
            {
                button4.Text = "关闭";
                button4.BackColor = Color.Red;
                CmdSend("2", 4, lights[3]);      //串口通讯发送函数，命令2关闭通道4，亮度light
                Array.Clear(rcvBuffer, 0, rcvBuffer.Length);     //清空接收缓存区
            }
        }
        private void button7_Click(object sender, EventArgs e)                    //退出软件按钮
        {
            this.Close();
        }

        string GetRcvData()                         //获取接收的信息，返回字符串
        {
            string str = "接收：";
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
            byte result = 16;             //失败返回16，成功返回0-15
            switch (ch)
            {
                case '0':
                    result = 0;
                    break;
                case '1':
                    result = 1;
                    break;
                case '2':
                    result = 2;
                    break;
                case '3':
                    result = 3;
                    break;

                case '4':
                    result = 4;
                    break;
                case '5':
                    result = 5;
                    break;
                case '6':
                    result = 6;
                    break;
                case '7':
                    result = 7;
                    break;

                case '8':
                    result = 8;
                    break;
                case '9':
                    result = 9;
                    break;
                case 'A':
                    result = 10;
                    break;
                case 'B':
                    result = 11;
                    break;

                case 'C':
                    result = 12;
                    break;
                case 'D':
                    result = 13;
                    break;
                case 'E':
                    result = 14;
                    break;
                case 'F':
                    result = 15;
                    break;
            }

            return result;

        }
        private ManualResetEvent mreTimeOut = new ManualResetEvent(false);
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {

                if (client != null)
                {
                    client.EndConnect(ar);
                    if (client.Client != null && !client.Connected)
                    {
                        ShowMesage("网口连接失败，请检查网络配置"); ;

                    }
                }
                else
                {
                    ShowMesage("网口连接失败，请检查网络配置"); ;
                }
            }
            catch (Exception)
            {
                ShowMesage("网口连接失败，请检查网络配置"); ;
            }
            finally
            {
                mreTimeOut.Set();
            }
        }



        private void button5_Click(object sender, EventArgs e)                 //串口连接开关
        {
            if (e_commmode == CommMode.网口)
            {
                if (client != null && client.Client != null && client.Connected)
                {
                    isReceiving = false;
                    client.Client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    if (thread != null)
                    {
                        thread.Abort();
                    }

                    button5.Text = "启动连接";
                    tbState.Text = "网口已断开";
                    butt_state(false);            //关闭按钮
                    textBox1.Text = "";
                }
                else
                {
                    try
                    {
                        ControlInfo controlInfo = m_ch1919Manger.localIpInfoLst.FirstOrDefault(p => p.mac.Trim() == comBoxPorts.Text.Trim());
                        if (controlInfo != null)
                        {


                            if (Convert.ToBoolean(m_ch1919Manger.curDeviceConfig.bDhcpEnable))
                            {

                                m_ch1919Manger.curInfo = m_ch1919Manger.localIpInfoLst.FirstOrDefault(p => p.mac.Trim() == comBoxPorts.Text.Trim());
                                if (m_ch1919Manger.curInfo != null)
                                {
                                    byte[] bytes = HexStringToByte(comBoxPorts.Text.Trim());
                                    m_ch1919Manger.ReadConfig(bytes);
                                    // FrmCommConfig frm = new FrmCommConfig(comBoxPorts.Text, m_ch1919Manger.curDeviceConfig, m_ch1919Manger.curProtConfig2, m_ch1919Manger.curInfo.ipaddress);
                                }
                            }


                            string ip = m_ch1919Manger.curDeviceConfig.bDevIP[0].ToString("000") + "." +
                                m_ch1919Manger.curDeviceConfig.bDevIP[1].ToString("000") + "." +
                                m_ch1919Manger.curDeviceConfig.bDevIP[2].ToString("000") + "." +
                                m_ch1919Manger.curDeviceConfig.bDevIP[3].ToString("000");
                            if (client != null && client.Client != null)
                            {
                                isReceiving = false;
                                client.Close();


                            }
                            mreTimeOut.Reset();
                            client = new TcpClient();
                            client.BeginConnect(IPAddress.Parse(ip), Convert.ToInt32(m_ch1919Manger.curProtConfig2.wNetPort), ConnectCallback, null);
                            mreTimeOut.WaitOne(2000);
                            if (client.Connected)
                            {

                                thread = new Thread(Receive);
                                thread.IsBackground = true;
                                thread.Start();
                                button5.Text = "断开连接";
                                tbState.Text = "网口已连接";
                                butt_state(true);
                                button6_Click(null, null);          //刷新函数
                            }

                        }



                    }
                    catch
                    {
                        tbState.Text = "网口连接失败";
                        button5.Text = "启动连接";
                        butt_state(false);
                        ShowMesage("连接失败，请检查网络配置");
                        return;
                    }

                }
            }
            else
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    button5.Text = "启动连接";
                    tbState.Text = "串口已断开";
                    butt_state(false);            //关闭按钮
                    textBox1.Text = "";
                }

                else
                {
                    try
                    {
                        serialPort1.PortName = comBoxPorts.Text;
                        serialPort1.Open();
                        butt_state(true);            //关闭按钮
                        button6_Click(null, null);          //刷新函数

                    }
                    catch
                    {
                        tbState.Text = "串口连接失败";
                        button5.Text = "启动连接";
                        butt_state(false);
                        return;
                    }
                    button5.Text = "断开连接";
                    tbState.Text = "串口已连接";
                    butt_state(true);            //开启按钮
                }
            }


        }

        void butt_state(bool state)              //按钮状态设置
        {
            groupBox1.Enabled = state;
        }

        private void button6_Click(object sender, EventArgs e)               //刷新按钮
        {
            if (e_commmode == CommMode.串口)
            {
                if (serialPort1.IsOpen == false) return;
            }
            else
            {
                if (client == null || client.Client == null) return;

                if (client != null && client.Client != null && !client.Connected) return;
            }
           // UpDetection();   //查询侦测功能
            UpPulse();       //查询脉宽

            for (byte i = 1; i <= 4; i++)
            {
                CmdSend("4", i, 0);
                if (i == 1)
                {
                    tbLight1.Value = lights[i - 1];                      //该赋值语句将触发tbLight1_MouseUp事件
                    button1.Text = "打开";
                    //button1.BackColor = Color.LimeGreen;
                    button1.BackColor = Color.LimeGreen;
                }
                if (i == 2)
                {
                    tbLight2.Value = lights[i - 1];                      //该赋值语句将触发tbLight2_MouseUp事件
                    button2.Text = "打开";
                    button2.BackColor = Color.LimeGreen;
                }
                if (i == 3)
                {
                    tbLight3.Value = lights[i - 1];                      //该赋值语句将触发tbLight3_MouseUp事件
                    button3.Text = "打开";
                    button3.BackColor = Color.LimeGreen;

                }
                if (i == 4)
                {
                    tbLight4.Value = lights[i - 1];                      //该赋值语句将触发tbLight4_MouseUp事件
                    button4.Text = "打开";
                    button4.BackColor = Color.LimeGreen;
                }
            }
            GetModel();
        }
        void GetModel()
        {
            string str1 = "$SS00014";        //读取模式
            CmdSend(str1);
        }


        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (AutoSize != null) AutoSize.FormSizeChanged();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

       /* private void button9_Click(object sender, EventArgs e)
        {
            if (e_commmode == CommMode.串口)
            {
                if (serialPort1.IsOpen == false) return;
            }
            else
            {
                if (client == null || client.Client == null) return;

                if (client != null && client.Client != null && !client.Connected) return;
            }


            if (button9.Text == "已打开")
            {
                CmdSend("A", 0, 0);                      //串口通讯发送函数，命令A,设置侦测功能
                button9.Text = "已关闭";
                button9.BackColor = Color.Red;
                Detection = 0;
                if (Detection == 1)
                {
                    panel1.Enabled = false;
                }
                else panel1.Enabled = true;
            }
            else
            {
                CmdSend("A", 0, 1);                      //串口通讯发送函数，命令A,设置侦测功能
                button9.Text = "已打开";
                button9.BackColor = Color.Green;
                Detection = 1;
                if (Detection == 1)
                {
                    panel1.Enabled = false;
                }
                else panel1.Enabled = true;
            }
        }*/
        void UpLight()     //查询亮度
        {
            if (serialPort1.IsOpen == false) return;
            CmdSend("4", 1, 0);
            tbLight1.Value = lights[0];                      //该赋值语句将触发tbLight1_MouseUp事件
            button1.Text = "打开";
            button1.BackColor = Color.LimeGreen;

            CmdSend("4", 2, 0);
            tbLight2.Value = lights[1];                      //该赋值语句将触发tbLight2_MouseUp事件
            button2.Text = "打开";
            button2.BackColor = Color.LimeGreen;

            CmdSend("4", 3, 0);
            tbLight3.Value = lights[2];                      //该赋值语句将触发tbLight3_MouseUp事件
            button3.Text = "打开";
            button3.BackColor = Color.LimeGreen;

            CmdSend("4", 4, 0);
            tbLight4.Value = lights[3];                      //该赋值语句将触发tbLight4_MouseUp事件
            button4.Text = "打开";
            button4.BackColor = Color.LimeGreen;
        }

        void UpPulse()    //查询脉宽
        {
            if (e_commmode == CommMode.串口)
            {
                if (serialPort1.IsOpen == false) return;
            }
            else
            {
                if (client == null || client.Client == null) return;

                if (client != null && client.Client != null && !client.Connected) return;
            }
            CmdSend("6", 1, 0);
            if (Pulse[0] > 0) numericUpDown1.Value = Pulse[0];
            CmdSend("6", 2, 0);
            if (Pulse[1] > 0) numericUpDown2.Value = Pulse[1];
            CmdSend("6", 3, 0);
            if (Pulse[2] > 0) numericUpDown3.Value = Pulse[2];
            CmdSend("6", 4, 0);
            if (Pulse[3] > 0) numericUpDown4.Value = Pulse[3];
        }


        /*void UpDetection()     //查询侦测功能
        {
            if (e_commmode == CommMode.串口)
            {
                if (serialPort1.IsOpen == false) return;
            }
            else
            {
                if (client == null || client.Client == null) return;

                if (client != null && client.Client != null && !client.Connected) return;
            }
            CmdSend("B", 0, 0);                      //串口通讯发送函数，命令B,查询侦测功能

            if (Detection == 1)
            {
                button9.Text = "已打开";
                button9.BackColor = Color.LimeGreen;
                panel1.Enabled = false;
            }
            else
            {
                button9.Text = "已关闭";
                button9.BackColor = Color.Red;
                panel1.Enabled = true;
            }
        }*/

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (e_commmode == CommMode.串口)
            {
                if (serialPort1.IsOpen == false) return;
            }
            else
            {
                if (client == null || client.Client == null) return;

                if (client != null && client.Client != null && !client.Connected) return;
            }
            if (true)
            {
                int Value = (int)numericUpDown1.Value;
                CmdSend("5", 1, Value);      //串口通讯发送函数，命令5设置脉宽，通道1
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (e_commmode == CommMode.串口)
            {
                if (serialPort1.IsOpen == false) return;
            }
            else
            {
                if (client == null || client.Client == null) return;

                if (client != null && client.Client != null && !client.Connected) return;
            }
            if (true)
            {
                int Value = (int)numericUpDown2.Value;
                CmdSend("5", 2, Value);      //串口通讯发送函数，命令5设置脉宽，通道2
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (e_commmode == CommMode.串口)
            {
                if (serialPort1.IsOpen == false) return;
            }
            else
            {
                if (client == null || client.Client == null) return;

                if (client != null && client.Client != null && !client.Connected) return;
            }
            if (true)
            {
                int Value = (int)numericUpDown3.Value;
                CmdSend("5", 3, Value);      //串口通讯发送函数，命令5设置脉宽，通道3
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (e_commmode == CommMode.串口)
            {
                if (serialPort1.IsOpen == false) return;
            }
            else
            {
                if (client == null || client.Client == null) return;

                if (client != null && client.Client != null && !client.Connected) return;
            }
            if (true)
            {
                int Value = (int)numericUpDown4.Value;
                CmdSend("5", 4, Value);      //串口通讯发送函数，命令5设置脉宽，通道4
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btn_searchDevice_Click(object sender, EventArgs e)
        {
            try
            {

                switch (e_commmode)
                {
                    case CommMode.串口:
                        {
                            string Cur_PortName = comBoxPorts.Text;
                            try
                            {
                                string[] strName = SerialPort.GetPortNames();
                                comBoxPorts.Text = string.Empty;
                                comBoxPorts.Items.Clear();
                                foreach (var item in strName)
                                {
                                    comBoxPorts.Items.Add(item);
                                }
                                if (comBoxPorts.Items.Count > 0)
                                {
                                    comBoxPorts.SelectedIndex = 0;
                                }
                            }

                            catch (Exception ex)
                            {
                                if (ex.Source != null)
                                {
                                }
                            }
                            break;
                        }
                    case CommMode.网口:
                        {
                            m_ch1919Manger.SendSearch();
                            System.Threading.Thread.Sleep(500);
                            string[] strName = m_ch1919Manger.localIpInfoLst.Select(p => p.mac).ToArray();
                            comBoxPorts.Text = string.Empty;
                            comBoxPorts.Items.Clear();


                            tbState.Text = "网口未打开";

                            for (int i = 0; i < strName.Length; i++)
                            {

                                comBoxPorts.Items.Add(strName[i]);
                            }
                            if (comBoxPorts.Items.Count > 0)
                            {
                                comBoxPorts.SelectedIndex = 0;
                                ShowMesage("发现" + comBoxPorts.Items.Count.ToString() + "台可用控制器");   //清空消息框
                            }
                            else
                            {
                                ShowMesage("没有发现可用控制器");

                            }

                            break;
                        }

                    default:
                        break;
                }

            }

            catch (Exception ex)
            {
                if (ex.Source != null)
                {
                }
            }
        }

        private void rdb_port_CheckedChanged(object sender, EventArgs e)
        {
            SetCommMode();

        }

        private void rdb_socket_CheckedChanged(object sender, EventArgs e)
        {
            SetCommMode();
        }
        private void SetCommMode()
        {
            if (rdb_port.Checked == true && rdb_socket.Checked == false)
            {
                e_commmode = CommMode.串口;
                btn_commConfig.Text = "详细信息";
                btn_commConfig.Enabled = false;
                if (client != null && client.Client != null && client.Connected)
                {
                    isReceiving = false;
                    client.Close();
                    thread.Abort();
                    button5.Text = "启动连接";
                    tbState.Text = "串口已断开";
                    butt_state(false);            //关闭按钮
                }
            }
            else
            {
                e_commmode = CommMode.网口;
                btn_commConfig.Text = "ip设置";
                btn_commConfig.Enabled = true;
                if (serialPort1.IsOpen)
                { serialPort1.Close(); }
                button5.Text = "启动连接";
                tbState.Text = "网口已断开";

            }
            comBoxPorts.Items.Clear();
            comBoxPorts.Text = String.Empty;
            btn_searchDevice_Click(null, null);
        }
        private byte[] HexStringToByte(string hs)
        {
            string strTemp = "";
            byte[] b = new byte[hs.Length / 2];
            for (int i = 0; i < hs.Length / 2; i++)
            {
                strTemp = hs.Substring(i * 2, 2);
                b[i] = Convert.ToByte(strTemp, 16);
            }
            //按照指定编码将字节数组变为字符串
            return b;
        }


        private void btn_commConfig_Click(object sender, EventArgs e)
        {

            if (client != null && client.Client != null && client.Connected)
            {
                MessageBox.Show("请先关闭连接!!!");
                return;
            }

            if (comBoxPorts.Text != null && comBoxPorts.Text != String.Empty)
            {
                m_ch1919Manger.curInfo = m_ch1919Manger.localIpInfoLst.FirstOrDefault(p => p.mac.Trim() == comBoxPorts.Text.Trim());
                if (m_ch1919Manger.curInfo != null)
                {
                    byte[] bytes = HexStringToByte(comBoxPorts.Text.Trim());
                    m_ch1919Manger.ReadConfig(bytes);
                    FrmCommConfig frm = new FrmCommConfig(comBoxPorts.Text, m_ch1919Manger.curDeviceConfig, m_ch1919Manger.curProtConfig2, m_ch1919Manger.curInfo.ipaddress);
                    ShowMesage("正在配置参数");
                    if (frm.ShowDialog() == DialogResult.Yes)
                    {
                        m_ch1919Manger.WriteConfig(bytes.ToArray(), new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                        ToolKits.UI.DialogUI.Loading loadFrm = new ToolKits.UI.DialogUI.Loading();
                        loadFrm.Text = "正在配置参数......";
                        loadFrm.Shown += (s, ev) =>
                        {
                            Task.Factory.StartNew(() =>
                            {
                                Thread.Sleep(3000);
                                loadFrm.TopMost = false;
                                loadFrm.Close();
                            });
                        };
                        loadFrm.TopMost = true;
                        loadFrm.ShowDialog();
                        ShowMesage("参数配置完成");
                    }
                    else
                    { ShowMesage("已取消配置"); }
                }

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\NetModuleConfig.exe";
            Process.Start(path);
        }



        private void comBoxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* int labl_value1 = 255;
            if ((string)comboBox1.SelectedItem == "999等级") labl_value1 = 999;

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
            numLight4.Maximum = labl_value1;*/
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
                            MessageBox.Show("读取模式SS返回模式有误：" + RecString[2]);
                            break;
                    }
                    res = true;
                }
                else MessageBox.Show(RecString + "," + CheckRecStr + "读取模式指令接收响应SS，校验和有误！");
            }
            else                                                                             //设置模式指令接收响应
            {
                try
                {
                    int reviByte = serialPort1.ReadByte();                         //指令S等待响应一个字符
                    RecString = ((char)reviByte).ToString();
                    if (RecString != "$") MessageBox.Show("命令字S,返回值有误！" + RecString);
                    else res = true;
                }
                catch (Exception a)
                {
                    MessageBox.Show(SendString + "接收响应" + a.Message);
                }
            }
            ShowMesage("接收:" + RecString);
            return res;
        }
        byte[] senModelBuffer = new byte[8];
        void SetModel(string Model)
        {
            string str1 = "";
            if (Model == "S1") { str1 = "$S100076"; Now_Model = 1; }
            if (Model == "S2") { str1 = "$S200075"; ; Now_Model = 2; }
            if (Model == "S3") { str1 = "$S300074"; ; Now_Model = 3; }
            CmdSend(str1);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            SetModel("S1");
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
            label18.Enabled = Is_visable;
            label21.Enabled = Is_visable;
            label23.Enabled = Is_visable;
            label25.Enabled = Is_visable;
            label19.Enabled = Is_visable;
            label20.Enabled = Is_visable;
            label22.Enabled = Is_visable;
            label24.Enabled = Is_visable;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SetModel("S2");
            ModelVisable();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SetModel("S3");
            ModelVisable();
        }
    }
}



