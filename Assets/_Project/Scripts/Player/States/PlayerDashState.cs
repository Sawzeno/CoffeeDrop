using UnityEngine;

namespace CoffeeDrop
{
    public class PlayerDashState : PlayerBaseState
    {
        public PlayerDashState(PlayerController player, Animator animator) : base(player, animator)
        {
        }
        public override void OnEnter()
        {
            Debug.Log("Dash State");
            Animator.CrossFade(DashHash, CrossFadeDuration);
        }
        public override void FixedUpdate()
        {
            Player.HandleMovement();
        }
    }
}