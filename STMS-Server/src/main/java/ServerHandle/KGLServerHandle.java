package ServerHandle;

import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;

import java.net.Socket;

public class KGLServerHandle extends AbstractServerHandle{
    KGLServerHandle(Socket s, MsgSendReceiver m)
    {
        clientSocket = s;
        msr = m;
    }

    @Override
    public void ServerHandle() {

    }

    private void SendLabels(Msg m)
    {

    }

    private void ChangeLabel(Msg m)
    {

    }

    private void CreateLabel(Msg m)
    {

    }

    private void DeleteLabel(Msg m)
    {

    }

    private void SendBill(Msg m)
    {

    }

    private void CloseSocket(){
        try {
            clientSocket.close();
        }
        catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }
}
