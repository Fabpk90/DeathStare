using Actor.Hittable;
using UnityEngine;

namespace Actor.Player
{
    public class PlayerHealth : HealthManager
    {
        public Transform[] hittablePoints;
        public override void Die()
        {
            print("Argh " + name);
            
            //Destroy(gameObject);
        }
    }
}
