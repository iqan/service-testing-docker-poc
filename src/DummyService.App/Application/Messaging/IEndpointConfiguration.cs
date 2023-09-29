namespace DummyService.App.Application.Messaging
{
    public interface IEndpointConfiguration
    {
        string ConnectionString { get; set; }
        string Subscription { get; set; }
        string Topic { get; set; }
    }
}