using UnityEngine;
using Utils;

namespace CoffeeDrop
{
    public class PlayerDetector : MonoBehaviour{
        [SerializeField] public Transform Player;
        [SerializeField] float DetectionAngle  =   60f; // cone in front of enemy
        [SerializeField] float InnerDetectionRadius =   5f; // inner radius from enemy
        [SerializeField] float OuterDetectionRadius =   10f; // large circle around enemy
        [SerializeField] float DetectionCooldown    =   1f; // Time between detections
        [SerializeField] float AttackRange = 0.2f;
        CountdownTimer DetectionTimer;
        IDetectionStrategy DetectionStrategy;

        void Start(){
            DetectionTimer = new CountdownTimer(DetectionCooldown);
            DetectionStrategy   =   new ConeDetectionStrategy(DetectionAngle, InnerDetectionRadius, OuterDetectionRadius);
        }
        void Update() => DetectionTimer.Tick(Time.deltaTime);

        public bool CanDetectPlayer(){
            return DetectionTimer.IsRunning || DetectionStrategy.Execute(Player, transform, DetectionTimer);
        }
        public bool CanAttackPlayer(){
            var directionToPlayer   =   Player.position - transform.position;
            return directionToPlayer.magnitude <= AttackRange;
        }
        public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) => DetectionStrategy = detectionStrategy;

        void OnDrawGizmos(){
            Gizmos.color    =  Color.red;
            Gizmos.DrawWireSphere(transform.position, OuterDetectionRadius);
            Gizmos.DrawWireSphere(transform.position, InnerDetectionRadius);

            Vector3 fcd =   Quaternion.Euler(0, DetectionAngle/2, 0) * transform.forward * OuterDetectionRadius;
            Vector3 bcd =   Quaternion.Euler(0, -DetectionAngle/2, 0) * transform.forward * OuterDetectionRadius;

            Gizmos.DrawLine(transform.position, transform.position  + fcd);
            Gizmos.DrawLine(transform.position, transform.position  + bcd);
        }
    }
}
