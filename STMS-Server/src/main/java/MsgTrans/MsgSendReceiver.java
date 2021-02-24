package MsgTrans;

import org.w3c.dom.Document;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import java.io.*;
import java.net.Socket;
import java.nio.charset.StandardCharsets;

public class MsgSendReceiver {
    private final Socket socket;
    public MsgSendReceiver(Socket s) {
        this.socket = s;
    }

    public void SendMsg(Msg myMsg) throws IOException, TransformerException {
        DataOutputStream dos = new DataOutputStream(
                new BufferedOutputStream(socket.getOutputStream()));

        TransformerFactory tf = TransformerFactory.newInstance();
        Transformer t = tf.newTransformer();

        ByteArrayOutputStream bos = new ByteArrayOutputStream();
        t.transform(new DOMSource(myMsg.getContent()), new StreamResult(bos));
        String msgStr = myMsg.getProtocol() + myMsg.getTopService() + myMsg.getLowService() + bos.toString();

        dos.write(msgStr.getBytes(StandardCharsets.UTF_8));
        dos.flush();
    }

    public Msg ReceiveMsg() throws IOException {
        DataInputStream dis = new DataInputStream(
                new BufferedInputStream(socket.getInputStream()));
        byte[] byteHead = new byte[1];
        dis.read(byteHead);
        String strProtocol= new String(byteHead);
        dis.read(byteHead);
        String strTopS = new String(byteHead);
        dis.read(byteHead);
        String strLowS = new String(byteHead);

        byte[] byteBody = new byte[1024];
        dis.read(byteBody);
        String strDoc = new String(byteBody);
        strDoc = strDoc.trim();

        System.out.println(strProtocol);
        System.out.println(strTopS);
        System.out.println(strLowS);
        System.out.println(strDoc);

        StringReader sr = new StringReader(strDoc);
        InputSource is = new InputSource(sr);
        DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
        DocumentBuilder builder;
        Document document = null;
        try {
            builder = factory.newDocumentBuilder();
            document = builder.parse(is);
        } catch (ParserConfigurationException | SAXException e) {
            e.printStackTrace();
        }
        return new Msg(strProtocol,strTopS,strLowS,document);
    }
}
