using DummyService.App.Application.Messaging;

namespace DummyService.App.Infrastructure.Messaging
{
    public class EndpointConfiguration : IEndpointConfiguration
    {
        public string ConnectionString { get; set; }

        public string Topic { get; set; }

        public string Subscription { get; set; }
    }
}
