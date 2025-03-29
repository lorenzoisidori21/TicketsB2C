namespace TicketsB2C.Models;

public class CheckoutSummary
{
    public CheckoutSummary(int ticketId, City departure, City destination, Carrier carrier, TransportType type, int quantity, double totalAmount)
    {
        TicketId = ticketId;
        Departure = departure;
        Destination = destination;
        Carrier = carrier;
        Type = type;
        Quantity = quantity;
        TotalAmount = totalAmount;
    }

    public int TicketId { get; set; }
    public City Departure { get; set; }
    public City Destination { get; set; }
    public Carrier Carrier { get; set; }
    public TransportType Type { get; set; }
    public int Quantity { get; set; }
    public double TotalAmount { get; set; }
    public bool Success { get; set; } = false;
}