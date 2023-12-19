using Contracts.Prize;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderProcessing.Application.Command;
using OrderProcessing.Domain;
using OrderProcessing.Infrastructure;
using OrderProcessing.Infrastructure.Repositories;

namespace OrderProcessing.Application.CommandHandler;

public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, OrderResponse>
{
    private readonly ILogger<CreateOrderCommandHandler> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint, ILogger<CreateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    /// <summary>
    /// Create Order 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
        _logger.LogInformation("Processing payment...");
        
        //If TotalAmount reach 500 bath threshold. Raise an event to UserService that user is eligible to claim the prize
        if (order.OrderAmount > 500)
        {
            await _publishEndpoint.Publish<UserEligibleForPrizeEvent>(new
            {
                Id = Guid.NewGuid(), 
                CampaignId = request.CampaignId, 
            }, cancellationToken);
        }
        
        return new OrderResponse { OrderId = order.Id, UserId = order.UserId, Success = true};
    }
}