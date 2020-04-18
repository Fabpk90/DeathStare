using System;
using UnityEngine;

namespace Actor.Hittable
{
    [RequireComponent(typeof(Collider))]
    public class HittablePoint : MonoBehaviour, IHittable
    {
        public HealthManager healthManager;
        public Rigidbody rigidBody;

        private void Awake()
        {
            //TODO: find a cleaner way
            StareHandler.HittablePoints.Add(this);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public bool TakeDamage(int playerIndex, float amount)
        {
            return healthManager.TakeDamage(playerIndex, amount);
        }

        public void AddForce(Vector3 force)
        {
            rigidBody.AddForce(force);
        }
    }
}