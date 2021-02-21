using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    public delegate void OnTick();
    public static class TickManager
    {
        public static event OnTick _Tick;

        private static Thread _TickThread = new Thread(new ThreadStart(Update));
        public static double _Ticks = 0;
        public static int _TickRate = 1;

        public static void Init()
        {
            _TickThread.Start();
        }

        public static void Update()
        {
            while (true)
            {
                Console.Title = "Project PBBG Server | Ticks -> " + _Ticks.ToString();

                    if (_Tick != null) _Tick.Invoke();
                    _Ticks++;

                Thread.Sleep(_TickRate * 1000);
            }
        }
    }
}
