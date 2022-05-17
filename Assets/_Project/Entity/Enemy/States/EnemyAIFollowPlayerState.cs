using Pathfinding;
using UnityEngine;


namespace Might.Entity.Enemy.States
{
    public class EnemyAIFollowPlayerState : MonoBehaviour
    {
        private AIPath enemyAI;

        private void Awake()
        {
            enemyAI = GetComponent<AIPath>();
        }
        private void OnEnable()
        {
            EnemyStateSwitcher enemyStateSwitcher;
            enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();

            enemyStateSwitcher.OnEnemyStateSwitched += HandleEnemyStateSwitched;
        }
        private void OnDisable()
        {
            EnemyStateSwitcher enemyStateSwitcher;
            enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();

            enemyStateSwitcher.OnEnemyStateSwitched -= HandleEnemyStateSwitched;
        }

        private void HandleEnemyStateSwitched(EnemyState newState)
        {
            if (newState == EnemyState.FollowingPlayer)
            {
                #region Get enemy AI
                AIPath enemyAI = GetComponent<AIPath>();
                #endregion
                //Disable aim by enabling AStar AI rotation
                enemyAI.enableRotation = true;
            }
        }

        private void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.FollowingPlayer) return;

            //Make sure the sword rotation is 0 when enemy not attacking
            EnemyAIAttackState attackState = GetComponent<EnemyAIAttackState>();
            attackState.SetSwordRotation(0);

            if (enemyAI.reachedDestination)
            {             
                 #region Get enemy state switcher
                 EnemyStateSwitcher enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();
                 #endregion
                 enemyStateSwitcher.SwitchToState(EnemyState.Attacking);
            }
        }
    }
}
