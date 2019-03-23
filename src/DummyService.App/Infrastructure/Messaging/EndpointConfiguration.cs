using DummyService.App.Application.Interfaces;

namespace DummyService.App.Infrastructure.Messaging
{
    public class EndpointConfiguration : IEndpointConfiguration
    {
        public string ConnectionString { get; set; }

        public string Topic { get; set; }

        public string Subscription { get; set; }
    }
}
