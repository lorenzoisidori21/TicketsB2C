using System.ComponentModel.DataAnnotations;

namespace TicketsB2C.Models;

public class Carrier
{
    public Carrier(int id, string name)
    {
        Id = id;
        Name = name;
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}