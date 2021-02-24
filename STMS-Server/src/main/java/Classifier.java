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
            ///协议头处理
            String topS = msg.getTopService();

            ///分类服务
            String id = "null";
            String serverType = "null";
            //用工厂创建对象

            ServerHandleFactory factory = ServerHandleFactory.getInstence();
            AbstractServerHandle service = factory.getServerHandle(serverType, clientSocket, msr, id);
            //服务进行
            service.ServerHandle();

        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            clientSocket.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
