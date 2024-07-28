using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace CoffeeDrop
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerDetector))]
    public class EnemyController : Entity
    {
        [SerializeField, Self] NavMeshAgent Agent;
        [SerializeField, Self] PlayerDetector PlayerDetector;
        [SerializeField, Child] Animator Animator;
        [SerializeField] float WanderRadius = 5f;
        [SerializeField] float TimeBetweenAttacks = 1f;
        StateMachine StateMachine;
        CountdownTimer AttackTimer;
        void OnValidate() => this.ValidateRefs();
        void Start()
        {
            AttackTimer =   new CountdownTimer(TimeBetweenAttacks);
            StateMachine = new();
            var wanderState = new EnemyWanderState(this, Animator, Agent, WanderRadius);
            var chaseState = new EnemyChaseState(this, Animator, Agent, PlayerDetector.Player);
            var attackState = new EnemyAttackState(this,Animator,Agent,PlayerDetector.Player);

            At(wanderState, chaseState, new FuncPredicate(() => PlayerDetector.CanDetectPlayer()));
            At(chaseState, wanderState, new FuncPredicate(() => !PlayerDetector.CanDetectPlayer()));
            At(chaseState, attackState, new FuncPredicate(() => PlayerDetector.CanAttackPlayer()));
            At(attackState, chaseState, new FuncPredicate(() => !PlayerDetector.CanAttackPlayer()));
            StateMachine.SetState(wanderState);
        }
        void Update()
        {
            StateMachine.Update();
            AttackTimer.Tick(Time.deltaTime);
        }
        void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }
        public void Attack()
        {
            if (AttackTimer.IsRunning) return;
            AttackTimer.Start();
        }
        void At(IState from, IState to, IPredicate condition) => StateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => StateMachine.AddAnyTransition(to, condition);

    }
}
