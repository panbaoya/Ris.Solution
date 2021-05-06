using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ris.Tools
{
    public class UdpUnit
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public static void SendMsg(string message)
        {
            UdpClient udp = new UdpClient();
            byte[] sendbuf = Encoding.Default.GetBytes(message);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.101"), AppConfSetting.Port);
            udp.SendAsync(sendbuf, sendbuf.Length, iPEndPoint);
            udp.Close();
        }
    }
}
