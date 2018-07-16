using System;
using System.Threading;

namespace Project.AtlasLab
{
    public class TimerService : IDisposable
    {
        public Timer Timer;

        public void Dispose()
        {
            Timer?.Dispose();
        }
    }
}