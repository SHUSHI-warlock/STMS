using System;
using System.Collections.Generic;
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
        private static int PORT = 7000;
        private static IPAddress ip = IPAddress.Parse("127.0.0.1");
        //private static IPAddress ip = IPAddress.Loopback;

        //连接服务器，返回MsgSendReceiver，失败时返回null
        public static MsgSendReceiver ConnServer()
        {
            //连接到的目标IP
            //IPAddress ip = IPAddress.Loopback;
            //IPAddress ip = IPAddress.Any;

            //连接到目标IP的哪个应用(端口号！)
            IPEndPoint server = new IPEndPoint(ip, PORT);
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
                socket.Disconnect(true);
                return true;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);

            }
            return false;
        }

    }
}
