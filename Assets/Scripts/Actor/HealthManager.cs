using Actor.Hittable;
using UnityEngine;

namespace Actor
{
    public abstract class HealthManager : MonoBehaviour, IHittable
    {
        public int maxHealth;
        private int health;

        public virtual bool TakeDamage(int amount)
        {
            if (health - amount <= 0)
            {
                Die();
                return true;
            }

            health -= amount;
        
            return false;
        }

        public abstract void Die();
    }
}