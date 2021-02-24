package ServerHandle;
import MsgTrans.ETopService;
import MsgTrans.MsgSendReceiver;
import java.net.Socket;

public class ServerHandleFactory {
    private static ServerHandleFactory instence;
    private ServerHandleFactory(){}

    public static ServerHandleFactory getInstence()
    {
        if (instence == null){
            instence = new ServerHandleFactory();
        }
        return instence;
    }

    public AbstractServerHandle getServerHandle(ETopService ServerType, Socket s, MsgSendReceiver m, String id)
    {
        switch (ServerType) {
            case ET_DKJ:
                return new DKJServerHandle(id,s,m);
            case ET_YTJ:
                return new YTJServerHandle(id,s,m);
            case ET_KGL:
                return new KGLServerHandle(s,m);
            case ET_DGL:
                return new DGLServerHandle(s,m);
            default:
                return null;
        }
    }
}
