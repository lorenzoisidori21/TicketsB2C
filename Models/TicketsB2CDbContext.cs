using Microsoft.EntityFrameworkCore;

namespace TicketsB2C.Models;

public class TicketsB2CDbContext : DbContext
{
    public TicketsB2CDbContext(DbContextOptions<TicketsB2CDbContext> options) : base(options) { }
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<TransportType> TransportTypes { get; set; } = null!;
    public DbSet<Carrier> Carriers { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>()
            .HasKey(city => city.Id);
        modelBuilder.Entity<TransportType>()
            .HasKey(transportType => transportType.Id);
        modelBuilder.Entity<Carrier>()
            .HasKey(carrier => carrier.Id);
        modelBuilder.Entity<Ticket>()
            .HasKey(ticket => ticket.Id);
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Departure);
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Destination);
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Type);
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Carrier);
    }
}