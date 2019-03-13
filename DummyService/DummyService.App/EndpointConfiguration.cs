namespace DummyService.App
{
    public class EndpointConfiguration : IEndpointConfiguration
    {
        public string ConnectionString { get; set; }

        public string Topic { get; set; }

        public string Subscription { get; set; }
    }
}
