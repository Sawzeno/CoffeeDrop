using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace CoffeeDrop
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
            Debug.Log("Attack");
            Animator.CrossFade(AttackHash, CrossFadeDuration);
        }
        public override void Update()
        {
            Debug.Log("brrrrrr");
            Agent.SetDestination(Player.position);
            Enemy.Attack();
        }
    }
}
