using DelegatesEvents.Section1;
using DelegatesEvents.Section2;

namespace DelegatesEvents;

internal class Program
{
    static void Handler1(Order order) =>
        Console.WriteLine($"[Handler1] Order {order.Id} processed. Total = {order.FinalPrice:C}");

    static void Handler2(Order order) =>
        Console.WriteLine($"[Handler2] Notification sent to {order.CustomerName}.");

    static void Handler3(Order order) =>
        Console.WriteLine($"[Handler3] Audit: order {order.Id} completed at {DateTime.Now:T}.");

    static void Main(string[] args)
    {
        Console.WriteLine("################ SECTION 1 ################");
        Section1Demo.Run();

        Console.WriteLine();
        Console.WriteLine("################ SECTION 2 ################");

        Order order = new Order { Id = 1, CustomerName = "Ahmed", Price = 100m, Quantity = 3 };

        // ---------- Part 1: user-defined delegate ----------
        Console.WriteLine("--- Part 1 ---");
        Console.WriteLine(OrderCalculator.CalculateOrderPrice(order, PriceMethods.CalculateTotal));              // 300
        Console.WriteLine(OrderCalculator.CalculateOrderPrice(order, PriceMethods.CalculateTotalWithDiscount)); // 270

        // ---------- Part 2: Func<> ----------
        Console.WriteLine("--- Part 2 ---");
        Console.WriteLine(OrderCalculator.CalculateOrderPriceWithFunc(order, x => x.Price * x.Quantity));
        Console.WriteLine(OrderCalculator.CalculateOrderPriceWithFunc(order, x => x.Price * x.Quantity * 0.8m));

        // ---------- Part 3: Predicate<> ----------
        Console.WriteLine("--- Part 3 ---");
        Predicate<Order> hasQuantity = o => o.Quantity > 0;
        Predicate<Order> hasPrice = o => o.Price > 0;
        Predicate<Order> hasCustomer = o => !string.IsNullOrWhiteSpace(o.CustomerName);

        Console.WriteLine(OrderValidator.ValidateOrder(order, hasQuantity));
        Console.WriteLine(OrderValidator.ValidateOrder(order, hasPrice));
        Console.WriteLine(OrderValidator.ValidateOrder(order, hasCustomer));
        Console.WriteLine(OrderValidator.ValidateOrder(order, o => o.Quantity > 10));   // false

        // ---------- Part 4: Action<> ----------
        Console.WriteLine("--- Part 4 ---");
        Action<Order> printOrder = o => Console.WriteLine($"Order {o.Id} processed.");
        Action<Order> sendConfirmation = o => Console.WriteLine($"Confirmation sent to {o.CustomerName}.");
        Action<Order> writeAudit = o => Console.WriteLine($"AUDIT: order {o.Id} at {DateTime.Now:T}");

        OrderProcessor.ProcessOrder(order, printOrder);
        OrderProcessor.ProcessOrder(order, sendConfirmation);
        OrderProcessor.ProcessOrder(order, writeAudit);

        // ---------- Part 5 + 6: Events (subscribe / unsubscribe) ----------
        Console.WriteLine("--- Part 5 & 6 ---");
        OrderService service = new OrderService();

        service.OrderProcessed += Handler1;
        service.OrderProcessed += Handler2;
        service.OrderProcessed += Handler3;

        Console.WriteLine("All 3 handlers subscribed:");
        service.ProcessOrder(order);

        service.OrderProcessed -= Handler1;
        Console.WriteLine("After unsubscribing Handler1:");
        service.ProcessOrder(order);

        // ---------- Part 7: Final flow ----------
        Console.WriteLine("--- Part 7 ---");
        Predicate<Order>[] rules = { hasQuantity, hasPrice, hasCustomer };

        Order finalOrder = new Order { Id = 2, CustomerName = "Sara", Price = 250m, Quantity = 2 };
        bool isValid = rules.All(rule => OrderValidator.ValidateOrder(finalOrder, rule));

        if (isValid)
        {
            service.OrderProcessed += Handler1;
            service.ProcessOrder(finalOrder);
        }

        Console.WriteLine("--- Bonus ---");
        var strategies = new (string Name, Func<Order, decimal> Strategy)[]
        {
            ("Normal",       PricingStrategies.Normal),
            ("10% discount", PricingStrategies.TenPercent),
            ("20% discount", PricingStrategies.TwentyPercent),
            ("VIP",          PricingStrategies.Vip)
        };

        foreach (var (name, strategy) in strategies)
        {
            OrderService pricedService = new OrderService(strategy);
            pricedService.OrderProcessed += o => Console.WriteLine($"{name,-13}: {o.FinalPrice:C}");
            pricedService.ProcessOrder(new Order { Id = 3, CustomerName = "VIP Customer", Price = 200m, Quantity = 5 });
        }
    }
}
