using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Mail;

namespace LightController
{

    internal enum CommMode
    {
        串口,
        网口,
    }


    internal class Broadcaster
    {
        private UdpClient udpClient;
        private IPEndPoint endPoint;
        public void Send(byte[] bytes)
        {
            //byte[] ipBytes = ipAddress.GetAddressBytes();
            //ipBytes[3] = 255; // 设置广播地址的最后一个字节为255  
            //IPAddress broadcastAddress = new IPAddress(ipBytes);
            udpClient = new UdpClient();
            udpClient.EnableBroadcast = true;
            endPoint = new IPEndPoint(IPAddress.Broadcast, 50000);
            try
            {
                udpClient.Send(bytes, bytes.Length, endPoint);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                udpClient.Close();
            }
        }
    }


    internal class Receiver
    {
        public event Action<byte[], IPEndPoint> RecivedEvent;
        private UdpClient udpClient;
        private Thread receiveThread;
        private bool isRunning = true;

        public Receiver()
        {

        }


        public void StartReceive()
        {
            isRunning = true;
            udpClient = new UdpClient(60000);
            udpClient.EnableBroadcast = true;
            receiveThread = new Thread(func);
            receiveThread.IsBackground = true;
            receiveThread.Start();

        }
        private void func()
        {

            while (true)
            {
                if (isRunning)
                {
                    try
                    {
                        IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                        byte[] bytesReceived = udpClient.Receive(ref remoteIpEndPoint);
                        if (RecivedEvent != null)
                        {
                            RecivedEvent.Invoke(bytesReceived, remoteIpEndPoint);
                        }
                    }
                    catch (Exception)
                    { 
                    
                    
                    }
                }
            }

        }

        public void StopRecive()
        {
            isRunning = false;
            receiveThread.Abort();
            udpClient.Client.Close();
            udpClient.Close();
          
        }
    }



    internal class CHA1919Manger
    {
        Broadcaster commSend;
        Receiver commRecive;
        private ManualResetEvent m_manualReset = new ManualResetEvent(false);
        //通信命令码
        const byte NET_MODULE_CMD_SET = 0X01;   //配置网络中的CH9121
        const byte NET_MODULE_CMD_GET = 0X02; //获取某个CH9121的配置
        const byte NET_MODULE_CMD_RESET = 0X03;  //获取某个CH9121的配置
        const byte NET_MODULE_CMD_SEARCH = 0X04;  //搜索网络中的CH9121

        //应答命令码
        const byte NET_MODULE_ACK_SET = 0X81;   //回应配置命令码
        const byte NET_MODULE_ACK_GET = 0X82;  //回应获取命令码
        const byte NET_MODULE_ACK_RESET = 0X83;   //获取某个CH9121的配置
        const byte NET_MODULE_ACK_SEARCH = 0X84; //回应所搜命令码

        //校验和错误时的应答命令码
        //byte NET_MODULE_NAK_SEARCH = 0xC0; //搜索校验错
        //byte NET_MODULE_NAK_SET = 0XC1;//配置校验错
       // byte NET_MODULE_NAK_GET = 0XC2;  //获取校验错
        byte[] CH9121_CFG_FLAG = Encoding.ASCII.GetBytes("CH9121_CFG_FLAG"); //用来标识通信
       
        public ControlInfo curInfo = null;
        public List<ControlInfo> localIpInfoLst = new List<ControlInfo>();
        public _DEVICEHW_CONFIG curDeviceConfig = null;
        public _DEVICEPORT_CONFIG curProtConfig1 = null;
        public _DEVICEPORT_CONFIG curProtConfig2= null;
        public NET_COMM curNetComm = null;
        public CHA1919Manger()
        {
            commSend = new Broadcaster();
            commRecive = new Receiver();
            commRecive.StartReceive();
            commRecive.RecivedEvent -= RecivedFunc;
            commRecive.RecivedEvent += RecivedFunc;
            
        }

