using Microsoft.EntityFrameworkCore;
using TicketsB2C.Models;

namespace TicketsB2C.DataAccess;

public class TicketsDal : ITicketsDal
{
    private readonly TicketsB2CDbContext _context;

    public TicketsDal(TicketsB2CDbContext ctx)
    {
        _context = ctx;
    }

    public async Task<IList<Ticket>> GetTickets()
    {
        return await _context.Tickets
            .Include(t => t.Carrier)
            .Include(t => t.Destination)
            .Include(t => t.Departure)
            .Include(t => t.Type)
            .ToListAsync();
    }

    public async Task<IList<Ticket>> GetTicketsByCarrier(string carrier)
    {
        return await _context.Tickets
            .Include(t => t.Carrier)
            .Include(t => t.Destination)
            .Include(t => t.Departure)
            .Include(t => t.Type)
            .Where(t => t.Carrier.Name == carrier)
            .ToListAsync();
    }
    public async Task<IList<Ticket>> SearchTickets(string departure, string destination)
    {
        return await _context.Tickets
            .Include(t => t.Carrier)
            .Include(t => t.Destination)
            .Include(t => t.Departure)
            .Include(t => t.Type)
            .Where(t => t.Departure.Name == departure && t.Destination.Name == destination)
            .ToListAsync();
    }
}