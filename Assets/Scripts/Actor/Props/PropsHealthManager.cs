using UnityEngine;

namespace Actor.Props
{
    public class PropsHealthManager : HealthManager
    {
        public bool takesDamage;
        public override bool TakeDamage(int playerIndex, float amount)
        {
            if(takesDamage)
                return base.TakeDamage(playerIndex, amount);
            return false;
        }

        public override void Die()
        {
            base.Die();
            Destroy(gameObject);
        }
    }
}