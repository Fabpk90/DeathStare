using UnityEngine;

namespace Actor
{
    [CreateAssetMenu(menuName = "Actor/CameraMovement")]
    public class ActorCameraMovementSettings : ScriptableObject
    {
        public CameraMovementSettings cameraSettings;
    }
}