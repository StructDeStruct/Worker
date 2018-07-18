namespace Project.AtlasLab
{
    public interface IAtlasConsumer
    {
        ITimerService TimerService { get; set; }
        void Read(object state);
    }
}