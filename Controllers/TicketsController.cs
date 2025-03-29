using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketsB2C.Dto;
using TicketsB2C.Models;

namespace TicketsB2C.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsB2CDbContext _context;

        public TicketsController(TicketsB2CDbContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Carrier) // Eager loading to include Carrier data
                .Include(t => t.Destination) // Eager loading to include Carrier data
                .Include(t => t.Departure) // Eager loading to include Carrier data
                .Include(t => t.Type) // Eager loading to include Carrier data
                .Select(t => new TicketDto()
                {
                    Id = t.Id,
                    Price = t.Price,
                    Departure = t.Departure.Name,
                    Destination = t.Destination.Name,
                    TransportType = t.Type.Description,
                    Carrier = t.Carrier.Name
                })
                .ToListAsync();

            return Ok(tickets);
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
            var tickets = await _context.Tickets
                .Include(t => t.Carrier)
                .Include(t => t.Destination)
                .Include(t => t.Departure)
                .Include(t => t.Type)
                .Where(t => t.Carrier.Name == carrier)
                .Select(t => new TicketDto()
                {
                    Id = t.Id,
                    Price = t.Price,
                    Departure = t.Departure.Name,
                    Destination = t.Destination.Name,
                    TransportType = t.Type.Description,
                    Carrier = t.Carrier.Name
                })
                .ToListAsync();

            if (!tickets.Any())
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Item not found",
                    Detail = "No available tickets for that carrier"
                });
            }
            return Ok(tickets);
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

            var tickets = await _context.Tickets
                .Include(t => t.Carrier) // Eager loading to include Carrier data
                .Include(t => t.Destination) // Eager loading to include Carrier data
                .Include(t => t.Departure) // Eager loading to include Carrier data
                .Include(t => t.Type) // Eager loading to include Carrier data
                .Where(t => t.Departure.Name == departure && t.Destination.Name == destination)
                .Select(t => new TicketDto()
                {
                    Id = t.Id,
                    Price = t.Price,
                    Departure = t.Departure.Name,
                    Destination = t.Destination.Name,
                    TransportType = t.Type.Description,
                    Carrier = t.Carrier.Name
                })
                .ToListAsync();

            if (!tickets.Any())
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Item not found",
                    Detail = "No available tickets for those cities"
                });
            }
            return Ok(tickets);
        }
    }
}
