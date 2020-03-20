namespace Actor.Hittable
{
    public interface IHittable
    {
        /// <summary>
        /// Called when this takes a "hit"
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>true if this is dead, false otherwise</returns>
        bool TakeDamage(int amount);
    }
}
