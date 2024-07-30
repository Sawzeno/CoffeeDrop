using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace Tests
{

    [CreateAssetMenu(fileName = "Tests_InputReader", menuName = "Tests/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction EnableMouseControlCamera = delegate { };
        public event UnityAction DisableMouseControlCamera = delegate { };
        public PlayerInputActions InputActions;
        public Vector3 Direction => InputActions.Player.Move.ReadValue<Vector2>();
        void OnEnable()
        {
            if(InputActions == null){
                InputActions   =   new();
                InputActions.Player.SetCallbacks(this);
            }
        }
        public void EnablePlayerActions(){
            InputActions.Enable();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            Move.Invoke(context.ReadValue<Vector2>());
        }
        public void OnLook(InputAction.CallbackContext context)
        {
            //noop
        }
        public void OnMouseControlCamera(InputAction.CallbackContext context)
        {
            //noop
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            //noop
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            //noop
        }
        public void OnRun(InputAction.CallbackContext context)
        {
            //noop
        }
        private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
    }
}