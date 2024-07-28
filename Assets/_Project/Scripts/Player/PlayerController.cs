using System.Collections.Generic;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;
using Utilities;

using static Utilities.Globals;

namespace CoffeeDrop
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        #region SETTINGS
        [Header("References")]
        [SerializeField, Self] Rigidbody RB;
        [SerializeField, Self] GroundChecker GroundChecker;
        [SerializeField, Self] Animator Animator;
        [SerializeField, Anywhere] CinemachineFreeLook FreeLookVCam;
        [SerializeField, Anywhere] InputReader InputReader;

        [Header("Movement Settings")]
        [SerializeField] float MoveSpeed = 6f;
        [SerializeField] float RotationSpeed = 15f;
        [SerializeField] float SmoothTime = 0.2f;

        [Header("Jump Settings")]
        [SerializeField] float JumpForce = 5f;
        [SerializeField] float JumpDuration = 0.5f;
        [SerializeField] float GravityMultiplier = 1f;
        [SerializeField] float JumpCooldown = 0f;

        [Header("Dash Settings")]
        [SerializeField] float DashForce = 3f;
        [SerializeField] float DashDuration = 1f;
        [SerializeField] float DashDistance = 1f;
        [SerializeField] float DashCooldown = 0f;
        #endregion
        #region VARS
        // animator params
        static readonly int Speed = Animator.StringToHash("Speed");

        //State machine
        StateMachine StateMachine;
        // timers
        List<Timer> Timers = new List<Timer>();
        CountdownTimer JumpTimer;
        CountdownTimer JumpCooldownTimer;
        CountdownTimer DashTimer;
        CountdownTimer DashCooldownTimer;

        // locals
        Transform MainCamera;
        float CurrentSpeed;
        float Velocity;
        float JumpVelocity;
        float DashVelocity = 1f;
        Vector3 Movement;
        #endregion

        void OnEnable()
        {
            InputReader.Jump += OnJump;
            InputReader.Dash += OnDash;
        }
        void OnDisable()
        {
            InputReader.Jump -= OnJump;
            InputReader.Dash -= OnDash;
        }
        void Start() => InputReader.EnablePlayerActions();
        void FixedUpdate()
        {
            StateMachine.FixedUpdate();
        }
        void UpdateAnimator()
        {
            Animator.SetFloat(Speed, CurrentSpeed);
        }
        void Update()
        {
            Movement = new Vector3(InputReader.Direction.x, 0f, InputReader.Direction.y);
            StateMachine.Update();
            HandleTimers();
            UpdateAnimator();
        }
        void Awake()
        {
            //Cinemachine camera
            MainCamera = Camera.main.transform;
            FreeLookVCam.Follow = transform;
            FreeLookVCam.LookAt = transform;

            // Invoke Event when observed transform is teleported , adjusting cam's position accordingly
            FreeLookVCam.OnTargetObjectWarped(transform, transform.position - FreeLookVCam.transform.position - Vector3.forward);
            RB.freezeRotation = true;

            // Setup timers
            JumpTimer = new CountdownTimer(JumpDuration);
            JumpCooldownTimer = new CountdownTimer(JumpCooldown);

            JumpTimer.OnTimerStart += () => JumpVelocity = JumpForce;
            JumpTimer.OnTimerStop += () => JumpCooldownTimer.Start();

            DashTimer = new CountdownTimer(DashDuration);
            DashCooldownTimer = new CountdownTimer(DashCooldown);
            DashTimer.OnTimerStart += () => DashVelocity = DashForce;
            DashTimer.OnTimerStop += () =>
            {
                DashVelocity = 1f;
                DashCooldownTimer.Start();
            };

            Timers = new List<Timer>(4) { JumpTimer, JumpCooldownTimer, DashTimer, DashCooldownTimer };

            // StateMachine
            StateMachine = new();

            //Declare States
            var locomotionState = new LocomotionState(this, Animator);
            var JumpState = new JumpState(this, Animator);
            var DashState = new DashState(this, Animator);

            //Define Transiions
            At(locomotionState, JumpState, new FuncPredicate(() => JumpTimer.IsRunning));
            At(locomotionState, DashState, new FuncPredicate(() => DashTimer.IsRunning));
            Any(locomotionState, new FuncPredicate(() => !DashTimer.IsRunning && GroundChecker.IsGrounded && !JumpTimer.IsRunning));

            //Set initial State
            StateMachine.SetState(locomotionState);
        }
        void OnJump(bool performed)
        {
            if (performed && !JumpTimer.IsRunning && !JumpCooldownTimer.IsRunning && GroundChecker.IsGrounded)
            {
                JumpTimer.Start();
            }
            else if (!performed && JumpTimer.IsRunning)
            {
                JumpTimer.Stop();
            }
        }
        void OnDash(bool performed)
        {
            if (performed && !DashTimer.IsRunning && !DashCooldownTimer.IsRunning)
            {
                DashTimer.Start();
            }
            else if (!performed && DashTimer.IsRunning)
            {
                DashTimer.Stop();
            }
        }
        public void HandleMovement()
        {
            // make an vector from the input
            var adjustedDirection = Quaternion.AngleAxis(MainCamera.eulerAngles.y, Vector3.up) * Movement;
            if (adjustedDirection.magnitude > ZeroF)
            {
                // adjust rotation to match movement direction
                HandleRotation(adjustedDirection);
                //move the player
                HandleHorizontalMovement(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(ZeroF);
                //reset velocity for a snappy stop
                RB.velocity = new Vector3(ZeroF, RB.velocity.y, ZeroF);
            }
        }
        public void HandleJump()
        {
            // if player fell and just hit the ground
            if (!JumpTimer.IsRunning && GroundChecker.IsGrounded)
            {
                JumpVelocity = ZeroF;
                JumpTimer.Stop();
                return;
            }
            //  if player is falling
            if (!JumpTimer.IsRunning)
            {
                // Gravity takes over
                JumpVelocity += Physics.gravity.y * GravityMultiplier * Time.fixedDeltaTime;

            }
            //Apply Velocity
            RB.velocity = new Vector3(RB.velocity.x, JumpVelocity, RB.velocity.z);
        }
        public void HandleSprint()
        {
            Debug.Log($"Sprint!!! : {DashDistance}");
        }
        private void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            Vector3 velocity = MoveSpeed * Time.fixedDeltaTime * adjustedDirection * DashVelocity;
            RB.velocity = new Vector3(velocity.x, RB.velocity.y, velocity.z);
        }
        private void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
        void HandleTimers()
        {
            foreach (var timer in Timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }
        private void SmoothSpeed(float value)
        {
            CurrentSpeed = Mathf.SmoothDamp(CurrentSpeed, value, ref Velocity, SmoothTime);
        }
        void At(IState from, IState to, IPredicate condition) => StateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => StateMachine.AddAnyTransition(to, condition);
    };
}
