using Campaign.Application.Command;
using Campaign.Application.CommandHandler;
using Campaign.Infrastructure;
using Campaign.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Campaign.UnitTests;

public class CreateCampaignCommandHandlerTests
{
    [Fact]
    public async void CreateCampaignCommandHandler_ShouldPersistCampaign()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CampaignContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        using var context = new CampaignContext(options);

        var repository = new CampaignRepository(context);
        var mockBus = new Mock<IBus>();
        var handler = new CreateCampaignCommandHandler(repository, mockBus.Object);

        var command = new CreateCampaignCommand
        {
            Name = "Test Campaign",
            Description = "Test Description",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(10),
            TotalAmount = 500m,
            PrizeCount = 50
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(1, context.Campaigns.Count());
    }
}