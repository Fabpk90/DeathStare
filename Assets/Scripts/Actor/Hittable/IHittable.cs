using UnityEngine;

namespace Actor.Hittable
{
    public interface IHittable
    {
        Vector3 GetPosition();
        /// <summary>
        /// Called when this takes a "hit"
        /// </summary>
        /// <param name="playerIndex">The player that has done damage, if none set this to -1</param>
        /// <param name="amount"></param>
        /// <returns>true if this is dead, false otherwise</returns>
        bool TakeDamage(int playerIndex,float amount);
    }
}
