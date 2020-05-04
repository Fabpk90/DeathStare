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

        private Matrix4x4 _rotation;
        private Vector2 _delta;
        private float _xRotation = 0.0f;
        private float _yRotation = 0.0f;
        public Vector2 _sensitivity;

        private void Start()
        {
            _controller = GetComponentInParent<PlayerController>();
        }

        public void MoveCamera(Vector2 delta)
        {
            _delta = delta;
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

        private void Update()
        {
            _sensitivity = GetSensitivity();
            _xRotation += -_delta.y * _sensitivity.y;
            _xRotation = Mathf.Clamp(_xRotation, settings.cameraSettings.angleYClamp.x, settings.cameraSettings.angleYClamp.y);

            _yRotation += _delta.x * _sensitivity.x;
            
            if(settings.cameraSettings.angleXClamp != Vector2.zero)
                _yRotation = Mathf.Clamp(_yRotation, settings.cameraSettings.angleXClamp.x, settings.cameraSettings.angleXClamp.y);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            camParent.transform.localRotation = Quaternion.Euler(Vector3.up * _yRotation);
        }
    }
}