using Microsoft.EntityFrameworkCore;

namespace Prize.Infrastructure.Repositories;

public class PrizeRepository : IPrizeRepository
{
    private readonly PrizeContext _context;

    public PrizeRepository(PrizeContext context)
    {
        _context = context;
    }

    public async Task AddPrizeAsync(Domain.Prize prize)
    {
        _context.Prizes.Add(prize);
        await _context.SaveChangesAsync();
    }

    public async Task<Domain.Prize> GetPrizeByCampaignIdAsync(Guid campaignId)
    {
        var prize = await _context.Prizes.FirstOrDefaultAsync(x => x.CampaignId == campaignId);

        return prize;
    }
}