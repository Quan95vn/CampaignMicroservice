using Campaign.Application.Query;
using Campaign.Application.Response;
using Campaign.Infrastructure.Repositories;
using MediatR;

namespace Campaign.Application.QueryHandler;

public class GetCampaignQueryHandler : IRequestHandler<GetCampaignQuery, CampaignResponse>
{
    private readonly ICampaignRepository _campaignRepository;

    public GetCampaignQueryHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<CampaignResponse> Handle(GetCampaignQuery request, CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetCampaignByCampaignIdAsync(request.Id);

        if (campaign == null)
            throw new NullReferenceException();
        
        return new CampaignResponse
        {
            Name = campaign.Name,
            Description = campaign.Description,
            StartDate = campaign.StartDate,
            EndDate = campaign.EndDate,
            TotalAmount = campaign.TotalAmount,
            PrizeCount = campaign.PrizeCount
        };
    }
}