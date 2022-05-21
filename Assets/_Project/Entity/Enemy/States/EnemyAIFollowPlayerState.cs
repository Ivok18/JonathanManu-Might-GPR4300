using Pathfinding;
using UnityEngine;


namespace Might.Entity.Enemy.States
{
    public class EnemyAIFollowPlayerState : MonoBehaviour
    {
        [SerializeField] private float stopDistance;
        private AIPath enemyAI;

        public float StopDistance
        {
            get => stopDistance;
            set => stopDistance = value;
        }

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

            #region Get enemy attack script
            EnemyAIAttackState attackState = GetComponent<EnemyAIAttackState>();
            #endregion
            //Make sure the sword is pointing straight forward when enemy not attacking
            attackState.SetSwordRotation(0);

            if(!enemyAI.canMove)
            {
                #region Get enemy state switcher
                EnemyStateSwitcher enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();
                #endregion
                enemyStateSwitcher.SwitchToState(EnemyState.IsBeingWeakened);
            }

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
