namespace DelegatesEvents.Section2;

public static class PriceMethods
{
    public static decimal CalculateTotal(Order order) => order.Price * order.Quantity;

    public static decimal CalculateTotalWithDiscount(Order order)
    {
        decimal total = order.Price * order.Quantity;
        decimal discount = total * 0.10m;
        return total - discount;
    }
}
