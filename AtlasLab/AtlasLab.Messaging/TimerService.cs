using System;
using System.Threading;
using AtlasLab.Abstract;

namespace AtlasLab.Messaging
{
    public class TimerService : IDisposable, ITimerService, IService
    {
        public Timer Timer;

        public void Repeat(TimerCallback callback)
        {
            Timer = new Timer(callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        public void StopRepeating()
        {
            Timer?.Change(Timeout.Infinite, 0);
        }

        public void Dispose()
        {
            Timer?.Dispose();
        }
    }
}