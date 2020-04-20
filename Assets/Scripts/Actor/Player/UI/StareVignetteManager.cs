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

        private CooldownTimer _timer;

        private void OnEnable()
        {
            stare.OnStareStart += OnStareStart;
            stare.OnStareStop += OnStareStop;
        }

        private void Start()
        {
            _timer = new CooldownTimer(vignettingTime);
        }

        private void Update()
        {
            _timer.Update(Time.deltaTime);
        }

        private void OnStareStop(object sender, EventArgs e)
        {
            isVignetting = false;
            
        }

        private void OnStareStart(object sender, EventArgs e)
        {
            isVignetting = true;
            //_timer
        }
        
        private void OnDisable()
        {
            stare.OnStareStart -= OnStareStart;
            stare.OnStareStop -= OnStareStop;
        }
    }
}