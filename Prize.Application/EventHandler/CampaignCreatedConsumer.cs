using Contracts.Campaign;
using MassTransit;
using Prize.Infrastructure;
using Prize.Infrastructure.Repositories;

namespace Prize.Application.EventHandler;

public class CampaignCreatedConsumer : IConsumer<CampaignCreatedEvent>
{
    private readonly IPrizeRepository _prizeRepository;

    public CampaignCreatedConsumer(IPrizeRepository prizeRepository)
    {
        _prizeRepository = prizeRepository;
    }

    /// <summary>
    /// Litsent to CampaignCreatedEvent to populate Prize data once Campaign is created
    /// </summary>
    /// <param name="consumeContextcontext"></param>
    public async Task Consume(ConsumeContext<CampaignCreatedEvent> consumeContextcontext)
    {
        var prize = new Domain.Prize
        {
            Id = Guid.NewGuid(),
            Name = consumeContextcontext.Message.Name,
            CampaignId = consumeContextcontext.Message.CampaignId,
            ClaimedQuantity = 0,
            TotalQuantity = consumeContextcontext.Message.TotalAmount,
        };

        await _prizeRepository.AddPrizeAsync(prize);
    }
}