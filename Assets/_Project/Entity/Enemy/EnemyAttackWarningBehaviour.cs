using Might.Entity.Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Enemy
{
    public class EnemyAttackWarningBehaviour : MonoBehaviour
    {
        private EnemyAIChargeAttackState enemyAIChargeAttack;
        [SerializeField] private float timeForActivation;
        [SerializeField] private SpriteRenderer attackWarningSprite;

        private void Awake()
        {
            enemyAIChargeAttack = GetComponent<EnemyAIChargeAttackState>();
        }

        
        // Update is called once per frame
        void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.ChargingAttack) return;

            if (enemyAIChargeAttack.timeUntilNextAttack <= timeForActivation)
            {
                attackWarningSprite.enabled = true;
            }
        }

    }
}