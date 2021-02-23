package Data;

public class FoodOrder {
    int foodid;
    int storeid;
    int count;//该食物的数量


    public FoodOrder(int foodid, int storeid, int count) {
        this.foodid = foodid;
        this.storeid = storeid;
        this.count = count;
    }

    public int getFoodid() {
        return foodid;
    }

    public void setFoodid(int foodid) {
        this.foodid = foodid;
    }

    public int getStoreid() {
        return storeid;
    }

    public void setStoreid(int storeid) {
        this.storeid = storeid;
    }

    public int getCount() {
        return count;
    }

    public void setCount(int count) {
        this.count = count;
    }
}
