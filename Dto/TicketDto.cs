using System.ComponentModel.DataAnnotations;

namespace TicketsB2C.Dto;

public class TicketDto
{
    public TicketDto(int id, double price, string departure, string destination, string transportType, string carrier)
    {
        Id = id;
        Price = price;
        Departure = departure;
        Destination = destination;
        TransportType = transportType;
        Carrier = carrier;
    }

    public int Id { get; set; }
    [Range(0, Double.PositiveInfinity)]
    public double Price { get; set; }
    [Required]
    [MaxLength(100)]
    public string Departure { get; set; }
    [Required]
    [MaxLength(100)]
    public string Destination { get; set; }
    [Required]
    [MaxLength(100)]
    public string TransportType { get; set; }
    [Required]
    [MaxLength(100)]
    public string Carrier { get; set; }
}