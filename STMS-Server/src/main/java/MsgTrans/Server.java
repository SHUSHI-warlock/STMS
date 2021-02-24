package MsgTrans;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class Server {
    public static void main(String[] args) {
        //端口号
        int port = 7000;
        //在端口号上创建一个服务器套接字
        ServerSocket serverSocket = null;
        try {
            serverSocket = new ServerSocket(port);
            //监听来自客户端的连接
            Socket socket = serverSocket.accept();
            MsgSendReceiver msgSendReceiver = new MsgSendReceiver(socket);
            Msg msg = msgSendReceiver.ReceiveMsg();
        } catch (IOException e) {
            e.printStackTrace();
        }

    }
}