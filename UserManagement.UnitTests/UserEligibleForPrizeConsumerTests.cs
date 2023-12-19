using Contracts.Prize;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using UserManagement.Application.EventHandler;
using UserManagement.Infrastructure;
using Xunit;

namespace UserManagement.UnitTests;

public class UserEligibleForPrizeConsumerTests
{
    [Fact]
    public async void Consumer_ShouldReceiveLogInfoWhenUserIsEligibleForPrize()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        using var context = new UserContext(options);
        
        var mock = new Mock<ILogger<UserEligibleForPrizeConsumer>>();
        ILogger<UserEligibleForPrizeConsumer> logger = mock.Object;
        var consumer = new UserEligibleForPrizeConsumer(logger);

        var userId = Guid.NewGuid();
        
        var userEligibleForPrize = new UserEligibleForPrizeEvent {
            UserId = userId,
        };
        
        // Mock the context
        var consumerContext = Mock.Of<ConsumeContext<UserEligibleForPrizeEvent>>(_ => 
            _.Message == userEligibleForPrize);
        
        
        // Act
        await consumer.Consume(consumerContext);
      
        // Assert
        mock.Verify(x => x.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => string.Equals($"User {userId} is eligible for a prize", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}