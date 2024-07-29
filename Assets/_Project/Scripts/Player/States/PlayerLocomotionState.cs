using UnityEngine;

namespace CoffeeDrop
{
    public class PlayerLocomotionState : PlayerBaseState
    {
        public PlayerLocomotionState(PlayerController player, Animator animator) : base(player, animator)
        {
        }
        public override void OnEnter()
        {
            Animator.CrossFade(LocomationHash, CrossFadeDuration);
        }
        public override void FixedUpdate()
        {
            Player.HandleMovement();
        }
    }
}