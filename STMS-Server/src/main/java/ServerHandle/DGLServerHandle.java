package ServerHandle;

import MsgTrans.Msg;
import MsgTrans.MsgSendReceiver;

import java.net.Socket;

public class DGLServerHandle extends AbstractServerHandle {

    public DGLServerHandle(Socket s, MsgSendReceiver m){
        clientSocket = s;
        msr = m;
    }

    @Override
    public void ServerHandle() {

    }

    /**
     * 根据消息，发送店铺列表
     * @param m
     */
    private void SendStores(Msg m){

    }

    /**
     * 修改店铺
     *
     */
    private void ChangeStore(Msg m){

    }

    /**
     * 新建店铺
     *
     */
    private void CreateStore(Msg m) {
    }

    /**
     * 删除店铺
     *
     * @param m
     */
    private  void DeleteStore(Msg m){

    }

    /**
     * 发送某店铺的菜单
     *
     * @param m
     */
    private void SendOrder(Msg m){

    }

    /**
     * 修改菜品
     *
     * @param m
     */
    private void ChnageFood(Msg m){

    }

    /**
     * 创建菜品
     *
     *
     */
    private void CreateFood(Msg m){

    }

    /**
     * 删除菜品
     *
     */
    private void DeleteFood(Msg m){

    }

    /**
     * 发送某店消费账单
     *
     */
    private void SendBill(Msg m){

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
