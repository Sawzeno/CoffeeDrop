using UnityEngine;

namespace Game.Actors.Player
{
    public class PlayerAttackState : PlayerBaseState
    {
        public PlayerAttackState(PlayerController player, Animator animator) : base(player, animator)
        {
        }
        public override void OnEnter()
        {
            Animator.CrossFade(AttackHash, CrossFadeDuration);
            Player.HandleAttack();
        }
        public override void FixedUpdate()
        {
            Player.HandleMovement();
        }
    }
}