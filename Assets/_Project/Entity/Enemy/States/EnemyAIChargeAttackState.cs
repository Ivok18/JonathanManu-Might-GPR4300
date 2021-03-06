using Pathfinding;
using UnityEngine;

namespace Might.Entity.Enemy.States
{
    public class EnemyAIChargeAttackState : MonoBehaviour
    {
        [SerializeField] private float timeBetweenAttacks;
        public float timeUntilNextAttack;

       

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
            if(newState == EnemyState.ChargingAttack)
            {
             
                #region Get enemy AI
                AIPath enemyAI = GetComponent<AIPath>();
                #endregion
                //Enable aim by disabling AStar AI rotation
                enemyAI.enableRotation = false;

                #region Get attack warning 
                EnemyAttackWarningBehaviour attackWarning;
                attackWarning = GetComponent<EnemyAttackWarningBehaviour>();
                #endregion
                //Desactivate attack warning
                attackWarning.AttackWarningSprite.enabled = false;
            }
        }

        private void Start()
        {
            timeUntilNextAttack = timeBetweenAttacks;
        }

        void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.ChargingAttack) return;

            #region Get enemy attack script
            EnemyAIAttackState attackState = GetComponent<EnemyAIAttackState>();
            #endregion
            //Make sure the sword points straight forward when enemy not attacking
            attackState.SetSwordRotation(0);
    
            #region Get enemy AI
            AIPath enemyAI = GetComponent<AIPath>();
            #endregion 
            if (enemyAI.reachedDestination)
            {
                timeUntilNextAttack -= Time.deltaTime;

                #region Get enemy state switcher
                EnemyStateSwitcher enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();
                #endregion
                if (timeUntilNextAttack <= 0)
                {
                    timeUntilNextAttack = timeBetweenAttacks;
                    enemyStateSwitcher.SwitchToState(EnemyState.Attacking);
                }
            }
            else
            {                     
                #region Get enemy state switcher
                EnemyStateSwitcher enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();
                #endregion
                enemyStateSwitcher.SwitchToState(EnemyState.FollowingPlayer);                           
            }
        }
    }
}
