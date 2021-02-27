package ServerHandle;
import MsgTrans.ETopService;
import MsgTrans.MsgSendReceiver;


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

    public AbstractServerHandle getServerHandle(ETopService ServerType, MsgSendReceiver m)
    {
        switch (ServerType) {
            case ET_DKJ -> { return new DKJServerHandle(m); }
            case ET_YTJ -> { return new YTJServerHandle(m); }
            case ET_KGL -> { return new KGLServerHandle(m); }
            case ET_DGL -> { return new DGLServerHandle(m); }
            default -> {
                System.out.println("接收到了奇怪的服务类型？！");
                return null; }
        }

    }
}
