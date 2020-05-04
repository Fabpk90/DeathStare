using System;
using System.Collections.Generic;
using Actor.Hittable;
using UnityEngine;

namespace Actor
{
    public abstract class HealthManager : MonoBehaviour, IHittable
    {
        public float maxHealth;
        public float health;

        public List<HittablePoint> points;

        private void Awake()
        {
            health = maxHealth;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public virtual bool TakeDamage(int playerIndex, float amount)
        {
//            print("I'm " + transform.name);
            if (health - amount <= 0)
            {
                Die();
                return true;
            }

            health -= amount;
        
            return false;
        }

        public virtual void Die()
        {
            foreach (HittablePoint point in points)
            {
                StareHandler.HittablePoints.Remove(point);
            }

        }
    }
}