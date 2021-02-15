import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.nio.charset.StandardCharsets;

public class STServer {
    private static int PORT = 7000; //端口号
    public static void main(String[] args)throws IOException {
        //1.先开启营业额结算服务


        //在端口号上创建一个服务器套接字
        ServerSocket serverSocket = new ServerSocket(PORT);

        int counter = 1;
        while (true){
            Socket socket = serverSocket.accept();
            System.out.println("第 "+(counter++)+" 个连接");

            //开启线程执行
            Thread t = new Thread(new Classifier(socket));
            t.start();
        }
    }
}
