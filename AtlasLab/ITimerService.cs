using System.Threading;

namespace Project.AtlasLab
{
    public interface ITimerService
    {
        //int a { get; set; }
        void Repeat(TimerCallback callback);
        void StopRepeating();
    }
}