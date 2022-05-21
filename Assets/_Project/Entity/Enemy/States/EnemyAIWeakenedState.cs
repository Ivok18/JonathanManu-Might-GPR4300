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

                    //Start stun animation
                }          
            }
        }

        private void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.IsBeingWeakened) return;

            


        }

        /*public void PutSwordOnEnemyBack()
        {
            EnemyAIAttackState enemyAIAttack = GetComponent<EnemyAIAttackState>();
            SpriteRenderer spriteRenderer = enemyAIAttack.Sword.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.transform.localPosition = Vector3.zero;
            spriteRenderer.transform.localEulerAngles = swordOnBackAngle;

        }*/
    }
}