        public void DisPose()
        {
            commRecive.StopRecive();
        }
        public void SendSearch()
        {
           
          //  commRecive.StartReceive();
            localIpInfoLst.Clear();
            List<IPAddress> iplst = GetIpList();
         
                NET_COMM netCom = new NET_COMM();
                netCom.flag = CH9121_CFG_FLAG;
                netCom.cmd = NET_MODULE_CMD_SEARCH;
              
           //     m_manualReset.Reset();
                commSend.Send(netCom.GetBytes());
              //  m_manualReset.WaitOne(500);
            
           // commRecive.StopRecive();
        }

        public void ReadConfig(byte[] idBytes)
        {
          //  commRecive.StartReceive();
            NET_COMM netCom = new NET_COMM();
            netCom.flag = CH9121_CFG_FLAG;
            netCom.cmd = NET_MODULE_CMD_GET;
            netCom.id = idBytes;
            m_manualReset.Reset();
            commSend.Send(netCom.GetBytes());
            m_manualReset.WaitOne(200);
           // commRecive.StopRecive();
        }




        public void WriteConfig(byte[] idBytes, byte[] pcidBytes)
        {
            NET_COMM netCom = new NET_COMM();
            netCom.flag = CH9121_CFG_FLAG;
            netCom.cmd = NET_MODULE_CMD_SET;
            netCom.id = idBytes;
            netCom.pcid = pcidBytes;
            int bytelen = curDeviceConfig.GetBytes().Length + curProtConfig1.GetBytes().Length+ curProtConfig2.GetBytes().Length;
            netCom.len = BitConverter.GetBytes(bytelen)[3];

            List<byte> byteLst = new List<byte>();
            byteLst.AddRange(curDeviceConfig.GetBytes());
            byteLst.AddRange(curProtConfig1.GetBytes());
            byteLst.AddRange(curProtConfig2.GetBytes());
            byte[] ttbyte = new byte[255 - bytelen];
            byteLst.AddRange(ttbyte);
            netCom.data = byteLst.ToArray();
            commSend.Send(netCom.GetBytes());
        }

