using System.Data.Common;
using UnityEngine;
using Utilities;

namespace CoffeeDrop
{
    public interface IDetectionStrategy
    {
        bool Execute(Transform player, Transform detector, CountdownTimer detectionTimer);
    }
}
