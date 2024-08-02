using UnityEngine;
using Timers;

namespace CoffeeDrop
{
    public interface IDetectionStrategy
    {
        bool Execute(Transform player, Transform detector, CountdownTimer detectionTimer);
    }
}
