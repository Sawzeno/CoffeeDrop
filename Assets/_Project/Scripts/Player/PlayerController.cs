using System.Collections.Generic;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;
using Utilities;

namespace CoffeeDrop
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        private const float ZeroF = 0f;
        [Header("References")]
        [SerializeField, Self] Rigidbody rb;
        [SerializeField, Self] GroundChecker GroundChecker;
        [SerializeField, Self] Animator Animator;
        [SerializeField, Anywhere] CinemachineFreeLook FreeLookVCam;
        [SerializeField, Anywhere] InputReader InputReader;

        [Header("Movement Settings")]
        [SerializeField] float MoveSpeed = 6f;
        [SerializeField] float RotationSpeed = 15f;
        [SerializeField] float SmoothTime = 0.2f;

        [Header("Jump Settings")]
        [SerializeField] float JumpForce = 1f;
        [SerializeField] float JumpDuration = 0.5f;
        [SerializeField] float JumpCooldown = 0f;
        [SerializeField] float JumpMaxHeight = 2f;
        [SerializeField] float GravityMultiplier = 1f;

        Transform MainCamera;
        float CurrentSpeed;
        float Velocity;
        float JumpVelocity;
        Vector3 Movement;
        List<Timer> Timers = new List<Timer>();
        CountdownTimer JumpTimer;
        CountdownTimer JumpCoolDownTimer;
        // animator params
        static readonly int Speed = Animator.StringToHash("Speed");

        void OnEnable(){
            InputReader.Jump += OnJump;
        }
        void OnDisable(){
            InputReader.Jump -= OnJump;
        }
        void Start() => InputReader.EnablePlayerActions();
        void Update()
        {
            Movement = new Vector3(InputReader.Direction.x,0f,InputReader.Direction.y);
            HandleTimers();
            UpdateAnimator();
        }
        void FixedUpdate(){
            HandleJump();
            HandleMovement();
        }
        void Awake()
        {
            MainCamera = Camera.main.transform;
            FreeLookVCam.Follow = transform;
            FreeLookVCam.LookAt = transform;
            // Invoke Event when observed transform is teleported , adjusting cam's position accordingly
            FreeLookVCam.OnTargetObjectWarped(transform, transform.position - FreeLookVCam.transform.position - Vector3.forward);
            rb.freezeRotation = true;
            //setup timers
            JumpTimer = new CountdownTimer(JumpDuration);
            JumpCoolDownTimer = new CountdownTimer(JumpCooldown);
            Timers = new List<Timer>(2){JumpTimer, JumpCoolDownTimer};
            JumpTimer.OnTimerStop += () => JumpCoolDownTimer.Start();
        }
        void OnJump(bool performed){
            if(performed && !JumpTimer.IsRunning && !JumpCoolDownTimer.IsRunning && GroundChecker.IsGrounded){
                JumpTimer.Start();
            }else if(!performed && JumpTimer.IsRunning){
                JumpTimer.Stop();
            }
        }
        private void UpdateAnimator()
        {
            Animator.SetFloat(Speed, CurrentSpeed);
        }
        private void HandleMovement()
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
                rb.velocity = new Vector3(ZeroF, rb.velocity.y, ZeroF);
            }
        }
        private void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            Vector3 velocity = MoveSpeed * Time.fixedDeltaTime * adjustedDirection;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }
        private void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
        void HandleTimers(){
            foreach(var timer in Timers){
                timer.Tick(Time.deltaTime);
            }
        }
        void HandleJump(){
            // if not jumping and grounded , keep jumping velocity at 0
            if(!JumpTimer.IsRunning && GroundChecker.IsGrounded){
                JumpVelocity    =   ZeroF;
                JumpTimer.Stop();
                return;
            }
            //  if jumping or falling and not gorunded apply gravity
            if(JumpTimer.IsRunning){
                // Progress point for initial burst of valocity
                float launchpoint = 0.9f;
                if(JumpTimer.Progress > launchpoint){
                    // calculate the velocity to reach the jump height using physics equaions v = sqrt(2gh)
                    JumpVelocity = Mathf.Sqrt( 2 * JumpMaxHeight * Mathf.Abs(Physics.gravity.y));
                }else{
                    // Gradually apply less velocity as the jump progresses
                    JumpVelocity+=( 1 - JumpTimer.Progress) * JumpForce * Time.fixedDeltaTime;
                }
            }else{
                // Gravity takes over
                JumpVelocity += Physics.gravity.y * GravityMultiplier * Time.fixedDeltaTime;
            }
            //Apply Velocity
            rb.velocity = new Vector3(rb.velocity.x, JumpVelocity, rb.velocity.z);

        }
        private void SmoothSpeed(float value)
        {
            CurrentSpeed = Mathf.SmoothDamp(CurrentSpeed, value, ref Velocity, SmoothTime);
        }
    };
}
