using System;
using UnityEngine;

namespace LD
{
    public enum ESurfaceType
    {
        WOOD,
        METAL,
        CONCRETE
    }
    
    public class SoundSurfaceInfo : MonoBehaviour
    {
        public ESurfaceType type;

        private void OnCollisionEnter(Collision other)
        {
            //TODO: notify the type of surface   
        }
    }
}