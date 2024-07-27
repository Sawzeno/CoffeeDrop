using UnityEngine;

namespace CoffeeDrop
{
    public class JumpState : BaseState
    {
        public JumpState(PlayerController player, Animator animator) : base(player, animator)
        {
        }
        public override void OnEnter()
        {

            Animator.CrossFade(JumpHash, CrossFadeDuration);
        }
        public override void FixedUpdate()
        {
            // call players jump logic and move logic
            Player.HandleJump();
            Player.HandleMovement();
        }
    }
}