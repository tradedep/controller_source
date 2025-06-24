using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace LightController
{
    public partial class FrmCommConfig : Form
    {
        _DEVICEHW_CONFIG m_Deviceconfig;
        _DEVICEPORT_CONFIG m_PortConfig;
        IPAddress m_ipaddress;
        string m_macStr;
        public FrmCommConfig(string macStr, _DEVICEHW_CONFIG deviceConfig, _DEVICEPORT_CONFIG portconfig,IPAddress ipaddress)
        {
            m_macStr = macStr;
            m_Deviceconfig = deviceConfig;
            m_PortConfig = portconfig;
            m_ipaddress= ipaddress; 
            InitializeComponent();
            ReadConfig();
        }


        private List<IPAddress> GetIpList()
        {
            List<IPAddress> ipAddressLst = new List<IPAddress>();
            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

            foreach (IPAddress ipAddress in hostEntry.AddressList)
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddressLst.Add(ipAddress);
                    // Console.WriteLine(ipAddress.ToString());
                }
            }
            return ipAddressLst;
        }
        private void ReadConfig()
        {
            List<IPAddress> iplst = GetIpList();
            dataGridView1.Rows.Clear();
            int t = 0;
            foreach (var item in iplst)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[t].Cells[0].Value = "1";
                dataGridView1.Rows[t].Cells[1].Value = item.ToString();
                dataGridView1.Rows[t].Cells[2].Value = "255.255.255.0";
                t++;
            }
       
            tb_macAddr.Text = m_macStr;
            cb_dhcp.Checked = BitConverter.ToBoolean(new byte[] { m_Deviceconfig.bDhcpEnable }, 0);
            maskedTextBox1.Text = m_Deviceconfig.bDevIP[0].ToString("000") + m_Deviceconfig.bDevIP[1].ToString("000") + m_Deviceconfig.bDevIP[2].ToString("000") + m_Deviceconfig.bDevIP[3].ToString("000");
            maskedTextBox2.Text = m_Deviceconfig.bDevIPMask[0].ToString("000") + m_Deviceconfig.bDevIPMask[1].ToString("000") + m_Deviceconfig.bDevIPMask[2].ToString("000") + m_Deviceconfig.bDevIPMask[3].ToString("000");
            maskedTextBox3.Text = m_Deviceconfig.bDevGWIP[0].ToString("000") + m_Deviceconfig.bDevGWIP[1].ToString("000") + m_Deviceconfig.bDevGWIP[2].ToString("000") + m_Deviceconfig.bDevGWIP[3].ToString("000");
            textBox2.Text = m_PortConfig.wNetPort.ToString();
            textBox1.Text = m_PortConfig.dBaudRate.ToString();
        }
        private void WriteConfig()
        {
            string []a = maskedTextBox1.Text.Split('.');
            string[] b = maskedTextBox2.Text.Split('.');
            string[] c = maskedTextBox3.Text.Split('.');
            string d = textBox2.Text;
            m_Deviceconfig.bDevIP[0] = Convert.ToByte(a[0]);
            m_Deviceconfig.bDevIP[1] = Convert.ToByte(a[1]);
            m_Deviceconfig.bDevIP[2] = Convert.ToByte(a[2]);
            m_Deviceconfig.bDevIP[3] = Convert.ToByte(a[3]);

            m_Deviceconfig.bDevIPMask[0] = Convert.ToByte(b[0]);
            m_Deviceconfig.bDevIPMask[1] = Convert.ToByte(b[1]);
            m_Deviceconfig.bDevIPMask[2] = Convert.ToByte(b[2]);
            m_Deviceconfig.bDevIPMask[3] = Convert.ToByte(b[3]);

            m_Deviceconfig.bDevGWIP[0] = Convert.ToByte(c[0]);
            m_Deviceconfig.bDevGWIP[1] = Convert.ToByte(c[1]);
            m_Deviceconfig.bDevGWIP[2] = Convert.ToByte(c[2]);
            m_Deviceconfig.bDevGWIP[3] = Convert.ToByte(c[3]);

            m_Deviceconfig.bDhcpEnable = Convert.ToByte(cb_dhcp.Checked);
            m_PortConfig.wNetPort = Convert.ToUInt16(d);
            m_PortConfig.bNetMode = Convert.ToByte(0);
            m_PortConfig.bRandSportFlag = Convert.ToByte(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteConfig();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void FrmCommConfig_Resize(object sender, EventArgs e)
        {
            return;
        }

        private void cb_dhcp_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_dhcp.Checked)
            {
                groupBox1.Enabled = false;
            }
            else
            { groupBox1.Enabled = true; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
