using System;
using UnityEngine;
using UnityEngine.UI;
using Sweet.UI;

namespace Actor.Player.UI
{
    public class HealthBarManager : MonoBehaviour
    {
		public UISlider uiSlider;
        
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
			uiSlider.value = health.health / health.maxHealth;
        }

        private void OnRespawn(object sender, EventArgs e)
        {
			uiSlider.value = 1;
        }

        private void OnTakingDamage(object sender, float e)
        {
			uiSlider.value = health.health / health.maxHealth;
        }
    }
}