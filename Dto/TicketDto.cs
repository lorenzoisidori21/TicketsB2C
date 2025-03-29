using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TicketsB2C.Dto;

public class TicketDto
{
    public int Id { get; set; }
    [Range(0, Double.PositiveInfinity)]
    [Precision(6, 2)]
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