namespace Project.AtlasLab
{
    public interface IMqService
    {
        ConfigService Config { get; set; }
        void Publish(Message message);
        Message Get();
        uint MessageCount();
        void Purge();
    }
}