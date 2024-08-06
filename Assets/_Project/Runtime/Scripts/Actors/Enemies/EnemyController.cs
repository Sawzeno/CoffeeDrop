using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;
using App.Timers;
using Game.StateMachine;

namespace Game.Actors.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerDetector))]
    public class EnemyController : ValidatedMonoBehaviour
    {
        [SerializeField, Self] NavMeshAgent Agent;
        [SerializeField, Self] PlayerDetector PlayerDetector;
        [SerializeField, Child] Animator Animator;
        [SerializeField] float WanderRadius = 5f;
        [SerializeField] float TimeBetweenAttacks = 1f;
        StateProcessor StateProcessor;
        CountdownTimer AttackTimer;
        void Start()
        {
            AttackTimer =   new CountdownTimer(TimeBetweenAttacks);
            StateProcessor = new();
            var wanderState = new EnemyWanderState(this, Animator, Agent, WanderRadius);
            var chaseState = new EnemyChaseState(this, Animator, Agent, PlayerDetector.Player);
            var attackState = new EnemyAttackState(this,Animator,Agent,PlayerDetector.Player);

            At(wanderState, chaseState, new FuncPredicate(() => PlayerDetector.CanDetectPlayer()));
            At(chaseState, wanderState, new FuncPredicate(() => !PlayerDetector.CanDetectPlayer()));
            At(chaseState, attackState, new FuncPredicate(() => PlayerDetector.CanAttackPlayer()));
            At(attackState, chaseState, new FuncPredicate(() => !PlayerDetector.CanAttackPlayer()));
            StateProcessor.SetState(wanderState);
        }
        void Update()
        {
            StateProcessor.Update();
        }
        void FixedUpdate()
        {
            StateProcessor.FixedUpdate();
        }
        public void Attack()
        {
            if (AttackTimer.IsRunning) return;
            AttackTimer.Start();
            PlayerDetector.PlayerHealth.TakeDamage(10);
        }
        void At(IState from, IState to, IPredicate condition) => StateProcessor.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => StateProcessor.AddAnyTransition(to, condition);
    }
}
