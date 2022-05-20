using Might.Entity.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Enemy
{
    public class EnemyTargetHitDetection : MonoBehaviour
    {
        private EnemyStateTracker enemyStateTracker;

        private void Awake()
        {
            enemyStateTracker = GetComponentInParent<EnemyStateTracker>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && enemyStateTracker.CurrentState == EnemyState.Attacking)
            {
                PlayerHealth targetHealth = collision.GetComponent<PlayerHealth>();
                targetHealth.ReceiveDamage(1);
            }
        }
    }
}
