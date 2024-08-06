using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class EnemyWanderState : EnemyBaseState
    {
        readonly NavMeshAgent Agent;
        readonly Vector3 StartPoint;
        readonly float WanderRadius;
        public EnemyWanderState(EnemyController enemy, Animator animator, NavMeshAgent agent, float wanderRadius) : base(enemy, animator)
        {
            Agent = agent;
            StartPoint = enemy.transform.position;
            WanderRadius = wanderRadius;
        }
        public override void OnEnter()
        {
            Animator.CrossFade(WalkHash, CrossFadeDuration);
        }
        public override void Update()
        {
            if (HasReachedDestination())
            { // time to find a new place
                var randomDirection = Random.insideUnitSphere * WanderRadius;
                randomDirection += StartPoint;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, WanderRadius, 1);
                var finalPosition = hit.position;
                Agent.SetDestination(finalPosition);
            }
        }

        private bool HasReachedDestination()
        {
            return !Agent.pathPending
                && Agent.remainingDistance <= Agent.stoppingDistance
                && (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f);
        }
    }
}

