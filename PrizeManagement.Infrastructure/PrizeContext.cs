using Microsoft.EntityFrameworkCore;
using PrizeManagement.Domain;

namespace PrizeManagement.Infrastructure;

public class PrizeContext: DbContext
{
    public PrizeContext(DbContextOptions<PrizeContext> options) : base(options) {}

    public DbSet<Prize> Prizes { get; set; }
    
    public DbSet<PrizeClaim> PrizeClaims { get; set; }

}