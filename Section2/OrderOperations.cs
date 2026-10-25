namespace DelegatesEvents.Section2;

public static class OrderCalculator
{
    public static decimal CalculateOrderPrice(Order order, PriceCalculator calculator) => calculator(order);

    public static decimal CalculateOrderPriceWithFunc(Order order, Func<Order, decimal> calculator) => calculator(order);
}

public static class OrderValidator
{
    // Part 3
    public static bool ValidateOrder(Order order, Predicate<Order> validationRule) => validationRule(order);
}

public static class OrderProcessor
{
    public static void ProcessOrder(Order order, Action<Order> action)
    {
        order.IsProcessed = true;
        action(order);
    }
}
