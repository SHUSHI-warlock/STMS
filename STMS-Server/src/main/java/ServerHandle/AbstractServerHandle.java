package ServerHandle;

import MsgTrans.MsgSendReceiver;

import java.net.Socket;

public abstract class AbstractServerHandle {
    protected Socket clientSocket;
    protected MsgSendReceiver msr;
    public abstract void ServerHandle();
}
