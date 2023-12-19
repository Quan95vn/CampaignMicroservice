using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrderProcessing.Application.Command;
using OrderProcessing.Application.CommandHandler;
using OrderProcessing.Domain;
using OrderProcessing.Infrastructure;
using OrderProcessing.Infrastructure.Repositories;
using Xunit;

namespace OrderProcessing.UnitTests;

public class CreateOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_GivenValidOrder_ShouldCreateOrder()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<OrderContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockBus = new Mock<IBus>();

        using var context = new OrderContext(options);
        var handler = new CreateOrderCommandHandler(mockOrderRepository.Object, mockBus.Object);

        var command = new CreateOrderCommand
        {
            UserId = Guid.NewGuid(),
            CampaignId = Guid.NewGuid(),
            Amount = 500
        };
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OrderResponse>(result);
        Assert.Equal(command.UserId, result.UserId);
        Assert.Equal(1, context.Orders.Count());
        mockOrderRepository.Verify(repo => repo.AddOrderAsync(It.IsAny<Order>()), Times.Once);
    }
}