using OrderProcessing.Domain;

namespace OrderProcessing.Infrastructure.Repositories;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);
}