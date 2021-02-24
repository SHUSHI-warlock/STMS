package MsgTrans;

import org.w3c.dom.Document;


public class Msg {
    private EProtocol protocol;
    private ETopService topService;
    private int lowService;
    private Document content;
    public Msg(EProtocol p, ETopService ts, int ls, Document c)
    {
        protocol = p;
        topService = ts;
        lowService = ls;
        content = c;
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

    public Document getContent() {
        return content;
    }
}
