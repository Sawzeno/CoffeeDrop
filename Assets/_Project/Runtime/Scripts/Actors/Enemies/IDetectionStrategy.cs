using UnityEngine;
using App.Timers;

namespace Game.Actors.Enemies
{
    public interface IDetectionStrategy
    {
        bool Execute(Transform player, Transform detector, CountdownTimer detectionTimer);
    }
}
