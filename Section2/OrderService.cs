namespace DelegatesEvents.Section2;

public class OrderService
{
    private readonly Func<Order, decimal> _pricingStrategy;

    public event Action<Order>? OrderProcessed;

    public OrderService(Func<Order, decimal>? pricingStrategy = null)
    {
        _pricingStrategy = pricingStrategy ?? PricingStrategies.Normal;
    }

    public void ProcessOrder(Order order)
    {
        order.FinalPrice = _pricingStrategy(order);
        order.IsProcessed = true;

        OrderProcessed?.Invoke(order);
    }
}

public static class PricingStrategies
{
    public static Func<Order, decimal> Discount(decimal percent) =>
        order => order.Price * order.Quantity * (1 - percent / 100m);

    public static readonly Func<Order, decimal> Normal = order => order.Price * order.Quantity;
    public static readonly Func<Order, decimal> TenPercent = Discount(10);
    public static readonly Func<Order, decimal> TwentyPercent = Discount(20);
    public static readonly Func<Order, decimal> Vip = Discount(30);
}
