using UnityEngine;

namespace Actor.Props
{
    public class PropsHealthManager : HealthManager
    {
        public bool takesDamage;
        public override bool TakeDamage(float amount)
        {
            if(takesDamage)
                return base.TakeDamage(amount);
            return false;
        }
    }
}