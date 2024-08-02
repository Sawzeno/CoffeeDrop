using Cinemachine;
using KBCore.Refs;
using UnityEngine;
using Timers;


using static Utils.Globals;

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
        [SerializeField] float DashCooldown = 0f;

        [Header("Attack Settings")]
        [SerializeField] float AttackCooldown = 0.5f;
        [SerializeField] float AttackingRange = 1f;
        [SerializeField] int AttackDamage = 10;
        #endregion
        #region VARS
        // animator params
        static readonly int Speed = Animator.StringToHash("Speed");

        //State machine
        StateMachine StateMachine;
        // timers
        CountdownTimer JumpTimer;
        CountdownTimer JumpCooldownTimer;
        CountdownTimer DashTimer;
        CountdownTimer DashCooldownTimer;
        CountdownTimer AttackTimer;

        // locals
        float CurrentSpeed;
        Vector3 MovedDirection;
        float Velocity;
        float JumpVelocity;
        float DashVelocity = 1f;
        #endregion

        void OnEnable()
        {
            InputReader.Jump += OnJump;
            InputReader.Dash += OnDash;
            InputReader.Attack += OnAttack;
        }
        void OnDisable()
        {
            InputReader.Jump -= OnJump;
            InputReader.Dash -= OnDash;
            InputReader.Attack -= OnAttack;

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
            MovedDirection = new Vector3(InputReader.Direction.x, 0f, InputReader.Direction.y);
            StateMachine.Update();
            UpdateAnimator();
        }
        void Awake()
        {
            FreeLookVCam.Follow = transform;
            FreeLookVCam.LookAt = transform;

            // Invoke Event when observed transform is teleported , adjusting cam's position accordingly
            FreeLookVCam.OnTargetObjectWarped(transform, transform.position - FreeLookVCam.transform.position - Vector3.forward);
            RB.freezeRotation = true;

            SetupTimers();
            SetupStateMachine();
        }
        private void SetupTimers()
        {
            JumpTimer = new CountdownTimer(JumpDuration);
            JumpCooldownTimer = new CountdownTimer(JumpCooldown);
            JumpTimer.OnTimerStart += () => JumpVelocity = JumpForce;

            DashTimer = new CountdownTimer(DashDuration);
            DashCooldownTimer = new CountdownTimer(DashCooldown);
            DashTimer.OnTimerStart += () => DashVelocity = DashForce;
            DashTimer.OnTimerStop += () => { DashVelocity = 1f; DashCooldownTimer.Start(); };

            AttackTimer = new CountdownTimer(AttackCooldown);
        }
        private void SetupStateMachine()
        {
            StateMachine = new();
            //Declare States
            var locomotionState = new PlayerLocomotionState(this, Animator);
            var jumpState = new PlayerJumpState(this, Animator);
            var dashState = new PlayerDashState(this, Animator);
            var attackState = new PlayerAttackState(this, Animator);
            //Define Transiions
            At(locomotionState, jumpState, new FuncPredicate(() => JumpTimer.IsRunning));
            At(locomotionState, dashState, new FuncPredicate(() => DashTimer.IsRunning));
            At(locomotionState, attackState, new FuncPredicate(() => AttackTimer.IsRunning));
            // At(attackState, locomotionState, new FuncPredicate(() => !AttackTimer.IsRunning));

            Any(locomotionState, new FuncPredicate(() =>
            GroundChecker.IsGrounded
            && !DashTimer.IsRunning
            && !JumpTimer.IsRunning
            && !AttackTimer.IsRunning));
            //Set initial State
            StateMachine.SetState(locomotionState);
        }
        void OnDash(bool performed)// logic for handling dash and movement is in  handleMovement()
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
        void OnJump(bool performed) // handlejump()
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
        void OnAttack() //handleAttack()
        {
            if (!AttackTimer.IsRunning)
            {
                AttackTimer.Start();
            }
        }
        public void HandleMovement()
        {
            // make an vector from the input
            var adjustedDirection = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * MovedDirection;
            if (adjustedDirection.magnitude > ZeroF)
            {
                // adjust rotation to match movement direction
                HandleRotation(adjustedDirection);
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
        private void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
        private void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            Vector3 velocity = DashVelocity * MoveSpeed * Time.fixedDeltaTime * adjustedDirection;
            RB.velocity = new Vector3(velocity.x, RB.velocity.y, velocity.z);
        }
        private void SmoothSpeed(float value)
        {
            CurrentSpeed = Mathf.SmoothDamp(CurrentSpeed, value, ref Velocity, SmoothTime);
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
        public void HandleAttack()
        {
            Vector3 attackPos = transform.position + transform.forward;
            Collider[] hitEnemies = Physics.OverlapSphere(attackPos, AttackingRange);
            foreach (var enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(AttackDamage);
                }
            }
        }

        void At(IState from, IState to, IPredicate condition) => StateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => StateMachine.AddAnyTransition(to, condition);

        void OnDestroy()
        {
            JumpTimer.Dispose();
            JumpCooldownTimer.Dispose();
            DashTimer.Dispose();
            DashCooldownTimer.Dispose();
            AttackTimer.Dispose();
        }
    };
}
