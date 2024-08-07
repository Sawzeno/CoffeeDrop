using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.Rendering;

namespace Utils
{
    public static class PlayerLoopUtils
    {
        public static void RemoveSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToRemove){
            if(loop.subSystemList == null) return;
            var playerLoopSystemList = new List<PlayerLoopSystem>(loop.subSystemList);
            for(int i = 0; i< playerLoopSystemList.Count; ++i){
                if(playerLoopSystemList[i].type == systemToRemove.type && playerLoopSystemList[i].updateDelegate == systemToRemove.updateDelegate){
                    playerLoopSystemList.RemoveAt(i);
                    loop.subSystemList = playerLoopSystemList.ToArray();
                }
            }
            HandleSubSystemLoopForRemoval<T>(ref loop, systemToRemove);
        }

        private static void HandleSubSystemLoopForRemoval<T>(ref PlayerLoopSystem loop, PlayerLoopSystem systemToRemove)
        {
            if(loop.subSystemList == null) return;
            for( int i = 0; i < loop.subSystemList.Length; ++i){
                RemoveSystem<T>(ref loop.subSystemList[i], systemToRemove);
            }
        }

        // insert the system into the player loop
        public static bool InsertSystem<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToInsert, int index)
        {
            if (loop.type != typeof(T)) return HandleSubSystemLoopForInsertion<T>(ref loop, systemToInsert, index);

            // found match
            var playerLoopSystemList = new List<PlayerLoopSystem>();
            if (loop.subSystemList != null) playerLoopSystemList.AddRange(loop.subSystemList);
            playerLoopSystemList.Insert(index, systemToInsert);
            loop.subSystemList = playerLoopSystemList.ToArray();
            return true;
        }

        static bool HandleSubSystemLoopForInsertion<T>(ref PlayerLoopSystem loop, in PlayerLoopSystem systemToInsert, int index)
        {
            //check to see if at leaf or no more subsystems to iterate over
            if (loop.subSystemList == null) return false;
            //otherwise check all subsystems of this node
            for (int i = 0; i < loop.subSystemList.Length; ++i)
            {
                if (!InsertSystem<T>(ref loop.subSystemList[i], in systemToInsert, index)) continue;
                return true;
            }
            return false;
        }
        public static void PrintPlayerLoop(PlayerLoopSystem rootLoop)
        {
            StringBuilder sb = new();

            if (rootLoop.type == null)
            {
                sb.AppendLine("UNITY PLAYER LOOP");
            }
            else
            {
                sb.AppendLine(rootLoop.type.ToString().ToUpper());
            }
            foreach (PlayerLoopSystem subSystem in rootLoop.subSystemList)
            {
                PrintSubsystem(subSystem, sb, 0);
            }

            Debug.Log(sb.ToString());
        }
        public static void PrintPlayerLoop(PlayerLoopSystem rootLoop, string[] highlights)
        {
            StringBuilder sb = new();

            if (rootLoop.type == null)
            {
                sb.AppendLine("UNITY PLAYER LOOP");
            }
            else
            {
                sb.AppendLine(rootLoop.type.ToString().ToUpper());
            }

            foreach (PlayerLoopSystem subSystem in rootLoop.subSystemList)
            {
                PrintSubsystem(subSystem, sb, highlights, 0);
            }

            Debug.Log(sb.ToString());
        }
        static void PrintSubsystem(PlayerLoopSystem system, StringBuilder sb, int level)
        {
            sb.Append(' ', level * 10).AppendLine(system.type.ToString());
            if (system.subSystemList == null || system.subSystemList.Length == 0) return;
            foreach (PlayerLoopSystem subSystem in system.subSystemList)
            {
                PrintSubsystem(subSystem, sb, level + 1);
            }
        }
        static void PrintSubsystem(PlayerLoopSystem system, StringBuilder sb, string[] targets, int level)
        {
            string str = system.type.ToString();
            bool found = false;
            foreach (string target in targets)
            {
                found = str.Equals(target);
                if (found) break;
            }
            if (found)
            {
                sb.Append('█', 50);
                sb.Append('\n');
            }
            sb.Append(' ', level * 10).AppendLine(system.type.ToString());
            if (system.subSystemList != null && system.subSystemList.Length > 0)
            {
                foreach (var subSystem in system.subSystemList)
                {
                    PrintSubsystem(subSystem, sb, targets, level + 1);
                }
            }
            if (found)
            {
                sb.Append('█', 50);
                sb.Append('\n');
            }
        }
    }
}