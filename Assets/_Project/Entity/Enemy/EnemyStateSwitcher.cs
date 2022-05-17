using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Enemy
{
    public class EnemyStateSwitcher : MonoBehaviour
    {
        private EnemyStateTracker enemyStateTracker;

        public delegate void EnemyStateSwitched(EnemyState newState);
        public event EnemyStateSwitched OnEnemyStateSwitched;

        private void Awake()
        {
            enemyStateTracker = GetComponent<EnemyStateTracker>();
        }

        public void SwitchToState(EnemyState newState)
        {
            enemyStateTracker.CurrentState = newState;

            OnEnemyStateSwitched?.Invoke(newState);
        }
    }
}
