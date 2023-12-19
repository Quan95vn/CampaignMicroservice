using Campaign.Application.Query;
using Campaign.Application.Response;
using Campaign.Infrastructure.Repositories;
using MediatR;

namespace Campaign.Application.QueryHandler;

public class GetCampaignsQueryHandler : IRequestHandler<GetCampaignsQuery, List<CampaignResponse>>
{
    private readonly ICampaignRepository _campaignRepository;

    public GetCampaignsQueryHandler(ICampaignRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<List<CampaignResponse>> Handle(GetCampaignsQuery request, CancellationToken cancellationToken)
    {
        var campaigns = await _campaignRepository.GetCampaignsAsync();

        return campaigns.Select(x => new CampaignResponse
        {
            Name = x.Name,
            Description = x.Description,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            TotalAmount = x.TotalAmount,
            PrizeCount = x.PrizeCount
        }).ToList();
    }
}