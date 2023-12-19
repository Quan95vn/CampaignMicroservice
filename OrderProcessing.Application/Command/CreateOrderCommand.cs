using MediatR;

namespace OrderProcessing.Application.Command;

public class CreateOrderCommand : IRequest<OrderResponse>
{
    public Guid UserId { get; set; }
    public Guid CampaignId { get; set; }
    public decimal Amount { get; set; }
}

public class OrderResponse
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public bool Success { get; set; }
}