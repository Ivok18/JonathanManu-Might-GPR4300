using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        public delegate void PlayerReceivedDamageCallback(float newHealth, float maxHealth);
        public static event PlayerReceivedDamageCallback OnPlayerReceivedDamageCallback;

        public delegate void PlayerDiedCallback(GameObject player);
        public static event PlayerDiedCallback OnPlayerDiedCallback;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void ReceiveDamage(float damage)
        {
            currentHealth -= damage;

            OnPlayerReceivedDamageCallback?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                OnPlayerDiedCallback?.Invoke(gameObject);
            }
        }

    }
}
