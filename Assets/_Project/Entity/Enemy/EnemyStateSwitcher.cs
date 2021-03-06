using System.Collections;
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

        public IEnumerator SwitchToStateWithDelay(EnemyState newState, float delay)
        {
            yield return new WaitForSeconds(delay);

            enemyStateTracker.CurrentState = newState;

            OnEnemyStateSwitched?.Invoke(newState);
        }

    }
}
