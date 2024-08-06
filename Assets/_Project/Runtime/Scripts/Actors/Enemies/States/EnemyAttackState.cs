using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class EnemyAttackState : EnemyBaseState
    {
        readonly NavMeshAgent Agent;
        readonly Transform Player;
        public EnemyAttackState(EnemyController enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
        {
            Agent = agent;
            Player = player;
        }
        public override void OnEnter()
        {
            Animator.CrossFade(AttackHash, CrossFadeDuration);
        }
        public override void Update()
        {
            Agent.SetDestination(Player.position);
            Enemy.Attack();
        }
    }
}
