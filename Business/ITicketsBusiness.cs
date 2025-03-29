using TicketsB2C.Models;

namespace TicketsB2C.Business;

public interface ITicketsBusiness
{
    Task<IList<Ticket>> GetTickets();
    Task<IList<Ticket>> GetTicketsByCarrier(string carrier);
    Task<IList<Ticket>> SearchTickets(string departure, string destination);
    Task<CheckoutSummary> BuyTickets(int ticketId, int quantity);
}