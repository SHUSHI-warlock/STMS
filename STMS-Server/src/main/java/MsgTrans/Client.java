package MsgTrans;

import org.w3c.dom.Document;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.TransformerException;
import java.io.IOException;
import java.io.StringReader;
import java.net.Socket;

public class Client {
    public static void main(String[] args) throws IOException, TransformerException {
        int port = 7000;
        String host = "localhost";
        //创建一个套接字并将其连接到指定端口号
        Socket socket = null;
        try {
            socket = new Socket(host,port);
        } catch (IOException e) {
            e.printStackTrace();
        }
        String str = "<abc>123汪帮传</abc>";
        StringReader sr = new StringReader(str);
        InputSource is = new InputSource(sr);
        DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
        DocumentBuilder builder = null;
        Document document = null;
        try {
            builder = factory.newDocumentBuilder();
            document = builder.parse(is);
        } catch (ParserConfigurationException e) {
            e.printStackTrace();
        } catch (SAXException e) {
            e.printStackTrace();
        }
        Msg msg = new Msg(1,10,110,document);
        MsgSendReceiver msgSendReceiver = new MsgSendReceiver(socket);
        msgSendReceiver.SendMsg(msg);
    }
}