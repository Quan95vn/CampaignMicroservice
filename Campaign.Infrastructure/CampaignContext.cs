using Microsoft.EntityFrameworkCore;

namespace Campaign.Infrastructure;

public class CampaignContext : DbContext
{
    public CampaignContext(DbContextOptions<CampaignContext> options) : base(options) {}

    public DbSet<Domain.Campaign> Campaigns { get; set; }
}