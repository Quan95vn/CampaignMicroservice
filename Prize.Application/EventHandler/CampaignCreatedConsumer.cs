using Contracts.Campaign;
using MassTransit;
using Prize.Infrastructure;
using Prize.Infrastructure.Repositories;

namespace Prize.Application.EventHandler;

public class CampaignCreatedConsumer : IConsumer<CampaignCreatedEvent>
{
    private readonly PrizeContext _context;
    private readonly IPrizeRepository _prizeRepository;

    public CampaignCreatedConsumer(PrizeContext context, IPrizeRepository prizeRepository)
    {
        _context = context;
        _prizeRepository = prizeRepository;
    }

    public async Task Consume(ConsumeContext<CampaignCreatedEvent> context)
    {
        var prize = new Domain.Prize
        {
            Id = Guid.NewGuid(),
            CampaignId = context.Message.Id,
            ClaimedQuantity = 0,
            TotalQuantity = context.Message.TotalAmount,
        };

        await _prizeRepository.AddPrizeAsync(prize);
    }
}