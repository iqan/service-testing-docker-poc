using DummyService.App.Application.Interfaces;
using DummyService.App.Application.Models;
using DummyService.App.Infrastructure.Persistence.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyService.App.Infrastructure.Persistence
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger _logger;
        private readonly DummyDbContext _context;

        public DatabaseService(ILogger<DatabaseService> logger, DummyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void InsertMessageData(MessageDataDto messageData)
        {
            _logger.LogInformation("Inserting data in database");

            var messageDataToInsert = new MessageData
            {
                MessageText = messageData.MessageText
            };

            _context.MessageDatas.Add(messageDataToInsert);
            _context.SaveChanges();

            _logger.LogInformation("Data successfully inserted. ID: " + messageDataToInsert.MessageDataId);
        }
    }
}
