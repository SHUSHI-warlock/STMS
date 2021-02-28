using System.Net.Sockets;

namespace MsgTransTest
{
    //统一使用MsgSendReceiver
    //通过调用GetTransXXX进行传输
    class SingleTrans
    {
        private readonly MsgSendReceiver msgSendReceiver;
        private readonly TransDGL transDGL;
        private readonly TransDKJ transDKJ;
        private readonly TransKGL transKGL;
        private readonly TransYTJ transYTJ;
        public SingleTrans(Socket socket)
        {
            this.msgSendReceiver = new MsgSendReceiver(socket);
            this.transDGL = new TransDGL(msgSendReceiver);
            this.transDKJ = new TransDKJ(msgSendReceiver);
            this.transKGL = new TransKGL(msgSendReceiver);
            this.transYTJ = new TransYTJ(msgSendReceiver);
        }
        public TransDGL GetTransDGL()
        {
            return transDGL;
        }
        public TransDKJ GetTransDKJ()
        {
            return transDKJ;
        }
        public TransKGL GetTransKGL()
        {
            return transKGL;
        }
        public TransYTJ GetTransYTJ()
        {
            return transYTJ;
        }
        public MsgSendReceiver GetMSR()
        {
            return msgSendReceiver;
        }
    }
}
