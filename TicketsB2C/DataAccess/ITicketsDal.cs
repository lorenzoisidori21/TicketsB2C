using TicketsB2C.Models;

namespace TicketsB2C.DataAccess;

public interface ITicketsDal
{
    Task<Ticket?> GetTicket(int id);
    Task<IList<Ticket>> GetTickets();
    Task<IList<Ticket>> GetTicketsByCarrier(string carrier);
    Task<IList<Ticket>> SearchTickets(string departure, string destination);
}