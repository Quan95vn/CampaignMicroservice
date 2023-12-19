using MassTransit;
using MediatR;
using OrderProcessing.Application.Command;
using OrderProcessing.Domain;
using OrderProcessing.Infrastructure;
using OrderProcessing.Infrastructure.Repositories;

namespace OrderProcessing.Application.CommandHandler;

public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBus _bus;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IBus bus)
    {
        _bus = bus;
        _orderRepository = orderRepository;
    }

    public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CampaignId = request.CampaignId,
            UserId = request.UserId,
            OrderDate = DateTime.UtcNow,
            OrderAmount = request.Amount
        };

        await _orderRepository.AddOrderAsync(order);

        //Integrate with a payment gateway service to process transactions
        
        return new OrderResponse { OrderId = order.Id, UserId = order.UserId, Success = true};
    }
}