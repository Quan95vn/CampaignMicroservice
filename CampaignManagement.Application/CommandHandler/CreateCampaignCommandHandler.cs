using CampaignManagement.Application.Command;
using CampaignManagement.Application.Response;
using CampaignManagement.Domain;
using CampaignManagement.Infrastructure;
using CampaignManagement.Infrastructure.Repositories;
using IntergrationEvent.Campaign;
using MediatR;
using MassTransit;

namespace CampaignManagement.Application.CommandHandler;

public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, CampaignResponse>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IBus _bus;
    
    public CreateCampaignCommandHandler(ICampaignRepository campaignRepository, IBus bus)
    {
        _campaignRepository = campaignRepository;
        _bus = bus;
    }
    public async Task<CampaignResponse> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = new Campaign
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalAmount = request.TotalAmount,
            PrizeCount = request.PrizeCount
        };

        await _campaignRepository.AddCampaignAsync(campaign);

        await _bus.Publish(new CampaignCreatedEvent
        {
            Id = Guid.NewGuid(), 
            Name = request.Name, 
            TotalAmount = request.TotalAmount, 
            PrizeCount = request.PrizeCount
        }, cancellationToken);
        
        return new CampaignResponse
        {
            Id = campaign.Id,
            Name = campaign.Name,
            Description = campaign.Description,
            StartDate = campaign.StartDate,
            EndDate = campaign.EndDate,
            TotalAmount = campaign.TotalAmount,
            PrizeCount = campaign.PrizeCount
        };
    }
}