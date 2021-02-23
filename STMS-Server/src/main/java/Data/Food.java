package Data;

import Strategy.IPriceStrategy;
import Strategy.OrderPriceStrategyFactory;

public class Food {

    public Food() {}

    public String id;
    public String foodClass;    //菜品类型
    public String st;           //IPriceStrategy
    public String name;
    public int price;
    public String foodTip;
    public int foodNum;


    /**
     * 计算菜价
     *
     * @return 未初始化返回-1，否则返回该菜品价格
     */
    public int CalculatePrice() {
        if (price < 0 || foodNum < 0)
            return -1;
        IPriceStrategy strategy = OrderPriceStrategyFactory.getStrategy(st);
        return strategy.quote(this);
    }

    public Food(String id, String foodClass, String st, String name, int price, String foodTip) {
        this.id = id;
        this.foodClass = foodClass;
        this.st = st;
        this.name = name;
        this.price = price;
        this.foodTip = foodTip;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getFoodClass() {
        return foodClass;
    }

    public void setFoodClass(String foodClass) {
        this.foodClass = foodClass;
    }

    public String getSt() {
        return st;
    }

    public void setSt(String st) {
        this.st = st;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getPrice() {
        return price;
    }

    public void setPrice(int price) {
        this.price = price;
    }

    public String getFoodTip() {
        return foodTip;
    }

    public void setFoodTip(String foodTip) {
        this.foodTip = foodTip;

    }
}

