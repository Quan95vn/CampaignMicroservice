using Contracts.Prize;
using MassTransit;
using Microsoft.Extensions.Logging;
using UserManagement.Application.Response;
using UserManagement.Domain;

namespace UserManagement.Application.EventHandler;

public class UserEligibleForPrizeConsumer : IConsumer<UserEligibleForPrizeEvent>
{
    private readonly ILogger<UserEligibleForPrizeConsumer> _logger;

    public UserEligibleForPrizeConsumer(ILogger<UserEligibleForPrizeConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserEligibleForPrizeEvent> context)
    {
        _logger.LogInformation($"User { context.Message.UserId } is eligible for a prize");
    }
}