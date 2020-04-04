using System;
using UnityEngine;

namespace Actor.Hittable
{
    [RequireComponent(typeof(Collider))]
    public class HittablePoint : MonoBehaviour, IHittable
    {
        public HealthManager HealthManager;

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
            return HealthManager.TakeDamage(amount);
        }
    }
}