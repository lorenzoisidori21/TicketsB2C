using System.ComponentModel.DataAnnotations;

namespace TicketsB2C.Models;

public class TransportType
{
    public TransportType(int id, string description)
    {
        Id = id;
        Description = description;
    }

    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}