using DummyService.App.Application.Handlers;
using DummyService.App.Application.Models;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;
using DummyService.App.Application.Messaging;

namespace DummyService.App.Infrastructure.Messaging
{
    public class MessageProcessor : IMessageProcessor
    {
        private ServiceBusClient _serviceBusClient;
        private readonly ILogger<MessageProcessor> _logger;
        private readonly IEndpointConfiguration _endpointConfiguration;
        private readonly IDummyEventHandler _dummyEventHandler;
        private readonly ServiceBusProcessor _serviceBusProcessor;

        public MessageProcessor(
            ILogger<MessageProcessor> logger, 
            IEndpointConfiguration endpointConfiguration,
            IDummyEventHandler dummyEventHandler)
        {
            _logger = logger;
            _endpointConfiguration = endpointConfiguration;
            _dummyEventHandler = dummyEventHandler;
            _serviceBusProcessor = CreateMessageProcessor();
        }

        public Task StartProcessingAsync()
        {
            return _serviceBusProcessor.StartProcessingAsync();
        }

        public Task StopProcessingAsync()
        {
            return _serviceBusProcessor.StopProcessingAsync();
        }

        private ServiceBusProcessor CreateMessageProcessor()
        {
            _logger.LogInformation("Creating servicebus client");

            _serviceBusClient = new ServiceBusClient(_endpointConfiguration.ConnectionString);

            var options = new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false
            };

            var processor = _serviceBusClient.CreateProcessor(_endpointConfiguration.Topic, _endpointConfiguration.Subscription, options);

            _logger.LogInformation("Registering message handler");
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            return processor;
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            var body = Encoding.UTF8.GetString(args.Message.Body);
            _logger.LogInformation($"Received message: Body:{body}");
            var @event = new DummyEvent { Text = body };
            _dummyEventHandler.Handle(@event);
            await args.CompleteMessageAsync(args.Message);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogInformation($"Message handler encountered an exception {args.Exception}.");
            return Task.CompletedTask;
        }
    }
}
