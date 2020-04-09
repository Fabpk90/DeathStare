using System;
using UnityEngine;

namespace Actor.Player.Movement
{
    [Serializable]
    public class MovementSettingsHelper
    {
        public float gravityMultiplier;
        
        public float runSpeed;
        public float walkSpeed;
        [Range(0f, 1f)] 
        public float runstepLenghten;
        
        public float jumpSpeed;
        public float crouchingSpeed;
        public float crouchingHeightCollider;

        public float aerialSpeed;
        public float stareAerialSpeed;
        public float stareWalkingSpeed;
        
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