package Data;

import Strategy.IPriceStrategy;
import Strategy.OrderPriceStrategyFactory;

public class Food {
    public String id;
    public String foodClass;    //菜品类型
    public String st;           //IPriceStrategy
    public String name;
    public int price;
    public String foodTip;
    public int foodNum;

    public Food() {
    }

    public Food(String id, String foodClass, String st, String name, int price, String foodTip) {
        this.id = id;
        this.foodClass = foodClass;
        this.st = st;
        this.name = name;
        this.price = price;
        this.foodTip = foodTip;
    }

    /**
     * 计算菜价
     *
     * @return 未初始化返回-1，价格策略错误-2，否则返回该菜品价格
     */
    public int CalculatePrice() {
        try {
            if (id == "" || foodNum < 0)
                return -1;

            IPriceStrategy strategy = OrderPriceStrategyFactory.getStrategy(st);
            if (strategy == null) {
                return -2;
            }
            return strategy.quote(this);
        } catch (Exception e) {
            e.printStackTrace();
            System.out.println("价格计算出错！");
            return -1;
        }
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

    public void setFoodNum(int foodNum) {
        this.foodNum = foodNum;
    }

    public int getFoodNum() {
        return foodNum;
    }
}

