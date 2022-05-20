using Might.Entity.Enemy;
using UnityEngine;

namespace Might.Entity.Player
{
    public class PlayerTargetHitDetection : MonoBehaviour
    {
        private PlayerStateTracker playerStateTracker;

        private void Awake()
        {
            playerStateTracker = GetComponentInParent<PlayerStateTracker>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy") && playerStateTracker.CurrentState == PlayerState.Attacking)
            {
                EnemyHealth targetHealth = collision.GetComponent<EnemyHealth>();
                targetHealth.ReceiveDamage(1);
            }
        }

    }
}
