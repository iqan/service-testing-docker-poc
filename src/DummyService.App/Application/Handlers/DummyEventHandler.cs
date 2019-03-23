using DummyService.App.Application.Interfaces;
using DummyService.App.Application.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyService.App.Application.Handlers
{
    public class DummyEventHandler : IDummyEventHandler
    {
        private readonly ILogger _logger;
        private readonly IDatabaseService _databaseService;

        public DummyEventHandler(ILogger<DummyEventHandler> logger, IDatabaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        public void Handle(DummyEvent dummyEvent)
        {
            _logger.LogInformation("Handling event");

            _databaseService.InsertMessageData(new MessageDataDto
            {
                MessageText = dummyEvent.Text
            });

            _logger.LogInformation("Event handled successfully");
        }
    }
}
