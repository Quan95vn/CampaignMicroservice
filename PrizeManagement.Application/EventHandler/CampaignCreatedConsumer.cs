using IntergrationEvent.Campaign;
using MassTransit;
using PrizeManagement.Domain;
using PrizeManagement.Infrastructure;

namespace PrizeManagement.Application.EventHandler;

public class CampaignCreatedConsumer : IConsumer<CampaignCreatedEvent>
{
    private readonly PrizeContext _context;
    private readonly IBus _bus;

    public CampaignCreatedConsumer(PrizeContext context, IBus bus)
    {
        _context = context;
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<CampaignCreatedEvent> context)
    {
        var prize = new Prize
        {
            Id = Guid.NewGuid(),
            CampaignId = context.Message.Id,
            ClaimedQuantity = 0,
            TotalQuantity = context.Message.TotalAmount,
        };

        _context.Prizes.Add(prize);
        await _context.SaveChangesAsync();
    }
}