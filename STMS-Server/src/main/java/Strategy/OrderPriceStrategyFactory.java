package Strategy;

public class OrderPriceStrategyFactory {

    public static IPriceStrategy getStrategy(String Strategy)
    {
        return switch (Strategy) {
            case "single" -> new SingleOrderPriceStrategy();
            case "weight" -> new WeighingOrderStrategy();
            default -> null;
        };
    }
}
