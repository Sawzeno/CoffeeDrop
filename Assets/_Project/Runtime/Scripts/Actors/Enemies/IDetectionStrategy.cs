using UnityEngine;
using App.Timers;

namespace Game
{
    public interface IDetectionStrategy
    {
        bool Execute(Transform player, Transform detector, CountdownTimer detectionTimer);
    }
}
