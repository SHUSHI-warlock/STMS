package MsgTrans;

import org.dom4j.Document;

import javax.xml.crypto.dsig.XMLObject;

public class Msg {
    private ProtocolType protocol;
    private String topService;
    private String lowService;
    private Document content;
    public Msg(ProtocolType p,String ts,String ls,Document c)
    {
        protocol = p;
        topService = ts;
        lowService = ls;
        content = c;
    }

    public ProtocolType getProtocol() {
        return protocol;
    }

    public Document getContent() {
        return content;
    }

    public String getLowService() {
        return lowService;
    }

    public String getTopService() {
        return topService;
    }
}
