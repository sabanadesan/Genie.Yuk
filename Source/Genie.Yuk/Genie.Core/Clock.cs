using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Timers;

namespace Genie.Yuk
{
    public class Clock
    {
        private Stopwatch stopWatch;

        public Clock()
        {
            stopWatch = new Stopwatch();
        }

        public void Start()
        {
            stopWatch.Start();
        }

        public void Stop()
        {
            stopWatch.Stop();
        }

        public TimeSpan Elapsed()
        {
            TimeSpan ts = stopWatch.Elapsed;
            return ts;
        }

        public int MsElapsed()
        {
            TimeSpan ts = stopWatch.Elapsed;
            return ts.Milliseconds;
        }

        public int SecElapsed()
        {
            TimeSpan ts = stopWatch.Elapsed;
            return ts.Seconds;
        }

        public double FPS(int ms)
        {
            double s = ((double) ms) / 1000;
            double fps = 1 / s;

            return fps;
        }
    }
}
