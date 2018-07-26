namespace AtlasLab.Abstract
{
    public interface IMqService
    {
        void Publish(Message message);
        Message Get();
        uint MessageCount();
        void Purge();
    }
}