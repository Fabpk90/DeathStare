using System;
using UnityEditor;
using UnityEngine;

namespace Actor
{

    [Serializable]
    public class MovementParams
    {
        public float maxSpeed;
        public float movementSpeed;
        public float gravity;

        public float jumpHeight;
    }
    
    [RequireComponent(typeof(CharacterController))]
    public abstract class ActorMovement : MonoBehaviour
    {
        public ActorMovementSettings settings;
        
        public bool isGrounded;
        private CharacterController _characterController;

        public Vector3 movement;

        private Vector3 velocity;

        public bool isJumping;
        
        public virtual void OnStart() {}
        public virtual void OnUpdate() {}

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();

            OnStart();
        }

        private void Update()
        {
            UpdateMovement();

            OnUpdate();
        }

        private void UpdateMovement()
        {
            //TODO: limit the velocity
            var movementLocal = movement;
            var transform1 = transform;
            
            movementLocal = transform1.right * movementLocal.x + transform1.forward * movement.z;
            _characterController.Move(movementLocal * (settings.MovementParams.movementSpeed * Time.deltaTime));

            isGrounded = Physics.CheckSphere(transform1.position + Vector3.down * _characterController.height,
                _characterController.radius);

            if (isGrounded)
                velocity.y = -_characterController.height;
            
            if(isGrounded && isJumping)
            {
                print("yes");
                velocity.y = Mathf.Sqrt(settings.MovementParams.jumpHeight * -2f * settings.MovementParams.gravity);
                isJumping = false;
            }
            
            velocity.y += settings.MovementParams.gravity * Time.deltaTime;
            
            print(velocity);
            _characterController.Move(velocity * Time.deltaTime);
        }

        public void AddMovement(Vector3 v)
        {
            //TODO: limit velocity here or at the addforce 
            movement += v;
        }

        public void SetMovement(Vector3 v)
        {
            movement = v;
        }

        public Vector3 GetMovement()
        {
            return movement;
        }

        public void ResetMovement()
        {
            movement = Vector3.zero;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position + (Vector3.down * 1.6f ), 0.5f);
        }
    }
}