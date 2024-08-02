using System.Collections.Generic;
using UnityEngine;
namespace Timers
{
    public static class TimerManager{
        static readonly List<Timer> timers = new ();
        static TimerManager(){
        }
        public static void RegisterTimer(Timer timer) => timers.Add(timer);
        public static void DeregisterTimer(Timer timer) => timers.Remove(timer);
        public static void UpdateTimers(){
            foreach(Timer timer in new List<Timer>(timers)){
                timer.Tick();
            }
        }
        public static void Clear() => timers.Clear();
    }
}
