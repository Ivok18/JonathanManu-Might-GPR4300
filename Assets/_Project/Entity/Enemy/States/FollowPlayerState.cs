using Pathfinding;
using UnityEngine;


namespace Might.Entity.Enemy.States
{
    public class FollowPlayerState : MonoBehaviour
    {
        private AIPath enemyAI;

        private void Awake()
        {
            enemyAI = GetComponent<AIPath>();
        }

        private void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.FollowingPlayer) return;
      
            if(enemyAI.reachedDestination)
            {             
                 #region Get enemy state switcher
                 EnemyStateSwitcher enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();
                 #endregion
                 enemyStateSwitcher.SwitchToState(EnemyState.ChargingAttack);
            }
        }
    }
}
