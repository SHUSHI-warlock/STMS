package MsgTrans;

import java.net.Socket;

public class MsgSendReceiver {
    private Socket clientSocket ;
    public MsgSendReceiver(Socket s){
        clientSocket = s;
    }
    /**判断是否有消息
     * 有则返回true，否则返回false
     * */
    public Boolean hasMsg(){
        return true;
    }

    /**读取一条消息
     * 如果当前没有消息，返回null
     * 否则返回消息
     *
     */
    public Msg readMsg(){
        return null;
    }

    /**发送消息
     *
     *
     *
     */
    public void sendMsg(Msg mess){

    }
}
