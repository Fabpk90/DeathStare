using System;
using UnityEngine;

namespace Actor.Player.Movement
{
    [Serializable]
    public class MovementSettingsHelper
    {
        public float gravityMultiplier;
        [Header("RUN")]
        public float runSpeedForward;
        public float runSpeedBackwards;
		[Header("WALK")]
		public float walkSpeedForward;
        public float walkSpeedBackwards;
        
        [Range(0f, 1f)] 
        public float runstepLenghten;

		[Header("JUMP")]
		public float jumpSpeed;
		[Header("CROUCH")]
		public float crouchingSpeedForward;
        public float crouchingSpeedBackwards;
        public float crouchingHeightCollider;
		[Header("AERIAL")]
		public float aerialSpeedForward;
        public float aerialSpeedBackwards;
        public float stareAerialSpeedForward;
        public float stareAerialSpeedBackwards;
		[Header("STARE")]
		public float stareWalkingSpeedForward;
        public float stareWalkingSpeedBackwards;
        [Space]
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