using Campaign.Application.Command;
using Campaign.Application.Response;
using Campaign.Infrastructure.Repositories;
using Contracts.Campaign;
using MediatR;
using MassTransit;

namespace Campaign.Application.CommandHandler;

public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, CampaignResponse>
{
    private readonly ICampaignRepository _campaignRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    
    public CreateCampaignCommandHandler(ICampaignRepository campaignRepository, IPublishEndpoint publishEndpoint)
    {
        _campaignRepository = campaignRepository;
        _publishEndpoint = publishEndpoint;
    }
    
    /// <summary>
    /// Create campaign
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CampaignResponse> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
    {
        var campaign = new Domain.Campaign
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
        
        // Once created, publish an event to Prize service to populate Prize data for campaign
        await _publishEndpoint.Publish<CampaignCreatedEvent>(new
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