using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TicketsB2C.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }
    [Range(0, Double.PositiveInfinity)]
    [Precision(6,2)]
    public double Price { get; set; }
    [Required]
    [MaxLength(100)]
    public int DepartureId { get; set; }
    [Required]
    [MaxLength(100)]
    public int DestinationId { get; set; }
    [Required]
    [MaxLength(100)]
    public int TypeId { get; set; }
    [Required]
    [MaxLength(100)]
    public int CarrierId { get; set; }

    // navigation props
    public City Departure { get; set; }
    public City Destination { get; set; }
    public TransportType Type { get; set; }
    public Carrier Carrier { get; set; }
}