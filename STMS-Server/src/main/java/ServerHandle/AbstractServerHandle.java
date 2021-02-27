package ServerHandle;

import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;

import java.net.Socket;

public abstract class AbstractServerHandle {
    protected MsgSendReceiver msr;
    public abstract int ServiceVerify(Msg m);
    public abstract void ServerHandle();
}
