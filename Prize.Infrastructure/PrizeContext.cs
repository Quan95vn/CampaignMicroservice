using Microsoft.EntityFrameworkCore;
using Prize.Domain;

namespace Prize.Infrastructure;

public class PrizeContext: DbContext
{
    public PrizeContext(DbContextOptions<PrizeContext> options) : base(options) {}

    public DbSet<Domain.Prize> Prizes { get; set; }
    
    public DbSet<PrizeClaim> PrizeClaims { get; set; }

}