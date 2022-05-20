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
                rb.AddForce(-transform.up * 2f);

                #region Get enemy AI
                AIPath enemyAI = GetComponent<AIPath>();
                enemyAI.canMove = false;
                #endregion
                ///velocity = (transform.position - GetComponent<AIDestinationSetter>().target.position) * -1 * 10;
            }
        }

    }
}
