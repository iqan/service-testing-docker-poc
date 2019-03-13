using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DummyService.App
{
    public class MessageReceiver : IMessageReceiver
    {
        private ISubscriptionClient _subscriptionClient;
        private readonly ILogger<MessageReceiver> _logger;
        private readonly IEndpointConfiguration _endpointConfiguration;

        public MessageReceiver(ILogger<MessageReceiver> logger, IEndpointConfiguration endpointConfiguration)
        {
            _logger = logger;
            _endpointConfiguration = endpointConfiguration;
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
            _logger.LogInformation($"Received message: Body:{Encoding.UTF8.GetString(message.Body)}");
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogInformation($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }
    }
}
