namespace DummyService.App
{
    public interface IEndpointConfiguration
    {
        string ConnectionString { get; set; }
        string Subscription { get; set; }
        string Topic { get; set; }
    }
}