using Might.Entity.Enemy.States;
using Might.Entity.Player;
using UnityEngine;

namespace Might.Entity.Enemy
{
    public class EnemyTargetHitDetection : MonoBehaviour
    {
        private EnemyStateTracker enemyStateTracker;
        private EnemyAIAttackState attackState;

        private void Awake()
        {
            enemyStateTracker = GetComponentInParent<EnemyStateTracker>();
            attackState = GetComponentInParent<EnemyAIAttackState>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && enemyStateTracker.CurrentState == EnemyState.Attacking)
            {
                #region Get Target State
                PlayerStateTracker playerStateTracker;
                playerStateTracker = collision.GetComponent<PlayerStateTracker>();
                #endregion

                //Damage player if he is not shielding
                if(playerStateTracker.CurrentState != PlayerState.Defending)
                {
                    PlayerHealth target = collision.GetComponent<PlayerHealth>();
                    target.ReceiveDamage(attackState.Atk);
                }
                //Knockback enemy if player is shielding
                else
                {
                    #region Get enemy state switcher
                    EnemyStateSwitcher enemyStateSwitcher;
                    enemyStateSwitcher = GetComponentInParent<EnemyStateSwitcher>();
                    #endregion
                    enemyStateSwitcher.SwitchToState(EnemyState.IsBeingWeakened);
                }
            }
        }
    }
}
