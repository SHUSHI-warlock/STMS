using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace CSMsgTrans
{
    class MsgSendReceiver
    {
        private readonly Socket mySocket;
        public MsgSendReceiver(Socket socket)
        {
            this.mySocket = socket;
        }
        public void SendMsg(Msg msg)
        {
            string msgStr = string.Format("{0,-4}", msg.GetProtocol())
                + string.Format("{0,-4}", msg.GetTopService())
                + string.Format("{0,-4}", msg.GetLowService())
                + string.Format("{0,-4}", msg.GetLength())
                + msg.GetContent().InnerXml;
            byte[] buffer = Encoding.UTF8.GetBytes(msgStr);
            mySocket.Send(buffer);
        }
        public Msg ReceiveMsg()
        {
            byte[] byteHead = new byte[4];
            mySocket.Receive(byteHead);
            string strProtocol = Encoding.UTF8.GetString(byteHead).Trim();
            mySocket.Receive(byteHead);
            string strTopS = Encoding.UTF8.GetString(byteHead).Trim();
            mySocket.Receive(byteHead);
            string strLowS = Encoding.UTF8.GetString(byteHead).Trim();
            mySocket.Receive(byteHead);
            string strLen = Encoding.UTF8.GetString(byteHead).Trim();

            byte[] byteBody = new byte[int.Parse(strProtocol)];
            mySocket.Receive(byteBody);
            string strDoc = Encoding.UTF8.GetString(byteBody).Trim();
            strDoc = strDoc.Trim();
            
            Console.WriteLine(strProtocol);
            Console.WriteLine(strTopS);
            Console.WriteLine(strLowS);
            Console.WriteLine(strLen);
            Console.WriteLine(strDoc);
            
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(strDoc);
            return new Msg((EProtocol)int.Parse(strProtocol), (ETopService)int.Parse(strTopS), int.Parse(strLowS), xmldoc);
            
        }
    }
}