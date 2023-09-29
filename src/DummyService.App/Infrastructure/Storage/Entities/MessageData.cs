using System.ComponentModel.DataAnnotations.Schema;

namespace DummyService.App.Infrastructure.Storage.Entities
{
    [Table("MessageData")]
    public class MessageData
    {
        public int MessageDataId { get; set; }
        public string MessageText { get; set; }
    }
}
