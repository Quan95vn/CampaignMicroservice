using CampaignManagement.Domain;

namespace CampaignManagement.Infrastructure.Repositories;

public interface ICampaignRepository
{
    Task AddCampaignAsync(Campaign campaign);
}