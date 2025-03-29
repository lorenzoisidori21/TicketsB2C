using System.ComponentModel.DataAnnotations;

namespace TicketsB2C.Models;

public class City
{
    public City(int id, string name)
    {
        Id = id;
        Name = name;
    }

    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
}