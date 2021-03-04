import MsgTrans.ETopService;
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
        boolean flag = true;
        try {
            while (flag) {
                msg = msr.ReceiveMsg();

                switch (msg.getProtocol()) {
                    case EP_Verify:
                        flag = !(LoginVerify(msg)); //这里要取反
                        break;
                    case EP_Disconnect:
                        DisConnect();
                        flag = false;
                        break;
                    case EP_Other:
                        System.out.println("其他");
                        msg.PrintHead();
                        flag = false;
                        break;
                    default:
                        System.out.println("未知的种类");
                        msg.PrintHead();
                        flag = false;
                        break;
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        finally {
            DisConnect();
        }
    }

    /**
     * 协议类型为登录
     */
    private boolean LoginVerify(Msg m) throws Exception {
        ServerHandleFactory factory = ServerHandleFactory.getInstance();
        AbstractServerHandle service = factory.getServerHandle(m.getTopService(), msr);
        try {
            //验证
            if (service.ServiceVerify(m) > 0) {
                //服务进行
                service.ServerHandle();
                DisConnect();
                return true;
            } else
                return false;
        } catch (Exception e) {
            msr.SocketClose();
            System.out.println("服务"+ m.getTopService().toString()+"返回错误！");
            e.printStackTrace();
        }
        DisConnect();
        return false;
    }
    private void DisConnect() {
        try {
            msr.SocketClose();
            System.out.println("连接已断开");
        } catch (IOException e) {
            e.printStackTrace();
            System.out.println("socket断开失败！");
        }

    }
}
