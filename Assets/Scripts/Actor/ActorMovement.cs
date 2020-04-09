using System;
using UnityEditor;
using UnityEngine;

namespace Actor
{
    
    //TODO: maybe only keep this params class

    [Serializable]
    public class MovementParams
    {
        public float gravity;

        public float jumpHeight;
        public float maxSpeed;
        public float movementSpeed;
    }
    
    [RequireComponent(typeof(CharacterController))]
    public abstract class ActorMovement : MonoBehaviour
    {
        private CharacterController _characterController;
        private MovementParams _movementParams;

        public bool isGrounded;

        public bool isJumping;

        public Vector3 movement;
        public ActorMovementSettings settings;

        private Vector3 velocity;

        public virtual void OnStart() {}
        public virtual void OnUpdate() {}

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();

            _movementParams = settings.MovementParams;

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
            _characterController.Move(movementLocal * (_movementParams.movementSpeed * Time.deltaTime));

            //5 raycast ?
            Debug.DrawLine(transform1.position + Vector3.down ,
                transform1.position + Vector3.down  + (Vector3.down * 0.25f), Color.red, 2.0f);

            if (Physics.SphereCast(transform1.position, _characterController.radius, Vector3.down, out var hitinfo))
            {
                print(hitinfo.transform.gameObject);
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            if (isGrounded)
                velocity.y = -2f;
            
            if(isGrounded && isJumping)
            {
                print("yes");
                velocity.y = Mathf.Sqrt(_movementParams.jumpHeight * -2f * _movementParams.gravity);
                print(velocity);
                isJumping = false;
            }
            else
            {
                isJumping = false;
            }
            
            velocity.y += _movementParams.gravity * Time.deltaTime;
            
            _characterController.Move(velocity * Time.deltaTime);
        }

        public void SlowDownMovementSpeed(float amountDivision)
        {
            _movementParams.movementSpeed /= amountDivision;
        }

        public void RestoreMovementSpeed()
        {
            _movementParams.movementSpeed = settings.MovementParams.movementSpeed;
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
    }
}