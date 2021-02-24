package MsgTrans;

import org.w3c.dom.Document;

import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import java.io.ByteArrayOutputStream;

public class Msg {
    private EProtocol protocol;
    private ETopService topService;
    private int lowService;
    private int length;
    private Document content;
    public Msg(EProtocol p, ETopService ts, int ls, Document c) throws TransformerException {
        protocol = p;
        topService = ts;
        lowService = ls;
        content = c;
        TransformerFactory tf = TransformerFactory.newInstance();
        Transformer t = tf.newTransformer();
        ByteArrayOutputStream bos = new ByteArrayOutputStream();
        t.transform(new DOMSource(c), new StreamResult(bos));
        length = bos.toString().length();
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

    public int getLength() {
        return length;
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
