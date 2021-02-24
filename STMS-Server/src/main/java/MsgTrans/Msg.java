package MsgTrans;

import org.w3c.dom.Document;


public class Msg {
    private int protocol;
    private int topService;
    private int lowService;
    private Document content;
    public Msg(int p, int ts, int ls, Document c)
    {
        protocol = p;
        topService = ts;
        lowService = ls;
        content = c;
    }

    public int getProtocol() {
        return protocol;
    }

    /**
     * Protocol的类型：
     * 0.验证登录
     * 2.
     */

    public int getLowService() {
        return lowService;
    }

    public int getTopService() {
        return topService;
    }

    public Document getContent() {
        return content;
    }
}
