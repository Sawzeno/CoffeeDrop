using UnityEngine;

namespace Game.Actors.Player
{
    public class PlayerJumpState : PlayerBaseState
    {
        public PlayerJumpState(PlayerController player, Animator animator) : base(player, animator)
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