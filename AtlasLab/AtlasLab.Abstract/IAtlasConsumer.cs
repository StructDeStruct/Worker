namespace AtlasLab.Abstract
{
    public interface IAtlasConsumer
    {
        ITimerService TimerService { get; set; }
        void Read(object state);
    }
}