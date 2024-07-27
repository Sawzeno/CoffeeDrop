using UnityEngine;

namespace CoffeeDrop
{
    public class SprintState : BaseState
    {
        public SprintState(PlayerController player, Animator animator) : base(player, animator)
        {
        }
        public override void OnEnter()
        {
            Debug.Log("Sprint State On Enter");
            Animator.CrossFade(SprintHash, CrossFadeDuration);
        }
        public override void FixedUpdate()
        {
            Player.HandleSprint();
        }
    }
}