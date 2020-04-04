using UnityEngine;

namespace Actor
{
    [CreateAssetMenu(menuName = "Actor/Movement")]
    public class ActorMovementSettings : ScriptableObject
    {
        public MovementParams MovementParams;
    }
}