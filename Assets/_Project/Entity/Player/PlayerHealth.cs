using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        public delegate void PlayerReceivedDamageCallback(float newHealth);
        public static event PlayerReceivedDamageCallback OnPlayerReceivedDamageCallback;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void ReceiveDamage(float damage)
        {
            currentHealth -= damage;

            OnPlayerReceivedDamageCallback?.Invoke(currentHealth);
        }
    }
}
