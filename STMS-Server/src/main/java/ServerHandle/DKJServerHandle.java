package ServerHandle;

import Data.Bill;
import MsgTrans.DKJMsgParse;
import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;

import java.net.Socket;

public class DKJServerHandle extends AbstractServerHandle {
    private DKJMsgParse msgParse;
    private String storeId;
    private Bill tempBill;

    public DKJServerHandle(Socket s, MsgSendReceiver m)
    {
        clientSocket = s;
        msr = m;
    }

    public DKJServerHandle(String id, Socket s, MsgSendReceiver m)
    {
        clientSocket = s;
        msr = m;
        storeId = id;
    }

    @Override
    public void ServerHandle() {

    }

    /**
     * 计算价格
     * @param m
     */
    private void CaculatePrice(Msg m)
    {

    }

    /**
     * 尝试交易
     *
     */
    private int Paying(Msg m)
    {
        return 0;
    }

    /**
     * 发送该店菜单
     *
     * @param m
     */
    private void SendOrder(Msg m)
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
