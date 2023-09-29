using DummyService.App.Application.Messaging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DummyService.App
{
    public class TimedHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IMessageProcessor _messageProcessor;

        public TimedHostedService(ILogger<TimedHostedService> logger, IMessageProcessor messageProcessor)
        {
            _logger = logger;
            _messageProcessor = messageProcessor;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is starting.");
            return _messageProcessor.StartProcessingAsync();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Service is running.");
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is stopping.");
            return _messageProcessor.StopProcessingAsync();
        }
    }
}
