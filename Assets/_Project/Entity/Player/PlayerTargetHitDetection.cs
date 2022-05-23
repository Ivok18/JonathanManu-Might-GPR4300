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
                #region Get Target State && Health
                EnemyStateTracker enemyStateTracker;
                enemyStateTracker = collision.GetComponent<EnemyStateTracker>();
                EnemyHealth target = collision.GetComponent<EnemyHealth>();
                #endregion 
                if (enemyStateTracker.CurrentState != EnemyState.IsBeingWeakened)
                {                    
                    target.ReceiveDamage(1);
                }
                else
                {         
                    target.ReceiveDamage(3);
                }
                
            }
        }

    }
}
