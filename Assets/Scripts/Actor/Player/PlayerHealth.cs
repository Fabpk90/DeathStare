using System;
using UnityEngine;

namespace Actor.Player
{
    public class PlayerHealth : HealthManager
    {
        private PlayerController _controller;
        private CooldownTimer _timer;
        //Sound
        public PlayerAudioManager playerAudioManager;
        private bool damageSoundsPosted = false;
        //Sound

        public bool canTakeDamage = true;

        public event EventHandler<float> OnTakingDamage; 

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
            
            _controller.OnRespawn += OnRespawn;
        }

        private void OnRespawn(object sender, EventArgs e)
        {
            health = maxHealth;
        }

        private void Update()
        {
            _timer.Update(Time.deltaTime);
            playerAudioManager.SetRTPCValue("RTPC_Character_Health", health);
        }

        public override void Die()
        {
            //Animation maybe ?
            //Sound
            playerAudioManager.PostEvent("VO_Char_Barks_Death");
            //Sound
        }

        public override bool TakeDamage(int playerIndex, float amount)
        {
            if (!canTakeDamage) return false;

            print("I'm " + _controller.GetPlayerIndex() + " and " + playerIndex);
            
            OnTakingDamage?.Invoke(this, amount);
            
            if (!base.TakeDamage(playerIndex, amount)) return false;
            
            GameMode.instance.PlayerKilled(playerIndex, _controller.GetPlayerIndex());
            return true;

        }
    }
}
