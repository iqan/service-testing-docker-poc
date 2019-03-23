using DummyService.App.Application.Models;

namespace DummyService.App.Application.Handlers
{
    public interface IDummyEventHandler
    {
        void Handle(DummyEvent dummyEvent);
    }
}