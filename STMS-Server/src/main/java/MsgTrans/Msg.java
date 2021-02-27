package MsgTrans;

import org.w3c.dom.Document;

import javax.xml.transform.*;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import java.io.ByteArrayOutputStream;

public class Msg {
    private EProtocol protocol;
    private ETopService topService;
    private int lowService;
    private Document content;
    public Msg(EProtocol p, ETopService ts, int ls, Document c) throws TransformerException {
        protocol = p;
        topService = ts;
        lowService = ls;
        content = c;
/*      不需要在此计算lengh，收发都由msr计算
        TransformerFactory tf = TransformerFactory.newInstance();
        Transformer t = tf.newTransformer();
        //设置编码方式
        t.setOutputProperty(OutputKeys.ENCODING,"gb2312");

        //指示是否要在转输出时添加XML声明
        t.setOutputProperty(OutputKeys.OMIT_XML_DECLARATION, "no");
        ByteArrayOutputStream bos = new ByteArrayOutputStream();
        t.transform(new DOMSource(c), new StreamResult(bos));
        length = bos.toString().length();
*/
    }

    public EProtocol getProtocol() {
        return protocol;
    }

    public ETopService getTopService() {
        return topService;
    }

    public int getLowService() {
        return lowService;
    }


    public void PrintHead() {
        System.out.println("Protocol : " + protocol);
        System.out.println("topService : " + topService);
        System.out.println("lowService : " + lowService);
        System.out.println("content : " + content);
    }

    public Document getContent() {
        return content;
    }
}
