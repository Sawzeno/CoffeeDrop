using UnityEngine;

namespace CoffeeDrop
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerController Player;
        protected readonly Animator Animator;

        protected static readonly int LocomationHash = Animator.StringToHash("Locomotion");
        protected static readonly int JumpHash = Animator.StringToHash("Jump");
        protected static readonly int SprintHash = Animator.StringToHash("Sprint");
        protected static readonly int DashHash  =   Animator.StringToHash("Dash");
        protected const float CrossFadeDuration = 0.1f;
        protected BaseState(PlayerController player, Animator animator)
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