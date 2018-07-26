using System.Threading;

namespace AtlasLab.Abstract
{
    public interface ITimerService
    {
        //int a { get; set; }
        void Repeat(TimerCallback callback);
        void StopRepeating();
    }
}