using UnityEngine;
using Utils;

namespace CoffeeDrop
{
    public interface IDetectionStrategy
    {
        bool Execute(Transform player, Transform detector, CountdownTimer detectionTimer);
    }
}
