using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TicketsB2C.Business;
using TicketsB2C.Controllers;
using TicketsB2C.Dto;
using TicketsB2C.Models;


namespace TicketsB2cUnitTests;

public class TicketsControllerUnitTests
{
    [Fact]
    public async Task GetTickets_ShouldReturn204WithEmptyList()
    {
        // Arrange
        var mockService = new Mock<ITicketsBusiness>();
        var expectedTickets = new List<Ticket>();

        mockService
            .Setup(ticketsBusiness => ticketsBusiness.GetTickets())
            .ReturnsAsync(expectedTickets);

        var controller = new TicketsController(mockService.Object);

        // Act
        var result = await controller.GetTickets();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<TicketDto>>>(result);
        Assert.IsType<NoContentResult>(actionResult.Result);
    }
    [Fact]
    public async Task GetTickets_ShouldReturn200WithListOfTickets()
    {
        // Arrange
        var mockService = new Mock<ITicketsBusiness>();
        var expectedTickets = new List<Ticket>
        {
            new Ticket { Id = 1, Price = 50.00m, Departure = new City(1, "NYC"), Destination = new City(2, "LA"), Carrier = new Carrier(1, "American Airlines"), CarrierId = 1, DepartureId = 1, DestinationId = 2, Type = new (1, "Flight"), TypeId = 1 },
            new Ticket { Id = 2, Price = 40.00m, Departure = new City(1, "NYC"), Destination = new City(3, "MIAMI"), Carrier = new Carrier(1, "American Airlines"), CarrierId = 1, DepartureId = 1, DestinationId = 3, Type = new (1, "Flight"), TypeId = 1 },
        };

        mockService
            .Setup(ticketsBusiness => ticketsBusiness.GetTickets())
            .ReturnsAsync(expectedTickets);

        var controller = new TicketsController(mockService.Object);

        // Act
        var result = await controller.GetTickets();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<TicketDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var actualTickets = Assert.IsType<List<TicketDto>>(okResult.Value);

        Assert.Equal(2, actualTickets.Count);
        Assert.Equal(expectedTickets[0].Id, actualTickets[0].Id);
        Assert.Equal(expectedTickets[1].Price, actualTickets[1].Price);
    }
}