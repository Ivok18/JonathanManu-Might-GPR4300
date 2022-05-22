using Might.Entity.Enemy;
using UnityEngine;

namespace Might.Entity.Player
{
    public class PlayerTargetHitDetection : MonoBehaviour
    {
        private PlayerStateTracker playerStateTracker;

        public BoxCollider2D SwordCollider { get;set; }
       
        private void Awake()
        {
            playerStateTracker = GetComponentInParent<PlayerStateTracker>();
            SwordCollider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy") && playerStateTracker.CurrentState == PlayerState.Attacking)
            {
                Debug.Log("ouch");
                EnemyHealth targetHealth = collision.GetComponent<EnemyHealth>();
                targetHealth.ReceiveDamage(0);
            }
        }

    }
}
