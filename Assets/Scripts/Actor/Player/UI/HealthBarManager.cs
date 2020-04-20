using System;
using UnityEngine;
using UnityEngine.UI;

namespace Actor.Player.UI
{
    public class HealthBarManager : MonoBehaviour
    {
        public Slider slider;
        
        public PlayerHealth health;
        public PlayerController controller;

        private void OnEnable()
        {
            health.OnTakingDamage += OnTakingDamage;
            controller.OnRespawn += OnRespawn;
        }

        private void OnDisable()
        {
            health.OnTakingDamage -= OnTakingDamage;
            controller.OnRespawn -= OnRespawn;
        }

        private void Start()
        {
            slider.value = health.health / health.maxHealth;
        }

        private void OnRespawn(object sender, EventArgs e)
        {
            slider.value = 1;
        }

        private void OnTakingDamage(object sender, float e)
        {
            slider.value = health.health / health.maxHealth;
        }
    }
}