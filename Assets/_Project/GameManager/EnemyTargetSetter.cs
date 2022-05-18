using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.GameManager
{
    public class EnemyTargetSetter : MonoBehaviour
    {
        [SerializeField] private Transform enemyTarget;

        private void OnEnable()
        {
            PlayerStorer.OnPlayerStoredCallback += ReceiveEnemyTargetInfo;
            EnemySpawner.OnEnemySpawnedCallback += SetEnemyTarget;
        }

        private void OnDisable()
        {
            PlayerStorer.OnPlayerStoredCallback -= ReceiveEnemyTargetInfo;
            EnemySpawner.OnEnemySpawnedCallback -= SetEnemyTarget;
        }

        public void ReceiveEnemyTargetInfo(Transform target)
        {
            enemyTarget = target;
            //Debug.Log(enemyTarget.name);
        }

        public void SetEnemyTarget(Transform enemy)
        {
            StartCoroutine("DelaySetEnemyTarget", enemy);          
        }

        public IEnumerator DelaySetEnemyTarget(Transform enemy)
        {
            yield return new WaitForSeconds(0.01f);
            AIDestinationSetter enemyAI = enemy.GetComponent<AIDestinationSetter>();
            enemyAI.target = enemyTarget;
        }
       
    }
}