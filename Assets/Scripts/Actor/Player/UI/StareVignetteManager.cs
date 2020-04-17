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

        public StareHandler stare;

        private float currentTime;

        public float normalizedTime;

        private void OnEnable()
        {
            stare.OnStareStart += OnStareStart;
            stare.OnStareStop += OnStareStop;
        }

        private void Update()
        {
            
        }

        private void OnStareStop(object sender, EventArgs e)
        {
            isVignetting = false;
            
        }

        private void OnStareStart(object sender, EventArgs e)
        {
            isVignetting = true;
            currentTime = 0.0f;
        }
        
        private void OnDisable()
        {
            stare.OnStareStart -= OnStareStart;
            stare.OnStareStop -= OnStareStop;
        }
    }
}