namespace Prize.Infrastructure.Repositories;

public interface IPrizeRepository
{
    Task AddPrizeAsync(Domain.Prize prize);
    Task<Domain.Prize> GetPrizeByCampaignIdAsync(Guid campaignId);
}