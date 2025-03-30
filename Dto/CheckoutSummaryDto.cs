namespace TicketsB2C.Dto;

public class CheckoutSummaryDto
{
    public CheckoutSummaryDto(int ticketId, string departure, string destination, string carrier, string type, int quantity, decimal totalAmount, decimal discountApplied, bool success)
    {
        TicketId = ticketId;
        Departure = departure;
        Destination = destination;
        Carrier = carrier;
        Type = type;
        Quantity = quantity;
        TotalAmount = totalAmount;
        DiscountApplied = discountApplied;
        Success = success;
    }

    public int TicketId { get; set; }
    public string Departure { get; set; }
    public string Destination { get; set; }
    public string Carrier { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountApplied { get; set; }
    public bool Success { get; set; }
}