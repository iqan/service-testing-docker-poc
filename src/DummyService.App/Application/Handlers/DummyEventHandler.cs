using DummyService.App.Application.Models;
using DummyService.App.Application.Storage;
using Microsoft.Extensions.Logging;

namespace DummyService.App.Application.Handlers
{
    public class DummyEventHandler : IDummyEventHandler
    {
        private readonly ILogger _logger;
        private readonly IMessageDatabase _messageDatabase;

        public DummyEventHandler(ILogger<DummyEventHandler> logger, IMessageDatabase messageDatabase)
        {
            _logger = logger;
            _messageDatabase = messageDatabase;
        }

        public void Handle(DummyEvent dummyEvent)
        {
            _logger.LogInformation("Handling event");

            _messageDatabase.InsertMessageData(new MessageDataDto
            {
                MessageText = dummyEvent.Text
            });

            _logger.LogInformation("Event handled successfully");
        }
    }
}
