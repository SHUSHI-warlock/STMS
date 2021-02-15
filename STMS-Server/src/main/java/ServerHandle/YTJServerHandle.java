package ServerHandle;

import Data.Label;
import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;

import java.net.Socket;

public class YTJServerHandle extends AbstractServerHandle{
    public Label user;
    public YTJServerHandle(Socket s, MsgSendReceiver m)
    {
        clientSocket = s;
        msr = m;
        user = new Label();
    }
    public YTJServerHandle(String Lid, Socket s, MsgSendReceiver m)
    {
        clientSocket = s;
        msr = m;
        user = new Label();
        Label.id = Lid;
    }

    @Override
    public void ServerHandle() {

    }

    private void SendLabel(Msg m)
    {

    }

    private void ChangeLabel(Msg m)
    {

    }

    private void SendBill(Msg m)
    {

    }

    private void Recharge(Msg m)
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
