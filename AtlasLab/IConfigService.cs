namespace Project.AtlasLab
{
    public interface IConfigService
    {
        string QueueName { get; }
        string Mode { get; }
        string UserName { get; }
        string Password { get; }
        string VirtualHost { get; }
        string HostName { get; }
        int Port { get; }
    }
}