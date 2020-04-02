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
            Stare.HittablePoints.Add(this);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public bool TakeDamage(int amount)
        {
            return healthManager.TakeDamage(amount);
        }

        public void AddForce(Vector3 force)
        {
            rigidBody.AddForce(force);
        }
    }
}