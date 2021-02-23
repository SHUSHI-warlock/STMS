package Strategy;

import Data.Food;

public interface IPriceStrategy {
    int quote(Food f);
}
