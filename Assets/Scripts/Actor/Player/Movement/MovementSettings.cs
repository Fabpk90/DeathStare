using System;
using UnityEngine;

namespace Actor.Player.Movement
{
    [Serializable]
    public class MovementSettingsHelper
    {
        public float gravityMultiplier;
        
        public float runSpeedForward;
        public float runSpeedBackwards;
        public float walkSpeedForward;
        public float walkSpeedBackwards;
        
        [Range(0f, 1f)] 
        public float runstepLenghten;
        
        public float jumpSpeed;
        public float crouchingSpeedForward;
        public float crouchingSpeedBackwards;
        public float crouchingHeightCollider;

        public float aerialSpeedForward;
        public float aerialSpeedBackwards;
        public float stareAerialSpeedForward;
        public float stareAerialSpeedBackwards;
        
        public float stareWalkingSpeedForward;
        public float stareWalkingSpeedBackwards;
        
        public float stepInterval;
        public float stickToGroundForce;

        [Header("Cooldowns")] 
        public float jumpCooldown;
        public float stareCooldown;

    }
    
    [CreateAssetMenu(menuName = "Actor/MovementSettings")]
    public class MovementSettings : ScriptableObject
    {
        public MovementSettingsHelper parameters;
    }
}