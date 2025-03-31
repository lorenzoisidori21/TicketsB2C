namespace TicketsB2C.Models;

public class CheckoutSummary
{
    public CheckoutSummary(int ticketId, City departure, City destination, Carrier carrier, TransportType type, int quantity, decimal totalAmount, decimal discountApplied)
    {
        TicketId = ticketId;
        Departure = departure;
        Destination = destination;
        Carrier = carrier;
        Type = type;
        Quantity = quantity;
        TotalAmount = totalAmount;
        DiscountApplied = discountApplied;
    }

    public int TicketId { get; set; }
    public City Departure { get; set; }
    public City Destination { get; set; }
    public Carrier Carrier { get; set; }
    public TransportType Type { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountApplied { get; set; }
    public bool Success { get; set; } = false;
}