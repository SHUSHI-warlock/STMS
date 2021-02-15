package ServerHandle;

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

    public AbstractServerHandle getServerHandle(String ServerType, Socket s, MsgSendReceiver m,String id)
    {
        switch (ServerType) {
            case "DKJ":
                return new DKJServerHandle(id,s,m);
            case "YTJ":
                return new YTJServerHandle(id,s,m);
            case "KGL":
                return new KGLServerHandle(s,m);
            case "DGL":
                return new DGLServerHandle(s,m);
            default:
                return null;
        }
    }
}
