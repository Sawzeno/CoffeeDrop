using System;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;

namespace Tests
{
    public class PlayerController : ValidatedMonoBehaviour {
        [Header("Refernces")]
        [SerializeField,Self] Rigidbody RB;
        [SerializeField,Anywhere] CinemachineFreeLook freeLookCamera;
        [SerializeField,Anywhere] InputReader input;

        [Header("Settings")]
        [SerializeField] float moveSpeed = 1;
        [SerializeField] float rotationSpeed = 1;
        private Vector3 movedDirection;

        void Awake(){
            freeLookCamera.Follow   =   transform;
            freeLookCamera.LookAt   =   transform;
            // for teleporation purposes
            freeLookCamera.OnTargetObjectWarped(transform,
                                                transform.position - freeLookCamera.transform.position);
        }

        void OnEnable(){
            Debug.Log("Player Controller Enabled");
        }
        void Start() => input.EnablePlayerActions();

        void Update(){
            movedDirection = new Vector3(input.Direction.x, 0, input.Direction.y);
        }
        void FixedUpdate(){
            HandleMovement();
        }
        private void HandleMovement()
        {
            var adjustedDirection = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * movedDirection;
        
            if(adjustedDirection.magnitude > 0f){

            HandleRotation(adjustedDirection);
            HandleHorizontalMovement(adjustedDirection);
            }else{
                RB.velocity = new Vector3(0f, RB.velocity.y, 0f);
            }
        }

        private void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation  =   Quaternion.LookRotation(adjustedDirection);
            transform.rotation  =   Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            Vector3 velocity = moveSpeed * Time.deltaTime * adjustedDirection;
            RB.velocity = new Vector3(velocity.x, RB.velocity.y, velocity.z);
        }
    }
}