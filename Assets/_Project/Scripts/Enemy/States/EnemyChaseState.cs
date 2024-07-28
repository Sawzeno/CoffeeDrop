using UnityEngine;
using UnityEngine.AI;

namespace CoffeeDrop
{
    public class EnemyChaseState : EnemyBaseState
    {
          readonly NavMeshAgent Agent;
          readonly Transform Player;
        public EnemyChaseState(EnemyController enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
        {
            Agent = agent;
            Player = player;
        }
        public override void OnEnter()
        {   
            Animator.CrossFade(RunHash, CrossFadeDuration);
        }

        public override void Update()
        {
            Agent.SetDestination(Player.position);
        }
    }
}
