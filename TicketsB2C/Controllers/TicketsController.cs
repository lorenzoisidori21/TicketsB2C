using Microsoft.AspNetCore.Mvc;
using TicketsB2C.Business;
using TicketsB2C.Dto;

namespace TicketsB2C.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsBusiness _business;

        public TicketsController(ITicketsBusiness business)
        {
            _business = business;
        }

        // GET: api/Tickets
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
        {
            var tickets = await _business.GetTickets();
            var dto = tickets.Select(t => new TicketDto(
                t.Id,
                t.Price,
                t.Departure.Name,
                t.Destination.Name,
                t.Type.Description,
                t.Carrier.Name
            ))
                .ToList();

            if(!dto.Any())
            {
                return NoContent();
            }
            return Ok(dto);
        }

        /// <summary>
        /// GET: api/Tickets/TicketsByCarrier
        /// </summary>
        /// <param name="carrier">Carrier name</param>
        /// <returns>All available tickets sold from <param name="carrier"></param></returns> 
        /// <response code="400">Please, insert a carrier</response>
        /// <response code="404">"No available tickets for that carrier"</response>
        [HttpGet("TicketsByCarrier")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsByCarrier(string carrier)
        {
            if (string.IsNullOrEmpty(carrier))
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid request",
                    Detail = "Please, insert a carrier."
                });
            var tickets = await _business.GetTicketsByCarrier(carrier);

            if (!tickets.Any())
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Item not found",
                    Detail = "No available tickets for that carrier"
                });
            }

            var dto = tickets.Select(t => new TicketDto(
                    t.Id,
                    t.Price,
                    t.Departure.Name,
                    t.Destination.Name,
                    t.Type.Description,
                    t.Carrier.Name
                ))
            .ToList();

            return Ok(dto);
        }
        /// <summary>
        /// GET: api/Tickets/SearchTickets
        /// </summary>
        /// <param name="departure">Departure city</param>
        /// <param name="destination">Destination city</param>
        /// <returns>All available ticket for <param name="departure"></param> and <param name="destination"></param> provided</returns>
        /// <response code="400">Please, insert a departure/destination</response>
        /// <response code="404">"No available tickets for those cities"</response>
        [HttpGet("SearchTickets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<TicketDto>>> SearchTickets(string departure, string destination)
        {
            if (string.IsNullOrEmpty(departure))
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Invalid request",
                    Detail = "Please, insert a departure"
                });
            if (string.IsNullOrEmpty(destination))
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Invalid request",
                    Detail = "Please, insert a destination"
                });

            var tickets = await _business.SearchTickets(departure, destination);

            if (!tickets.Any())
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Item not found",
                    Detail = "No available tickets for those cities"
                });
            }

            var dto = tickets.Select(t => new TicketDto(
                    t.Id,
                    t.Price,
                    t.Departure.Name,
                    t.Destination.Name,
                    t.Type.Description,
                    t.Carrier.Name
                ))
            .ToList();

            return Ok(dto);
        }
        /// <summary>
        /// POST: api/Tickets/BuyTickets
        /// </summary>
        /// <param name="ticket">TicketId</param>
        /// <param name="quantity">Ticket quantity</param>
        /// <returns>Checkout summary</returns>
        /// <response code="400">Please, insert a ticket id/ Please insert a quantity > 0</response>
        /// <response code="404">Ticket not found</response>
        [HttpPost("BuyTickets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CheckoutSummaryDto>>> BuyTickets(int ticket, int quantity)
        {
            if (quantity <= 0)
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Invalid request",
                    Detail = "Please insert a quantity > 0"
                });

            var summary = await _business.BuyTickets(ticket, quantity);

            if (!summary.Success)
            {
                // actually the only reason why BuyTickets fails, is because the ticket id doesn't exist
                // if there will be other reasons (payment refused, ..) the response should be more detailed
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Ticket not found",
                    Detail = "Requested ticket is not available."
                });
            }

            var dto = new CheckoutSummaryDto(
                ticket,
                summary.Departure.Name,
                summary.Destination.Name,
                summary.Carrier.Name,
                summary.Type.Description,
                quantity,
                summary.TotalAmount,
                summary.DiscountApplied,
                summary.Success
            );

            return Ok(dto);
        }
    }
}
