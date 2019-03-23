using DummyService.App.Application.Handlers;
using DummyService.App.Application.Interfaces;
using DummyService.App.Application.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DummyService.App.Infrastructure.Messaging
{
    public class MessageReceiver : IMessageReceiver
    {
        private ISubscriptionClient _subscriptionClient;
        private readonly ILogger<MessageReceiver> _logger;
        private readonly IEndpointConfiguration _endpointConfiguration;
        private readonly IDummyEventHandler _dummyEventHandler;

        public MessageReceiver(
            ILogger<MessageReceiver> logger, 
            IEndpointConfiguration endpointConfiguration,
            IDummyEventHandler dummyEventHandler)
        {
            _logger = logger;
            _endpointConfiguration = endpointConfiguration;
            _dummyEventHandler = dummyEventHandler;
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            _logger.LogInformation("Creating subscription client");

            _subscriptionClient = new SubscriptionClient(
                _endpointConfiguration.ConnectionString,
                _endpointConfiguration.Topic,
                _endpointConfiguration.Subscription);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _logger.LogInformation("Registering message handler");
            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var body = Encoding.UTF8.GetString(message.Body);
            _logger.LogInformation($"Received message: Body:{body}");
            var @event = new DummyEvent { Text = body };
            _dummyEventHandler.Handle(@event);
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogInformation($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }
    }
}
