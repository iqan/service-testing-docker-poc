using System.Threading.Tasks;

namespace DummyService.App.Application.Messaging
{
    public interface IMessageProcessor
    {
        Task StartProcessingAsync();

        Task StopProcessingAsync();
    }
}