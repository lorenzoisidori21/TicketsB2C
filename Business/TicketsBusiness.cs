using TicketsB2C.DataAccess;
using TicketsB2C.Models;

namespace TicketsB2C.Business;

public class TicketsBusiness : ITicketsBusiness
{
    private readonly ITicketsDal _dal;
    public TicketsBusiness(ITicketsDal dal)
    {
        _dal = dal;
    }

    public async Task<IList<Ticket>> GetTickets()
    {
        return await _dal.GetTickets();
    }
    public async Task<IList<Ticket>> GetTicketsByCarrier(string carrier)
    {
        return await _dal.GetTicketsByCarrier(carrier);
    }
    public async Task<IList<Ticket>> SearchTickets(string departure, string destination)
    {
        return await _dal.SearchTickets(departure, destination);
    }

    public async Task<CheckoutSummary> BuyTickets(int ticketId, int quantity)
    {
        var ticket = await _dal.GetTicket(ticketId);
        if (ticket == null)
        {
            return new CheckoutSummary(ticketId, null, null, null, null, quantity, 0);
        }

        // TODO perform payment

        var summary = new CheckoutSummary(ticketId, ticket.Departure, ticket.Destination, ticket.Carrier, ticket.Type, quantity, ticket.Price * quantity)
        {
            Success = true
        };
        return summary;

    }
}