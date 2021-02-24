package MsgTrans;

public enum ETopService {
    ET_DKJ(0),//打卡机
    ET_YTJ(1),//一体机
    ET_DGL(2),//店铺管理
    ET_KGL(3);//卡管理

    private int index;

    private ETopService(int index) {
        this.index = index;
    }

    public int getIndex() {
        return this.index;
    }

    public static ETopService getET(int index) {
        for (ETopService c : ETopService.values()) {
            if (c.getIndex() == index) {
                return c;
            }
        }
        return null;
    }
}
