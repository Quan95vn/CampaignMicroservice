using Campaign.Application.Query;
using Campaign.Application.QueryHandler;
using Campaign.Infrastructure;
using Campaign.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Campaign.UnitTests;

public class GetCampaignsQueryHandlerTests
{
    [Fact]
    public async void GetCampaignsQueryHandler_ShouldGetCampaigns()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CampaignContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        using var context = new CampaignContext(options);
        
        context.Campaigns.Add(new Domain.Campaign
        {
            Id = Guid.NewGuid(),
            Name = "Test Campaign 1",
            Description = "Test Description 1",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(10),
            TotalAmount = 500m,
            PrizeCount = 50
        });
        
        context.Campaigns.Add(new Domain.Campaign
        {
            Id = Guid.NewGuid(),
            Name = "Test Campaign 2",
            Description = "Test Description 2",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(10),
            TotalAmount = 500m,
            PrizeCount = 50
        });
        
        await context.SaveChangesAsync();
        
        var repository = new CampaignRepository(context);
        var handler = new GetCampaignsQueryHandler(repository);
        var command = new GetCampaignsQuery();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count);
    }
}