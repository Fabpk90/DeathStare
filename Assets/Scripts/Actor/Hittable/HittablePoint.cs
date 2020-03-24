using UnityEngine;

namespace Actor.Hittable
{
    public class HittablePoint : MonoBehaviour, IHittable
    {
        public HealthManager HealthManager;
        public bool TakeDamage(int amount)
        {
            return HealthManager.TakeDamage(amount);
        }
    }
}