using CampaignManagement.Application.Query;
using CampaignManagement.Domain;
using CampaignManagement.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CampaignManagement.Application.QueryHandler;

public class GetCampaignQueryHandler : IRequestHandler<GetCampaignQuery, Campaign>
{
    private readonly CampaignContext _context;

    public GetCampaignQueryHandler(CampaignContext context)
    {
        _context = context;
    }

    public async Task<Campaign> Handle(GetCampaignQuery request, CancellationToken cancellationToken)
    {
        var campaign = await _context.Campaigns.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        return campaign;
    }
}