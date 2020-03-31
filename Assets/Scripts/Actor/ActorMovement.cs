using System;
using UnityEngine;

namespace Actor
{

    [Serializable]
    public struct SMovementParams
    {
        public float maxSpeed;
        public float movementSpeed;
        public float fallingSpeed;
    }
    
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public abstract class ActorMovement : MonoBehaviour
    {
        public ActorMovementSettings settings;
        
        public bool isGrounded;

        private CapsuleCollider _collider;
        private Rigidbody _rigidbody;

        public Vector3 movement;
        
        public virtual void OnStart() {}
        public virtual void OnUpdate() {}

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            
            OnStart();
        }

        private void Update()
        {
            CheckForGround();
            UpdateMovement();

            OnUpdate();
        }

        private void UpdateMovement()
        {
            //TODO: limit the velocity
            _rigidbody.AddForce(movement * settings.MovementParams.movementSpeed);
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

        private void CheckForGround()
        {
            if (Physics.SphereCast(transform.position - Vector3.down * _collider.radius / 2, 1, Vector3.down,
                out var hitInfo))
            {
                isGrounded = true;
                Vector3 rigidbodyVelocity = _rigidbody.velocity;
                rigidbodyVelocity.y = 0;

                _rigidbody.velocity = rigidbodyVelocity;
            }
        }
    }
}