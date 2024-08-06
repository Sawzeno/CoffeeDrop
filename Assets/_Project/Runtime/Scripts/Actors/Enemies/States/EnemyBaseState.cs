using UnityEngine;
using Game.StateMachine;
namespace Game.Actors.Enemies;
{
    public abstract class EnemyBaseState : IState
    {
        #region VARS
        protected readonly EnemyController Enemy;
        protected readonly Animator Animator;
        protected const float CrossFadeDuration = 0.5f;
        #endregion
        #region STATES
        protected static readonly int IdleHash = Animator.StringToHash("IdleNormal");
        protected static readonly int WalkHash = Animator.StringToHash("WalkFWD");
        protected static readonly int RunHash = Animator.StringToHash("RunFWD");
        protected static readonly int AttackHash = Animator.StringToHash("Attack01");
        protected static readonly int DeathHash = Animator.StringToHash("Die");
        #endregion
        protected EnemyBaseState(EnemyController enemy, Animator animator)
        {
            Enemy = enemy;
            Animator = animator;
        }

        public virtual void FixedUpdate()
        {
            //noop
        }

        public virtual void OnEnter()
        {
            //noop
        }

        public virtual void OnExit()
        {
            //noop
        }

        public virtual void Update()
        {
            //noop
        }
    }
}
