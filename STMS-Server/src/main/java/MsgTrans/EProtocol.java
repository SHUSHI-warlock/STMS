package MsgTrans;

public enum EProtocol {
    EP_Verify(0),//验证
    EP_Request(1),//请求
    EP_Put(2),//提交
    EP_Return(3),//返回
    EP_Disconnect(4),//断开
    EP_Other(5);//其他

    private int index;

    private EProtocol(int index) {
        this.index = index;
    }

    public int getIndex() {
        return this.index;
    }

    public static EProtocol getEP(int index) {
        for (EProtocol c : EProtocol.values()) {
            if (c.getIndex() == index) {
                return c;
            }
        }
        return null;
    }
}
