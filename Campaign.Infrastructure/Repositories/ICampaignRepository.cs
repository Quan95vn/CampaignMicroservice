
namespace Campaign.Infrastructure.Repositories;

public interface ICampaignRepository
{
    Task AddCampaignAsync(Domain.Campaign campaign);
    Task<Domain.Campaign> GetCampaignByCampaignIdAsync(Guid campaignId);
    Task<List<Domain.Campaign>> GetCampaignsAsync();
}