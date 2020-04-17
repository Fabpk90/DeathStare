using System;
using UnityEngine;

namespace Actor.Player.Stare
{
    public class StareVignetteManager : MonoBehaviour
    {
        public float vignettingTime;
        public AnimationCurve curve;
        private float _vignetting;

        private bool isVignetting;


        public void StartVignetting()
        {
            isVignetting = true;
        }

        public void StopVignetting()
        {
            isVignetting = false;
        }
    }
}