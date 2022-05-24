using UnityEngine;

namespace Might.Entity.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        public delegate void PlayerHealthChangedCallback(float oldHealth, float newHealth, float maxHealth);
        public static event PlayerHealthChangedCallback OnPlayerHealthChangedCallback;

        public delegate void PlayerDiedCallback(GameObject player);
        public static event PlayerDiedCallback OnPlayerDiedCallback;


        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void ReceiveDamage(float damage)
        {
            float oldHealth = currentHealth;

            currentHealth -= damage;

            OnPlayerHealthChangedCallback?.Invoke(oldHealth, currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                OnPlayerDiedCallback?.Invoke(gameObject);
            }
        }

       
    }
}
