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
}