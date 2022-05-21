using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Enemy.States
{
    public class EnemyAIWeakenedState : MonoBehaviour
    {
        Rigidbody2D rb;
        [SerializeField] private float knockbackForce;
        [SerializeField] private float stunDuration;
        [SerializeField] private float timeUntilEndOfStun;
 
 

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void OnEnable()
        {
            EnemyStateSwitcher enemyStateSwitcher;
            enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();

            enemyStateSwitcher.OnEnemyStateSwitched += HandleStateSwitch;
        }

        private void OnDisable()
        {
            EnemyStateSwitcher enemyStateSwitcher;
            enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();

            enemyStateSwitcher.OnEnemyStateSwitched -= HandleStateSwitch;
        }


        private void HandleStateSwitch(EnemyState newState)
        {
            if(newState == EnemyState.IsBeingWeakened)
            {
                #region Get enemy AI 
                AIPath enemyAI = GetComponent<AIPath>();
                #endregion
                //(#hardcoding #sorry)
                if (enemyAI.canMove)
                {
                                      
                    //Apply knockback only when player could move before entering weak state 
                    rb.AddForce(-transform.up * knockbackForce);
                    GetComponent<AIPath>().canMove = false;

                    //Start stun effect
                    timeUntilEndOfStun = stunDuration;
                }          
            }
        }

        private void Start()
        {
          
        }
        private void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.IsBeingWeakened) return;

           /* timeUntilEndOfStun -= Time.deltaTime;

            if(timeUntilEndOfStun <= 0)
            {
                #region Get enemy state switcher
                EnemyStateSwitcher enemyStateSwitcher;
                enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();
                #endregion
                enemyStateSwitcher.SwitchToState(EnemyState.FollowingPlayer);
            }*/
        }


        public void StartStunAnim()
        {

        }


        
    }
}
