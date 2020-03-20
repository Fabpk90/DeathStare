using Actor.Hittable;
using UnityEngine;

namespace Actor.Player
{
    public class PlayerHealth : HealthManager
    {
        public override void Die()
        {
            print("Argh " + name);
        }
    }
}
