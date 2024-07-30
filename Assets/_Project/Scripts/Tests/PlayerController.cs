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
        private Vector3 movedDirection;

        Transform mainCamera;

        void Awake(){
            mainCamera  =   Camera.main.transform;
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
            Vector3 velocity    =   moveSpeed * Time.deltaTime * movedDirection;
            RB.velocity = new Vector3(velocity.x, RB.velocity.y, velocity.z);
            Debug.Log($"speed = {RB.velocity.magnitude}");
        }
    }
}