package MsgTrans;

import org.w3c.dom.Document;


public class Msg {
    private String protocol;
    private String topService;
    private String lowService;
    private Document content;
    public Msg(String p, String ts, String ls, Document c)
    {
        protocol = p;
        topService = ts;
        lowService = ls;
        content = c;
    }

    public String getProtocol() {
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
