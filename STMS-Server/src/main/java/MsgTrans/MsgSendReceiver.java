package MsgTrans;

import org.w3c.dom.Document;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerException;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import java.io.*;
import java.net.Socket;
import java.nio.charset.StandardCharsets;

public class MsgSendReceiver {

    private final Socket socket;

    public MsgSendReceiver(Socket s) {
        this.socket = s;
    }

    public void SendMsg(Msg myMsg) throws IOException, TransformerException {
        if(SocketTest()) {
            DataOutputStream dos = new DataOutputStream(
                    new BufferedOutputStream(socket.getOutputStream()));

            TransformerFactory tf = TransformerFactory.newInstance();
            Transformer t = tf.newTransformer();
            ByteArrayOutputStream bos = new ByteArrayOutputStream();

            //指示是否要在转输出时添加XML声明  no是添加 yes是不添加
            t.setOutputProperty(OutputKeys.OMIT_XML_DECLARATION, "yes");
            t.transform(new DOMSource(myMsg.getContent()), new StreamResult(bos));
            int length = bos.toByteArray().length;

            String msgStr = String.format("%-4d", myMsg.getProtocol().getIndex())
                    + String.format("%-4d", myMsg.getTopService().getIndex())
                    + String.format("%-4d", myMsg.getLowService())
                    + String.format("%-4d", length)
                    + bos.toString();

            System.out.println(msgStr);

            dos.write(msgStr.getBytes(StandardCharsets.UTF_8));
            dos.flush();
        } else {
            throw new IOException("连接断开！");
        }
    }

    public Msg ReceiveMsg() throws IOException, TransformerException {
        DataInputStream dis = new DataInputStream(
                new BufferedInputStream(socket.getInputStream()));
        //读取msg头部
        byte[] byteHead = new byte[4];
        dis.read(byteHead);
        String strProtocol= new String(byteHead).trim();
        dis.read(byteHead);
        String strTopS = new String(byteHead).trim();
        dis.read(byteHead);
        String strLowS = new String(byteHead).trim();
        dis.read(byteHead);
        String strLen = new String(byteHead).trim();
        //读取XML
        byte[] byteBody = new byte[Integer.parseInt(strLen)];
        dis.read(byteBody);
        String strDoc = new String(byteBody);
        strDoc = strDoc.trim();

        System.out.println(strProtocol);
        System.out.println(strTopS);
        System.out.println(strLowS);
        System.out.println(strLen);
        System.out.println(strDoc);
        //Str转XML
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
        return new Msg(EProtocol.getEP(Integer.parseInt(strProtocol)),
                ETopService.getET(Integer.parseInt(strTopS)),
                Integer.parseInt(strLowS),
                document);
    }

    public Boolean SocketTest() {
        try {
            socket.sendUrgentData(0xFF);//发送1个字节的紧急数据，默认情况下，服务器端没有开启紧急数据处理，不影响正常通信
            return true;
        } catch (IOException se) {
            return false;
        }
    }

    public void SocketClose() throws IOException {
        this.socket.shutdownInput();
        this.socket.shutdownOutput();
        this.socket.close();
    }
}