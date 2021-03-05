using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Util.Controls.WPFTest
{
    class ServerConn
    {
        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static int PORT = int .Parse(ConfigurationManager.AppSettings["ServerPORT"].ToString());
        
        private static IPAddress IP = IPAddress.Parse(ConfigurationManager.AppSettings["ServerIP"].ToString());
        //private static IPAddress ip = IPAddress.Loopback;

        //连接服务器，返回MsgSendReceiver，失败时返回null
        public static MsgSendReceiver ConnServer()
        {
            IPEndPoint server = new IPEndPoint(IP, PORT);
            try
            {
                socket.Connect(server);
                return new MsgSendReceiver(socket);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool SocketClose()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);//保证所有数据传输完毕
                socket.Close();//关闭socket
                return true;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            return false;
        }

        public static bool SocketTest()
        {
            bool blockingState = socket.Blocking;
            try
            {
                byte[] tmp = new byte[1];
                socket.Blocking = false;
                socket.Send(tmp, 0, 0);
                return true;
            }
            catch (SocketException e)
            {
                if (e.NativeErrorCode.Equals(10035))
                    return true;
                else
                    return false;
            }
            finally
            {
                socket.Blocking = blockingState;    // 恢复状态
            }
        }
    }
}
