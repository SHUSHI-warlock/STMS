package Data;

import Strategy.IPriceStrategy;
import Strategy.OrderPriceStrategyFactory;

public class Food {
    public Food(){}
    public String id;
    public String foodClass;    //菜品类型
    public String st;           //IPriceStrategy
    public String name;
    public int price;
    public String foodTip;
    public int foodNum;

    /**
     *  计算菜价
     * @return 未初始化返回-1，否则返回该菜品价格
     */
    public int CalculatePrice()
    {
        if(price<0||foodNum<0)
            return -1;
        IPriceStrategy strategy = OrderPriceStrategyFactory.getStrategy(st);
        return strategy.quote(this);
    }
}
