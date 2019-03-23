using DummyService.App.Application.Models;

namespace DummyService.App.Application.Interfaces
{
    public interface IDatabaseService
    {
        void InsertMessageData(MessageDataDto messageData);
    }
}