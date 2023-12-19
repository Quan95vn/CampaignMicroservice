using CampaignManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace CampaignManagement.Infrastructure;

public class CampaignContext : DbContext
{
    public CampaignContext(DbContextOptions<CampaignContext> options) : base(options) {}

    public DbSet<Campaign> Campaigns { get; set; }
}