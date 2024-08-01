using System.Collections;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;

namespace Tests
{
    public class CameraManager : MonoBehaviour{
        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookCam;

        [Header("Settings")]
        [SerializeField, Range(0.5f, 3f)] float SpeedMultiplier = 1f;

        bool isRBMPressed;
        bool isCameraMovementLocked;

        void OnEnable(){
            input.Look += OnLook;
            input.EnableMouseControlCamera += OnEnableMouseControlCamera;
            input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }
        void OnDisable(){
            input.Look -= OnLook;
            input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
            input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        }
        void OnLook(Vector2 cameraMovement, bool isDeviceMouse){
            if(isCameraMovementLocked) return;
            if(isDeviceMouse && ! isRBMPressed) return;
            float deviceMuliplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;
            freeLookCam.m_XAxis.m_InputAxisValue    =   cameraMovement.x * SpeedMultiplier * deviceMuliplier;
            freeLookCam.m_YAxis.m_InputAxisValue    =   cameraMovement.y * SpeedMultiplier * deviceMuliplier;
        }
        void OnDisableMouseControlCamera(){
            isRBMPressed = false;
            Cursor.lockState    =   CursorLockMode.None;
            Cursor.visible  =   true;

            freeLookCam.m_XAxis.m_InputAxisValue    =   0f;
            freeLookCam.m_YAxis.m_InputAxisValue    =   0f;
        }
        void OnEnableMouseControlCamera(){
            isRBMPressed = true;
            Cursor.lockState    =   CursorLockMode.Locked;
            Cursor.visible  =   false;
            StartCoroutine(DisableMouseForFrame());
        }
        IEnumerator DisableMouseForFrame(){
            isCameraMovementLocked  =   true;
            yield return new WaitForEndOfFrame();
            isCameraMovementLocked  =   false;
        }
    }
}