        public void Reset(byte[] idBytes)
        {
            NET_COMM netCom = new NET_COMM();
            netCom.flag = CH9121_CFG_FLAG;
            netCom.cmd = NET_MODULE_CMD_RESET;
            netCom.id = idBytes;
            commSend.Send(netCom.GetBytes());
        }
        public void tt()
        {
            // 获取所有网络接口  
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            // 遍历每个网络接口以获取其物理地址（即MAC地址）  
            foreach (NetworkInterface adapter in nics)
            {
                // 排除非以太网适配器  
                if (adapter.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;

                // 获取当前适配器的物理地址（MAC地址）  
                PhysicalAddress address = adapter.GetPhysicalAddress();
                // 打印MAC地址  
                Console.WriteLine("MAC Address: " + address.ToString());
            }
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





       
            //// 获取所有网络接口  
            //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            //// 遍历每个网络接口并输出其名称和IP地址  
            //foreach (NetworkInterface adapter in nics)
            //{
            //    foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
            //    {
            //        if (ip.Address.AddressFamily == AddressFamily.InterNetwork) // 只获取IPv4地址  
            //        {
            //            ipAddressLst.Add(ip.Address);
            //        }
            //    }
            //}
            return ipAddressLst;
        }



        public void RecivedFunc(byte[] byteRecived, IPEndPoint remoteIpEndPoint)
        {
            curNetComm = new NET_COMM();
            List<byte> byteLst = byteRecived.ToList();
            curNetComm.flag = byteLst.Skip(0).Take(16).ToArray();
            curNetComm.cmd = byteLst.Skip(16).Take(1).ToArray()[0];
            curNetComm.id = byteLst.Skip(17).Take(6).ToArray();
            curNetComm.pcid = byteLst.Skip(23).Take(6).ToArray();
            curNetComm.len = byteLst.Skip(29).Take(1).ToArray()[0];
            curNetComm.data = byteLst.Skip(30).Take(255).ToArray();
            switch (curNetComm.cmd)
            {
                case NET_MODULE_ACK_SET:
                    {
                        break;
                    }

                case NET_MODULE_ACK_GET:
                    {
                        curDeviceConfig = new _DEVICEHW_CONFIG();
                        curProtConfig1 = new _DEVICEPORT_CONFIG();
                        curProtConfig2 = new _DEVICEPORT_CONFIG();
                        byte[] deviceBytes = curNetComm.data.Skip(0).Take(curDeviceConfig.GetBytes().Length).ToArray();
                        byte[] portBytes1 = curNetComm.data.Skip(curDeviceConfig.GetBytes().Length).Take(curProtConfig1.GetBytes().Length).ToArray();
                        byte[] portBytes2 = curNetComm.data.Skip(curDeviceConfig.GetBytes().Length+ curProtConfig1.GetBytes().Length).Take(curProtConfig2.GetBytes().Length).ToArray();
                        curDeviceConfig.FitData(deviceBytes);
                        curProtConfig1.FitData(portBytes1);
                        curProtConfig2.FitData(portBytes2);
                        m_manualReset.Set();
                        break;
                    }
                case NET_MODULE_ACK_RESET:
                    {
                        break;
                    }
                case NET_MODULE_ACK_SEARCH:
                    {
                      
                        ControlInfo ctrInfo = new ControlInfo();
                        ctrInfo.mac = BitConverter.ToString(new byte[] { curNetComm.id[0] }, 0)
                            + BitConverter.ToString(new byte[] { curNetComm.id[1] }, 0)
                            + BitConverter.ToString(new byte[] { curNetComm.id[2] }, 0)
                            + BitConverter.ToString(new byte[] { curNetComm.id[3] }, 0)
                            + BitConverter.ToString(new byte[] { curNetComm.id[4] }, 0)
                            + BitConverter.ToString(new byte[] { curNetComm.id[5] }, 0);
                        ctrInfo.version = (uint)curNetComm.data.Skip(12).Take(1).ToList().ToArray()[0];
                        if (localIpInfoLst != null)
                        {
                            localIpInfoLst.Add(ctrInfo);
                        }
                      //  m_manualReset.Set();
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

        }

    }



    //网络通信结构体
    internal class NET_COMM
    {
        public byte[] flag = new byte[16];                     //通信标识，因为都是用广播方式进行通信的，所以这里加一个固定值
        public byte cmd = new byte();                          //命令头
        public byte[] id = new byte[6];                        //CH9121MAC地址
        public byte[] pcid = new byte[6];                      //PC的MAC地址
        public byte len = new byte();                          //数据区长度
        public byte[] data = new byte[255]; //数据区缓冲区
        public byte[] GetBytes()
        {
            List<byte> byts = new List<byte>();
            byts.AddRange(flag);
            byts.Add(new byte());
            byts.Add(cmd);
            byts.AddRange(id);
            byts.AddRange(pcid);
            byts.Add(len);
            byts.AddRange(data);
            return byts.ToArray();
        }
    }


    public class _DEVICEHW_CONFIG
    {
        public byte bDevType = new byte();                /* 设备类型，只读 */
        public byte bAuxDevType = new byte();             /* 设备子类型, 只读*/
        public byte bIndex = new byte();                  /* 设备序号, 只读*/
        public byte bDevHardwareVer = new byte();         /* 设备硬件版本号,只读 */
        public byte bDevSoftwareVer = new byte();         /* 设备软件版本号,只读 */
        public byte[] szModulename = new byte[21];        /* 用户名同CH9121名*/
        public byte[] bDevMAC = new byte[6];            /* CH9121网络MAC地址 */
        public byte[] bDevIP = new byte[4];             /* CH9121IP地址*/
        public byte[] bDevGWIP = new byte[4];           /* CH9121网关IP */
        public byte[] bDevIPMask = new byte[4];         /* CH9121子网掩码 */
        public byte bDhcpEnable = new byte();         /* DHCP 使能，是否启用DHCP,1:启用，0：不启用*/
        public ushort breserved1 = new ushort();              /* 预留暂未启用 */
        public byte[] breserved2 = new byte[8];           /* 预留暂未启用*/
        public byte breserved3 = new byte();              /* 预留暂未启用*/
        public byte[] breserved4 = new byte[8];           /* 预留暂未启用*/
        public byte breserved5 = new byte();              /* 预留暂未启用*/
        public byte bComcfgEn = new byte();               /* 串口协商配置标志 1：启用 0：禁用*/
        public byte[] breserved6 = new byte[8];           /* 预留暂未启用*/

        public void FitData(byte[] bytes)
        {
            List<byte> byteLst = bytes.ToList();
            this.bDevType = byteLst.Skip(0).Take(1).ToArray()[0];
            this.bAuxDevType = byteLst.Skip(1).Take(1).ToArray()[0];
            this.bIndex = byteLst.Skip(2).Take(1).ToArray()[0];
            this.bDevHardwareVer = byteLst.Skip(3).Take(1).ToArray()[0];
            this.bDevSoftwareVer = byteLst.Skip(4).Take(1).ToArray()[0];
            this.szModulename = byteLst.Skip(5).Take(21).ToArray();
            this.bDevMAC = byteLst.Skip(26).Take(6).ToArray();
            this.bDevIP = byteLst.Skip(32).Take(4).ToArray();
            this.bDevGWIP = byteLst.Skip(36).Take(4).ToArray();
            this.bDevIPMask = byteLst.Skip(40).Take(4).ToArray();
            this.bDhcpEnable = byteLst.Skip(44).Take(1).ToArray()[0];
            this.breserved1 = BitConverter.ToUInt16(byteLst.Skip(45).Take(2).ToArray(), 0);
            this.breserved2 = byteLst.Skip(47).Take(8).ToArray();
            this.breserved3 = byteLst.Skip(55).Take(1).ToArray()[0];
            this.breserved4 = byteLst.Skip(56).Take(8).ToArray();
            this.breserved5 = byteLst.Skip(64).Take(1).ToArray()[0];
            this.bComcfgEn = byteLst.Skip(65).Take(1).ToArray()[0];
            this.breserved6 = byteLst.Skip(66).Take(8).ToArray();
        }



        public byte[] GetBytes()
        {
            List<byte> byts = new List<byte>();
            byts.Add(bDevType);
            byts.Add(bAuxDevType);
            byts.Add(bIndex);
            byts.Add(bDevHardwareVer);
            byts.Add(bDevSoftwareVer);
            byts.AddRange(szModulename);
            byts.AddRange(bDevMAC);
            byts.AddRange(bDevIP);
            byts.AddRange(bDevGWIP);
            byts.AddRange(bDevIPMask);
            byts.Add(bDhcpEnable);
            byts.AddRange(BitConverter.GetBytes(breserved1));
            byts.AddRange(breserved2);
            byts.Add(breserved3);
            byts.AddRange(breserved4);
            byts.Add(breserved5);
            byts.Add(bComcfgEn);
            byts.AddRange(breserved6);
            return byts.ToArray();
        }




    }



    public class _DEVICEPORT_CONFIG
    {
        public byte bIndex = new byte();                   /* 子设备序号，只读  */
        public byte bPortEn = new byte();                  /* 端口启用标志 1：启用 ；0：不启用 */
        public byte bNetMode = new byte();                 /* 网络工作模式: 0: TCP SERVER;1: TCP CLENT; 2: UDP SERVER 3：UDP CLIENT; */
        public byte bRandSportFlag = new byte();           /* TCP 客户端模式下随机本地端口号，1：随机 0: 不随机*/
        public ushort wNetPort = new ushort();                 /* 本地端口号 */
        public byte[] bDesIP = new byte[4];              /* 目的IP地址 */
        public ushort wDesPort = new ushort();                 /* 目的端口号 */
        public uint dBaudRate = new uint();                /* 串口波特率: 300---921600bps */
        public byte bDataSize = new byte();                /* 串口数据位: 5---8位 */
        public byte bStopBits = new byte();                /* 串口停止位: 0表示1个停止位; 1表示1.5个停止位; 2表示2个停止位 */
        public byte bParity = new byte();                  /* 串口校验位: 4表示无校验，0表示奇校验; 1表示偶校验; 2表示标志位(MARK,置1); 3表示空白位(SPACE,清0);  */
        public byte bPHYChangeHandle = new byte();         /* PHY断开，Socket动作，1：关闭Socket 0：不动作*/
        public uint dRxPktlength = new uint();             /* 串口RX数据打包长度，最大1024 */
        public uint dRxPktTimeout = new uint();            /* 串口RX数据打包转发的最大等待时间,单位为: 10ms,0则表示关闭超时功能 */
        public byte bResv = new byte();                    /* 预留未启用*/
        public byte bResetCtrl = new byte();               /* 串口复位操作: 0表示不清空串口数据缓冲区; 1表示连接时清空串口数据缓冲区 */
        public byte bDNSFlag = new byte();                 /* 域名功能启用标志，1：启用 0：不启用*/
        public byte[] szDomainname = new byte[20];         /* TCP客户端模式下，目的地址，域名*/
        public byte[] breserved = new byte[14];            /* 保留*/


        public void FitData(byte[] bytes)
        {
            List<byte> byteLst = bytes.ToList();
            this.bIndex = byteLst.Skip(0).Take(1).ToArray()[0];
            this.bPortEn = byteLst.Skip(1).Take(1).ToArray()[0];
            this.bNetMode = byteLst.Skip(2).Take(1).ToArray()[0];
            this.bRandSportFlag = byteLst.Skip(3).Take(1).ToArray()[0];
            this.wNetPort = BitConverter.ToUInt16(byteLst.Skip(4).Take(2).ToArray(), 0);
            this.bDesIP = byteLst.Skip(6).Take(4).ToArray();
            this.wDesPort = BitConverter.ToUInt16(byteLst.Skip(10).Take(2).ToArray(), 0);
            this.dBaudRate = BitConverter.ToUInt16(byteLst.Skip(12).Take(4).ToArray(), 0);
            this.bDataSize = byteLst.Skip(16).Take(1).ToArray()[0];
            this.bStopBits = byteLst.Skip(17).Take(1).ToArray()[0];
            this.bParity = byteLst.Skip(18).Take(1).ToArray()[0];
            this.bPHYChangeHandle = byteLst.Skip(19).Take(1).ToArray()[0];
            this.dRxPktlength = BitConverter.ToUInt16(byteLst.Skip(20).Take(4).ToArray(), 0);
            this.dRxPktTimeout = BitConverter.ToUInt16(byteLst.Skip(24).Take(4).ToArray(), 0);
            this.bResv = byteLst.Skip(28).Take(1).ToArray()[0];
            this.bResetCtrl = byteLst.Skip(29).Take(1).ToArray()[0];
            this.bDNSFlag = byteLst.Skip(30).Take(1).ToArray()[0];
            this.szDomainname = byteLst.Skip(31).Take(20).ToArray();
            this.breserved = byteLst.Skip(51).Take(14).ToArray();
        }

        public byte[] GetBytes()
        {
            List<byte> byts = new List<byte>();
            byts.Add(bIndex);
            byts.Add(bPortEn);
            byts.Add(bNetMode);
            byts.Add(bRandSportFlag);
            byts.AddRange(BitConverter.GetBytes(wNetPort));
            byts.AddRange(bDesIP);
            byts.AddRange(BitConverter.GetBytes(wDesPort));
            byts.AddRange(BitConverter.GetBytes(dBaudRate));

            byts.Add(bDataSize);
            byts.Add(bStopBits);
            byts.Add(bParity);
            byts.Add(bPHYChangeHandle);
            byts.AddRange(BitConverter.GetBytes(dRxPktlength));
            byts.AddRange(BitConverter.GetBytes(dRxPktTimeout));
            byts.Add(bResv);
            byts.Add(bResetCtrl);
            byts.Add(bDNSFlag);
            byts.AddRange(szDomainname);
            byts.AddRange(breserved);
            return byts.ToArray();
        }
    }
    public class ControlInfo
    {
        public string mac = string.Empty;
        public UInt32 version = 0;
        public IPAddress ipaddress;
       
    }



}
