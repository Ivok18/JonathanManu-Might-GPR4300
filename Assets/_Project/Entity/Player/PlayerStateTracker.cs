using UnityEngine;

namespace Might.Entity.Player
{
    public class PlayerStateTracker : MonoBehaviour
    {
        [SerializeField] private PlayerState currentState;

        public PlayerState CurrentState
        {
            get => currentState;
            set => currentState = value;
        }
    }
}
