using System;
using UnityEngine;

namespace Actor
{
    [Serializable]
    public class CameraMovementSettings
    {
        public Vector2 sensitivityWalkRun;
        public Vector2 sensitivityAerial;
        public Vector2 sensitivityStare;
        
        public Vector2 angleYClamp;
        public Vector2 angleXClamp;
    }
    public class ActorCameraMovement : MonoBehaviour
    {
        public ActorCameraMovementSettings settings;
        public GameObject camParent;

        private PlayerController _controller;

        private Matrix4x4 rotation;
        private Vector2 delta;
        private float xRotation = 0.0f;
        private float yRotation = 0.0f;

        private void Start()
        {
            _controller = GetComponentInParent<PlayerController>();
        }

        public void MoveCamera(Vector2 delta)
        {
            this.delta = delta;
        }

        private Vector2 GetSensitivity()
        {
            if (_controller.controller.m_CharacterController.isGrounded)
            {
                if (_controller.controller.isStaring)
                    return settings.cameraSettings.sensitivityStare;
                
                return settings.cameraSettings.sensitivityWalkRun;
            }

            return settings.cameraSettings.sensitivityAerial;
        }

        private void FixedUpdate()
        {
            var sensitivity = GetSensitivity();
            xRotation += -delta.y * sensitivity.y * Time.fixedTime;
            xRotation = Mathf.Clamp(xRotation, settings.cameraSettings.angleYClamp.x, settings.cameraSettings.angleYClamp.y);

            
            yRotation += delta.x * sensitivity.x * Time.fixedTime;
            
            if(settings.cameraSettings.angleXClamp != Vector2.zero)
                yRotation = Mathf.Clamp(yRotation, settings.cameraSettings.angleXClamp.x, settings.cameraSettings.angleXClamp.y);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            camParent.transform.localRotation = Quaternion.Euler(Vector3.up * yRotation);
        }
    }
}