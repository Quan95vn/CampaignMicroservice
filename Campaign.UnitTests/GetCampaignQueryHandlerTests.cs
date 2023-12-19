using Campaign.Application.Query;
using Campaign.Application.QueryHandler;
using Campaign.Infrastructure;
using Campaign.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Campaign.UnitTests;

public class GetCampaignQueryHandlerTests
{
    [Fact]
    public async void GetCampaignQueryHandler_ShouldGetCampaign()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CampaignContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        using var context = new CampaignContext(options);
        
        var id = Guid.NewGuid();
        var name = "Test Campaign 1";
        var description = "Test Description 1";
        context.Campaigns.Add(new Domain.Campaign
        {
            Id = id,
            Name = name,
            Description = description,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(10),
            TotalAmount = 500m,
            PrizeCount = 50
        });
        await context.SaveChangesAsync();
        
        var repository = new CampaignRepository(context);
        var handler = new GetCampaignQueryHandler(repository);
        var command = new GetCampaignQuery
        {
            Id = id
        };
            
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
        Assert.Equal(description, result.Description);
    }

    [Fact]
    public async void GetCampaignQueryHandler_ShouldThrowNullReferenceException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CampaignContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        using var context = new CampaignContext(options);
        
        var repository = new CampaignRepository(context);
        var handler = new GetCampaignQueryHandler(repository);
        var command = new GetCampaignQuery
        {
            Id = Guid.NewGuid()
        };
            
        // Act
        Func<Task> result = () => handler.Handle(command, CancellationToken.None);
        
        // Assert
        await Assert.ThrowsAsync<NullReferenceException>(result);
    }
}