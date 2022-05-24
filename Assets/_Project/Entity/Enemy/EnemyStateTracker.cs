using UnityEngine;

namespace Might.Entity.Enemy
{
    public class EnemyStateTracker : MonoBehaviour
    {
        [SerializeField] private EnemyState currentState;
        public EnemyState CurrentState
        {
            get => currentState;
            set => currentState = value;
        }
    }
}