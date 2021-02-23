package Strategy;

import Data.Food;

public class SingleOrderPriceStrategy implements IPriceStrategy {
    @Override
    public int quote(Food f) {
        return f.price*f.foodNum;
    }
}
