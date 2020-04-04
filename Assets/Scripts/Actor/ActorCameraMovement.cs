using System;
using UnityEngine;

namespace Actor
{
    [Serializable]
    public class CameraMovementSettings
    {
        public Vector2 sensitivity;
        public Vector2 angleYClamp;
        public Vector2 angleXClamp;
    }
    public class ActorCameraMovement : MonoBehaviour
    {
        public ActorCameraMovementSettings settings;
        public GameObject camParent;

        private Matrix4x4 rotation;
        private Vector2 delta;
        private float xRotation = 0.0f;
        private float yRotation = 0.0f;

        public void MoveCamera(Vector2 delta)
        {
            this.delta = delta;
        }

        private void FixedUpdate()
        {
            xRotation += -delta.y * settings.cameraSettings.sensitivity.y * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, settings.cameraSettings.angleYClamp.x, settings.cameraSettings.angleYClamp.y);

            
            yRotation += delta.x * settings.cameraSettings.sensitivity.x * Time.deltaTime;
            
            if(settings.cameraSettings.angleXClamp != Vector2.zero)
                yRotation = Mathf.Clamp(yRotation, settings.cameraSettings.angleXClamp.x, settings.cameraSettings.angleXClamp.y);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            camParent.transform.localRotation = Quaternion.Euler(Vector3.up * yRotation);
        }
    }
}