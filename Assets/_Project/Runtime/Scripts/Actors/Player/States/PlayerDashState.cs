using UnityEngine;

namespace Game.Player.States
{
    public class PlayerDashState : PlayerBaseState
    {
        public PlayerDashState(PlayerController player, Animator animator) : base(player, animator)
        {
        }
        public override void OnEnter()
        {
            Animator.CrossFade(DashHash, CrossFadeDuration);
        }
        public override void FixedUpdate()
        {
            Player.HandleMovement();
        }
    }
}