using DummyService.App.Application.Models;
using DummyService.App.Application.Storage;
using DummyService.App.Infrastructure.Storage.Entities;
using Microsoft.Extensions.Logging;

namespace DummyService.App.Infrastructure.Storage
{
    public class SqlMessageDatabase : IMessageDatabase
    {
        private readonly ILogger _logger;
        private readonly DummyDbContext _context;

        public SqlMessageDatabase(ILogger<SqlMessageDatabase> logger, DummyDbContext context)
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
