using UnityEngine;

namespace CoffeeDrop
{
    public class LocomotionState : PlayerBaseState
    {
        public LocomotionState(PlayerController player, Animator animator) : base(player, animator)
        {
        }
        public override void OnEnter()
        {
            Animator.CrossFade(LocomationHash, CrossFadeDuration);
        }
        public override void FixedUpdate()
        {
            // call players move logic
            Player.HandleMovement();
        }
    }
}