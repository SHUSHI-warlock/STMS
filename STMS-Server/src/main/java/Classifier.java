import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;
import ServerHandle.AbstractServerHandle;
import ServerHandle.ServerHandleFactory;

import java.io.IOException;
import java.net.Socket;

///分类器，根据不同消息创建对应服务
public class Classifier implements Runnable{

    private Socket clientSocket;
    private MsgSendReceiver msr;
    Classifier(Socket cs)
    {
        clientSocket = cs;
        msr = new MsgSendReceiver(clientSocket);
    }

    @Override
    public void run() {
        try {
            ///获取消息
            Msg msg = msr.ReceiveMsg();
            switch (msg.getProtocol())
            {
                case EP_Verify:
                    LoginVerify(msg);
                case EP_Disconnect:
                    break;
                case EP_Other:
                    System.out.println("其他");
                default:
                    System.out.println("未知的种类");
            }

        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            clientSocket.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /**
     * 协议类型为登录
     */
    private void LoginVerify(Msg m){
        String id = null;
        ServerHandleFactory factory = ServerHandleFactory.getInstence();
        AbstractServerHandle service = factory.getServerHandle(m.getTopService(), clientSocket, msr, id);

        //验证
        if(service.ServiceVerify(m)>0){
            //服务进行
            service.ServerHandle();
        }
    }

}
