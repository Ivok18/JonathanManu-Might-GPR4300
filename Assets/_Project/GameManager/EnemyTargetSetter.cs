using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.GameManager
{
    public class EnemyTargetSetter : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void OnEnable()
        {
            PlayerStorer.OnPlayerStoredCallback += ReceiveTargetInfo;
            //EnemySpawner.OnEnemySpawnedCallback += SetTargetOfEnemy;
        }

        private void OnDisable()
        {
            PlayerStorer.OnPlayerStoredCallback -= ReceiveTargetInfo;
            //EnemySpawner.OnEnemySpawnedCallback -= SetTargetOfEnemy;
        }

        public void ReceiveTargetInfo(Transform target)
        {
            this.target = target;
        }

       /* public void SetTargetOfEnemy(Transform enemy)
        {
            StartCoroutine("DelayTargetSetOfEnemy", enemy);
        }

        public IEnumerator DelayTargetSetOfEnemy(Transform enemy)
        {
            yield return new WaitForSeconds(0.05f);
            AIDestinationSetter enemyAI = enemy.GetComponent<AIDestinationSetter>();
            enemyAI.target = target;
        }*/

        
    }
}