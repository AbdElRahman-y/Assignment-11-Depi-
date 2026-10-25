namespace DelegatesEvents.Section2;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public decimal FinalPrice { get; set; }
    public bool IsProcessed { get; set; }
}

// Part 1: user-defined delegate
public delegate decimal PriceCalculator(Order order);
