using Microsoft.EntityFrameworkCore;
using OrderProcessing.Domain;

namespace OrderProcessing.Infrastructure;

public class OrderContext: DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options) {}

    public DbSet<Order> Orders { get; set; }
}