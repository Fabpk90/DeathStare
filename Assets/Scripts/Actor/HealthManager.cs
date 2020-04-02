using System.Collections.Generic;
using Actor.Hittable;
using UnityEngine;

namespace Actor
{
    public abstract class HealthManager : MonoBehaviour, IHittable
    {
        public int maxHealth;
        private int health;

        public List<HittablePoint> points;

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public virtual bool TakeDamage(int amount)
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
            //TODO: remove the points from the pool

            foreach (HittablePoint point in points)
            {
                Stare.HittablePoints.Remove(point);
            }
            
        }
    }
}