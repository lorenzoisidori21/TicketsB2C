using TicketsB2C.DataAccess;
using TicketsB2C.Models;
using TicketsB2C.Services;

namespace TicketsB2C.Business;

public class TicketsBusiness : ITicketsBusiness
{
    private readonly ITicketsDal _dal;
    private readonly IDiscountService _discountService;
    public TicketsBusiness(ITicketsDal dal, IDiscountService discountService)
    {
        _dal = dal;
        _discountService = discountService;
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
            return new CheckoutSummary(ticketId, null, null, null, null, quantity, 0, 0);
        }

        // TODO perform payment

        decimal percentageOff = _discountService.CalculateDiscount(ticket.TypeId, quantity);
        decimal appliedDiscount = 0;
        if (percentageOff > 0)
            appliedDiscount = ticket.Price * quantity * (percentageOff / 100);
        appliedDiscount = Math.Round(appliedDiscount, 2);
        decimal totalAmount = (ticket.Price * quantity) - appliedDiscount;
        var summary = new CheckoutSummary(ticketId, ticket.Departure, ticket.Destination, ticket.Carrier, ticket.Type, quantity, totalAmount, appliedDiscount)
        {
            Success = true
        };
        return summary;
    }
}