using CampaignManagement.Application.Query;
using CampaignManagement.Domain;
using CampaignManagement.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CampaignManagement.Application.QueryHandler;

public class GetAllCampaignsQueryHandler : IRequestHandler<GetAllCampaignsQuery, List<Campaign>>
{
    private readonly CampaignContext _context;

    public GetAllCampaignsQueryHandler(CampaignContext context)
    {
        _context = context;
    }

    public async Task<List<Campaign>> Handle(GetAllCampaignsQuery request, CancellationToken cancellationToken)
    {
        var campaigns = await _context.Campaigns.ToListAsync(cancellationToken);
        return campaigns;
    }
}