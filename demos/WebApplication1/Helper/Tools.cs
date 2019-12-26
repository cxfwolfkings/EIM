using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace WebApplication1.Helper
{
    /// <summary>
    /// 每次启动、注册服务都要指定一个端口，本地测试集群的时候可能要启动多个实例，很麻烦
    /// 在ASP.Net Core中只要设定端口为0，那么服务器会随机找一个可用的端口绑定（测试一下），
    /// 但是没有找到读取到这个随机端口号的方法，因此自己写
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// 产生一个介于minPort-maxPort之间的随机可用端口
        /// </summary>
        /// <param name="minPort"></param>
        /// <param name="maxPort"></param>
        /// <returns></returns>
        public static int GetRandAvailablePort(int minPort = 1024, int maxPort = 65535)
        {
            Random r = new Random();
            while (true)
            {
                int port = r.Next(minPort, maxPort);
                if (!IsPortInUsed(port))
                {
                    return port;
                }
            }
        }

        /// <summary>
        /// 判断port端口是否在使用中
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static bool IsPortInUsed(int port)
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();
            if (ipsTCP.Any(p => p.Port == port))
            {
                return true;
            }

            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();
            if (ipsUDP.Any(p => p.Port == port))
            {
                return true;
            }

            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            if (tcpConnInfoArray.Any(conn => conn.LocalEndPoint.Port == port))
            {
                return true;
            }
            return false;
        }
    }
}
