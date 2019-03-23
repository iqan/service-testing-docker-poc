using DummyService.App.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DummyService.App
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IMessageReceiver _messageReceiver;
        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger, IMessageReceiver messageReceiver)
        {
            _logger = logger;
            _messageReceiver = messageReceiver;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            _messageReceiver.RegisterOnMessageHandlerAndReceiveMessages();

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Service is running.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
