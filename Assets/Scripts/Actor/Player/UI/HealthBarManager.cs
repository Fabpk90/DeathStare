using System;
using UnityEngine;
using UnityEngine.UI;

namespace Actor.Player.UI
{
    public class HealthBarManager : MonoBehaviour
    {

        public Slider slider;
        
        private PlayerHealth _health;

        private void Start()
        {
           _health = gameObject.GetComponentInParent<PlayerHealth>();
           gameObject.GetComponentInParent<PlayerController>().OnRespawn += OnRespawn;
           _health.OnTakingDamage += OnTakingDamage;

           slider.value = _health.health / _health.maxHealth;
        }

        private void OnRespawn(object sender, EventArgs e)
        {
            slider.value = 1;
        }

        private void OnTakingDamage(object sender, float e)
        {
            slider.value = _health.health / _health.maxHealth;
        }
    }
}