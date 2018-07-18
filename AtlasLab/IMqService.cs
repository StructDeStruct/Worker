namespace Project.AtlasLab
{
    public interface IMqService
    {
        void Publish(IMessage message);
        IMessage Get();
        uint MessageCount();
        void Purge();
    }
}