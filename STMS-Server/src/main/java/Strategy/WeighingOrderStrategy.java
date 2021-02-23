package Strategy;

import Data.Food;

public class WeighingOrderStrategy implements IPriceStrategy {
    @Override
    public int quote(Food f) {
        return (f.price*f.foodNum)/100;
    }
}
