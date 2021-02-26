package ServerHandle;
import MsgTrans.ETopService;
import MsgTrans.MsgSendReceiver;
import java.net.Socket;

public class ServerHandleFactory {
    private static ServerHandleFactory instance;
    private ServerHandleFactory(){}

    public static ServerHandleFactory getInstance()
    {
        if (instance == null){
            instance = new ServerHandleFactory();
        }
        return instance;
    }

    public AbstractServerHandle getServerHandle(ETopService ServerType, Socket s, MsgSendReceiver m, String id)
    {
        return switch (ServerType) {
            case ET_DKJ -> new DKJServerHandle(id, m);//打卡机 店铺ID登录
            case ET_YTJ -> new YTJServerHandle(id, m);//一体机 卡ID登录
            case ET_KGL -> new KGLServerHandle(m);//卡管理 管理员登录
            case ET_DGL -> new DGLServerHandle(m);//店管理 管理员登录
        };
    }
}
