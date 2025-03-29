using System.ComponentModel.DataAnnotations;

namespace TicketsB2C.Models;

public class Ticket
{
    public Ticket(int id, double price, int departure, int destination, int type, int carrier)
    {
        Id = id;
        Price = price;
        Departure = departure;
        Destination = destination;
        Type = type;
        Carrier = carrier;
    }
    [Key]
    public int Id { get; set; }
    public double Price { get; set; }
    public int Departure { get; set; }
    public int Destination { get; set; }
    public int Type { get; set; }
    public int Carrier { get; set; }
}