using UnityEngine;

namespace CoffeeDrop
{
    public abstract class PlayerBaseState : IState
    {
        #region VARS
        protected readonly PlayerController Player;
        protected readonly Animator Animator;
        protected const float CrossFadeDuration = 0.1f;
        #endregion
        #region STATES
        protected static readonly int LocomationHash = Animator.StringToHash("Locomotion");
        protected static readonly int JumpHash = Animator.StringToHash("Jump");
        protected static readonly int DashHash = Animator.StringToHash("Dash");
        #endregion
        protected PlayerBaseState(PlayerController player, Animator animator)
        {
            Player = player;
            Animator = animator;
        }


        public virtual void OnEnter()
        {
            //noop
        }
        public virtual void Update()
        {
            //noop
        }
        public virtual void FixedUpdate()
        {
            //noop
        }
        public virtual void OnExit()
        {
            //noop
        }
    }
}