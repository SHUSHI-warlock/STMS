import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;
import ServerHandle.AbstractServerHandle;
import ServerHandle.ServerHandleFactory;

import javax.xml.transform.TransformerException;
import java.io.IOException;
import java.net.Socket;

///分类器，根据不同消息创建对应服务
public class Classifier implements Runnable{

    private final Socket clientSocket;
    private final MsgSendReceiver  msr;
    Classifier(Socket cs)
    {
        clientSocket = cs;
        msr = new MsgSendReceiver(clientSocket);
    }

    @Override
    public void run() {
        ///获取消息
        Msg msg = null;
        try {
            msg = msr.ReceiveMsg();

            switch (msg.getProtocol()) {
                case EP_Verify:
                    LoginVerify(msg);
                case EP_Disconnect:
                    DisConnect();
                case EP_Other:
                    System.out.println("其他");
                default:
                    System.out.println("未知的种类");
            }
        } catch (Exception e) {
            System.out.println("异常信息如下：");
            e.getMessage();
            e.printStackTrace();
        }
    }

    /**
     * 协议类型为登录
     */
    private void LoginVerify(Msg m) throws Exception{
        ServerHandleFactory factory = ServerHandleFactory.getInstance();
        AbstractServerHandle service = factory.getServerHandle(m.getTopService(), msr);
        try {
            //验证
            if (service.ServiceVerify(m) > 0) {
                //服务进行
                service.ServerHandle();
            }
        } catch (Exception e) {
            clientSocket.shutdownInput();
            clientSocket.shutdownOutput();
            clientSocket.close();
            System.out.println("异常信息如下：");
            e.getMessage();
            e.printStackTrace();
        }
    }
    private void DisConnect() throws Exception {
        clientSocket.shutdownInput();
        clientSocket.shutdownOutput();
        clientSocket.close();
    }
}
