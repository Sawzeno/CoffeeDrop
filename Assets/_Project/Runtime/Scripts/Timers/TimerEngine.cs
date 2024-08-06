using UnityEngine;
using UnityEngine.LowLevel;
using Utils;
using UnityEngine.PlayerLoop;
using UnityEditor;

namespace App.Timers
{
    internal static class TimerBootstrapper
    {
        static PlayerLoopSystem TimerSystem;
        //define and insert this system into the loop 

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize()
        {
            // get the current state of the player loop
            PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

            if (!InsertTimerManager<Update>(ref currentPlayerLoop, 0))
            {
                Debug.LogWarning("Timers not initialized, unable to register Time Manager into Update loop");
                return;
            }
            PlayerLoop.SetPlayerLoop(currentPlayerLoop);
            string[] highlghts = { "UnityEngine.PlayerLoop.Update", "Timers.TimerManager" };
            PlayerLoopUtils.PrintPlayerLoop(currentPlayerLoop, highlghts);
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;
#endif
            static void OnPlayModeState(PlayModeStateChange state)
            {
                if(state == PlayModeStateChange.ExitingPlayMode){
                    PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
                    RemoveTimerManager<Update>(ref currentPlayerLoop);
                    PlayerLoop.SetPlayerLoop(currentPlayerLoop);
                    // unity does not gaurantee the clearing if statics
                    // so well have to make sure we clear it 
                    TimerManager.Clear();
                    Debug.Log("timer cleared");
                }
            }
        }

        static void RemoveTimerManager<T>(ref PlayerLoopSystem loop){
            PlayerLoopUtils.RemoveSystem<T>(ref loop, in TimerSystem);
        }
        static bool InsertTimerManager<T>(ref PlayerLoopSystem loop, int index)
        {
            //T so that we can actually insert it into any subsytem we want 
            //index => where in the subsystem we want to poisition this
            TimerSystem = new PlayerLoopSystem()
            {
                type = typeof(TimerManager),
                updateDelegate = TimerManager.UpdateTimers,
                subSystemList = null,
            };
            return PlayerLoopUtils.InsertSystem<T>(ref loop, in TimerSystem, index);
        }
    }
}
