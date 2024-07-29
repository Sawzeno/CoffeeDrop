using System;
using System.Collections;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace CoffeeDrop
{
    public class CameraManager : MonoBehaviour{
        [Header("References")]
        [SerializeField, Anywhere] InputReader Input;
        [SerializeField, Anywhere] CinemachineFreeLook FreeLookVcam;
        [Header("Settings")]
        [SerializeField, Range(0.5f, 3f)] float speedMultiplier = 1f;
        bool IsRMBPressed;
        bool IsCameraMovementLocked;
        private void OnEnable(){
            Debug.Log("Camera Manager Enabled");
            Input.Look += OnLook;
            Input.EnableMouseControlCamera += OnEnableMouseControlCamera;
            Input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }
        private void OnDisable(){
            Debug.Log("Camera Manager Disabled");
            Input.Look -= OnLook;
            Input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
            Input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        }
        private void OnLook(Vector2 CameraMovement, bool isDeviceMouse){
            // Debug.Log("OnLook");
            if(IsCameraMovementLocked) return;
            if(isDeviceMouse && !IsRMBPressed) return;
            // is the mouse is fixed use fixed delta time otherwise use delta time
            float deviceMuliplier = isDeviceMouse? Time.fixedDeltaTime : Time.deltaTime;
            FreeLookVcam.m_XAxis.m_InputAxisValue   =   CameraMovement.x * speedMultiplier * deviceMuliplier;
            FreeLookVcam.m_YAxis.m_InputAxisValue   =   CameraMovement.y * speedMultiplier * deviceMuliplier;

        }
        private void OnDisableMouseControlCamera()
        {
            IsRMBPressed = false;
            // unlock the cursor and make it visible;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // Reset the camera axis to prevent jumping when re enabling mouse control
            FreeLookVcam.m_XAxis.m_InputAxisValue = 0f;
            FreeLookVcam.m_YAxis.m_InputAxisValue = 0f;
        }
        private void OnEnableMouseControlCamera()
        {
            Debug.Log("OnEnableMouseControlCamera");
            IsRMBPressed = true;
            // lock the cursosr to the center of the screen and hide it
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible  =   false;
            StartCoroutine(DisableMouseForFrame());
        }
        private IEnumerator DisableMouseForFrame()
        {
            IsCameraMovementLocked = true;
            yield return new WaitForEndOfFrame();
            IsCameraMovementLocked = false;
        }
    };
}
 