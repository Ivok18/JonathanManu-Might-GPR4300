using DG.Tweening;
using UnityEngine;

namespace Might.Entity.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        public delegate void EnemyReceivedDamageCallback(float newHealth);
        public event EnemyReceivedDamageCallback OnEnemyReceivedDamageCallback;


        
        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void ReceiveDamage(float damage)
        {
            currentHealth -= damage;

            OnEnemyReceivedDamageCallback?.Invoke(currentHealth);

            if(currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }

            
    }
}