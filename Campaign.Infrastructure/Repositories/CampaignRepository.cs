using Campaign.Domain;
using Microsoft.EntityFrameworkCore;

namespace Campaign.Infrastructure.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly CampaignContext _context;

    public CampaignRepository(CampaignContext context)
    {
        _context = context;
    }
    
    public async Task AddCampaignAsync(Domain.Campaign campaign)
    {
        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();
    }

    public async Task<Domain.Campaign> GetCampaignByCampaignIdAsync(Guid campaignId)
    {
        var campaign = await _context.Campaigns.FirstOrDefaultAsync(x => x.Id == campaignId);

        return campaign;
    }

    public async Task<List<Domain.Campaign>> GetCampaignsAsync()
    {
        var campaigns = await _context.Campaigns.ToListAsync();

        return campaigns;
    }
}