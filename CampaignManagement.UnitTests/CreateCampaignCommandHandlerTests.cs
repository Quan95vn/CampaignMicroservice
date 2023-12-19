using CampaignManagement.Application.Command;
using CampaignManagement.Application.CommandHandler;
using CampaignManagement.Infrastructure;
using CampaignManagement.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CampaignManagement.UnitTests;

public class CreateCampaignCommandHandlerTests
{
    [Fact]
    public async void Handle_GivenValidCampaign_ShouldPersistCampaign()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CampaignContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var mockCampaignRepository = new Mock<ICampaignRepository>();
        var mockBus = new Mock<IBus>();

        using var context = new CampaignContext(options);
        var handler = new CreateCampaignCommandHandler(mockCampaignRepository.Object, mockBus.Object);

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