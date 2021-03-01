package ServerHandle;

import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;

import java.net.Socket;

public abstract class AbstractServerHandle {
    protected MsgSendReceiver msr;
    public abstract int ServiceVerify(Msg m) throws Exception;
    public abstract void ServerHandle() throws Exception;
}
