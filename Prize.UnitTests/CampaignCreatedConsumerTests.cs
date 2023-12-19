using Contracts.Campaign;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Moq;
using Prize.Application.EventHandler;
using Prize.Infrastructure;
using Prize.Infrastructure.Repositories;
using Xunit;

namespace Prize.UnitTests;

public class CampaignCreatedConsumerTests
{
    [Fact]
    public async void Consumer_ShouldPopulatePrizeData()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PrizeContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        using var context = new PrizeContext(options);
        
        var repository = new PrizeRepository(context);
        var consumer = new CampaignCreatedConsumer(repository);

        var campaignCreatedEvent = new CampaignCreatedEvent {
            CampaignId = Guid.NewGuid(),
            Name = "Campaign 1",
            TotalAmount = 500,
            PrizeCount  = 50
        };
        
        // Mock the context
        var consumerContext = Mock.Of<ConsumeContext<CampaignCreatedEvent>>(_ => 
            _.Message == campaignCreatedEvent);
        
        // Act
        await consumer.Consume(consumerContext);
        
        // Assert
        Assert.NotEmpty(await context.Prizes.ToListAsync());
        Assert.Equal(1, context.Prizes.Count());
    }
}