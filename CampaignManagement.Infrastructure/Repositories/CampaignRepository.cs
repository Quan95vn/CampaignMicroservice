using CampaignManagement.Domain;

namespace CampaignManagement.Infrastructure.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly CampaignContext _context;

    public CampaignRepository(CampaignContext context)
    {
        _context = context;
    }
    
    public async Task AddCampaignAsync(Campaign campaign)
    {
        _context.Campaigns.Add(campaign);
        await _context.SaveChangesAsync();
    }
}