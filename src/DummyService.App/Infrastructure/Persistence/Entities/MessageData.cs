using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DummyService.App.Infrastructure.Persistence.Entities
{
    [Table("MessageData")]
    public class MessageData
    {
        public int MessageDataId { get; set; }
        public string MessageText { get; set; }
    }
}
