using UnityEngine;
using Utilities;

namespace CoffeeDrop
{
    public class ConeDetectionStrategy : IDetectionStrategy
    {
        float DetectionAngle;
        float InnerDetectionRadius;
        float OuterDetectionRadius;
        public ConeDetectionStrategy(float detectionAngle, float innerDetectionRadius, float outerDetectionRadius)
        {
            DetectionAngle = detectionAngle;
            InnerDetectionRadius = innerDetectionRadius;
            OuterDetectionRadius = outerDetectionRadius;
        }

        public bool Execute(Transform player, Transform detector, CountdownTimer detectionTimer)
        {
            if (detectionTimer.IsRunning) return false;
            var directionToPlayer = player.position - detector.position;
            var angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);

            // ------------------------------------------return if 
            if (!(angleToPlayer < DetectionAngle / 2f) // not within the detection angle
                || !(directionToPlayer.magnitude < OuterDetectionRadius) // outside the outer radius
                && !(directionToPlayer.magnitude < InnerDetectionRadius)) return false; // inside the inner radius
            
            detectionTimer.Start();
            return true;
        }
    }
}
