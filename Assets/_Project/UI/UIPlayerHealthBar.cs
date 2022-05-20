using Might.Entity.Player;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Might.UI
{
    public class UIPlayerHealthBar : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }
        private void OnEnable()
        {
            PlayerHealth.OnPlayerReceivedDamageCallback += HandlePlayerReceivedDamage;
        }


        private void OnDisable()
        {
            PlayerHealth.OnPlayerReceivedDamageCallback -= HandlePlayerReceivedDamage;
        }

        private void HandlePlayerReceivedDamage(float newHealth, float maxHealth)
        {
            UpdateBar(newHealth, maxHealth);
        }

        public void UpdateBar(float newHealth, float maxHealth)
        {
            slider.value = newHealth / maxHealth;
        }
    }
}
