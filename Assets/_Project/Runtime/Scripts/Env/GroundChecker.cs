using UnityEngine;

namespace Game
{
    public class GroundChecker : MonoBehaviour{
        [SerializeField] float GroundDistance = 0.08f;
        [SerializeField] LayerMask GroundLayers;
        public bool IsGrounded { get; private set;}

        void Update(){
            IsGrounded = Physics.SphereCast(transform.position, GroundDistance, Vector3.down, out _,GroundDistance, (int)GroundLayers);

        }
    }
}
