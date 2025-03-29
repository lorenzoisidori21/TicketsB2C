using Microsoft.AspNetCore.Mvc;
using TicketsB2C.Business;
using TicketsB2C.Dto;
using TicketsB2C.Models;

namespace TicketsB2C.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsB2CDbContext _context;
        private readonly ITicketsBusiness _business;

        public TicketsController(TicketsB2CDbContext context, ITicketsBusiness business)
        {
            _context = context;
            _business = business;
        }

        // GET: api/Tickets
        [HttpGet]
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
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsByCarrier(string carrier = "")
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
        public async Task<ActionResult<IEnumerable<TicketDto>>> SearchTickets(string departure = "", string destination = "")
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
    }
}
