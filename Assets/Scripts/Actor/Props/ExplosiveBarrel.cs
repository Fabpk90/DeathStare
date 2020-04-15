using Actor.Hittable;
using UnityEngine;

namespace Actor.Props
{
    public class ExplosiveBarrel : PropsHealthManager
    {
        public float explosionRadius;
        
        
        private int playerThatMadeMeGoBOOM;
        private Collider[] cachedCollisions = new Collider[30];

        public override bool TakeDamage(int playerIndex, float amount)
        {
            if (base.TakeDamage(playerIndex, amount))
            {
                playerThatMadeMeGoBOOM = playerIndex;
                return true;
            }

            return false;
        }

        public override void Die()
        {
            base.Die();

            int foundColliders = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, cachedCollisions);

            for (int i = 0; i < foundColliders; i++)
            {
                IHittable hit = cachedCollisions[i].GetComponent<IHittable>();
                //TODO: hit that thing and make sure to not hit it more than once
            }
            
            Destroy(gameObject);
        }
    }
}