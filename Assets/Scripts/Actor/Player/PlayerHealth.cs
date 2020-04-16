using System;
using UnityEngine;

namespace Actor.Player
{
    public class PlayerHealth : HealthManager
    {
        private PlayerController _controller;
        private CooldownTimer _timer;

        public bool canTakeDamage = true;

        public void ActivateInvicibility(float time)
        {
            canTakeDamage = false;
            
            _timer.Start(time);
        }

        private void Start()
        {
            _controller = GetComponent<PlayerController>();
            _timer = new CooldownTimer(0);

            _timer.TimerCompleteEvent += () =>
            {
                canTakeDamage = true;
            };
        }

        private void Update()
        {
            _timer.Update(Time.deltaTime);
        }

        public override void Die()
        {
            //Animation maybe ?
            //Sound ?
        }

        public override bool TakeDamage(int playerIndex, float amount)
        {
            if (!canTakeDamage) return false;
            if (!base.TakeDamage(playerIndex, amount)) return false;
            
            GameMode.instance.PlayerKilled(playerIndex, _controller.GetPlayerIndex());
            return true;

        }
    }
}
