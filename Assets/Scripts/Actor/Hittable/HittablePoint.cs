using UnityEngine;

namespace Actor.Hittable
{
    public class HittablePoint : MonoBehaviour, IHittable
    {
        public HealthManager HealthManager;
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