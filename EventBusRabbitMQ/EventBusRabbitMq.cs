using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace EventBusRabbitMQ;

public class EventBusRabbitMq : IEventBus, IDisposable
{
    const string BROKER_NAME = "eshop_event_bus";
    const string AUTOFAC_SCOPE_NAME = "eshop_event_bus";

    private readonly IRabbitMqPersistentConnection _persistentConnection;
    private readonly ILogger<EventBusRabbitMQ> _logger;
    private readonly IEventBusSubscriptionsManager _subsManager;
    private readonly ILifetimeScope _autofac;
    private readonly int _retryCount;

    private IModel _consumerChannel;
    private string _queueName;
    
    public void Publish(IntegrationEvent @event)
    {
        throw new NotImplementedException();
    }

    public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}