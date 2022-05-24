using Might.Entity.Player;
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
            PlayerHealth.OnPlayerHealthChangedCallback += HandlePlayerHealthChanged;
        }


        private void OnDisable()
        {
            PlayerHealth.OnPlayerHealthChangedCallback -= HandlePlayerHealthChanged;
        }

        private void HandlePlayerHealthChanged(float oldHealth, float newHealth, float maxHealth)
        {
            UpdateBar(newHealth, maxHealth);
        }

        public void UpdateBar(float newHealth, float maxHealth)
        {
           slider.value = newHealth / maxHealth;
        }
    }
}
