using DummyService.App.Application.Models;

namespace DummyService.App.Application.Storage
{
    public interface IMessageDatabase
    {
        void InsertMessageData(MessageDataDto messageData);
    }
}