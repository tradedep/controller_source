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


namespace FourChannelLightController
{
    public partial class MainForm : Form
{
        //int flat;
        AutoAdaptWindowsSize AutoSize;
        //private bool m_CH340Flag = false;//CH340驱动存在标志位
        //private string[] comNames = null;
       // private int m_comBoxNums = 0;
        //private string m_LastComPort = null;
        //private bool m_ComOpened = false;
       //private bool m_ComRcvOk = false;
        private int[] lights = new int[32];                    //预设32通道亮度

        byte[] rcvBuffer = new byte[8];
        byte[] senBuffer = new byte[8];

        public MainForm()
        {
            InitializeComponent();
           // flat = 0;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

      
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string strLeftLight = null, strLeftColTem = null, strRightLight = null, strRightColTem = null;

            //string str_rcv = serialPort1.ReadExisting();

            //Array.Clear(rcvBuffer, 0, rcvBuffer.Length);
            
            //int count = serialPort1.BytesToRead;
            //serialPort1.Read(rcvBuffer, 0, count);
            //serialPort1.Read(rcvBuffer, 0, 8);
            
        }
  
     

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            AutoSize.FormSizeChanged();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            ThirdGenMiniController f = new ThirdGenMiniController();
            f.ShowDialog();
            this.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            EightChannelController f = new EightChannelController();
            f.ShowDialog();
            this.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            BrighteningStroboscopicController f = new BrighteningStroboscopicController();
            f.ShowDialog();
            this.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            ThirdGenController f = new ThirdGenController();
            f.ShowDialog();
            this.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
            SecondGenHighPowerController f = new SecondGenHighPowerController();
            f.ShowDialog();
            this.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConventionalController f = new ConventionalController();
            f.ShowDialog();
            this.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AutoSize = new AutoAdaptWindowsSize(this);
        }
    }
}



