using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Enemy.States
{
    public class EnemyAIChargeAttackState : MonoBehaviour
    {
        [SerializeField] private float timeBetweenAttacks;
        [SerializeField] private float timeUntilNextAttack;

     

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

                timeUntilNextAttack = timeBetweenAttacks;

            }
        }

        void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.ChargingAttack) return;

            //Make sure the sword rotation is 0 when enemy not attacking
            EnemyAIAttackState attackState = GetComponent<EnemyAIAttackState>();
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
