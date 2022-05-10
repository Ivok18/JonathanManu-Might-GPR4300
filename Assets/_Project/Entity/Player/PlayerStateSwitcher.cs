using UnityEngine;
using Might.Entity.Player.States;

namespace Might.Entity.Player
{
    public class PlayerStateSwitcher : MonoBehaviour
    {
        private PlayerStateTracker playerStateTracker;

        public delegate void StateSwitchedCallback(PlayerState state);
        public static event StateSwitchedCallback OnStateSwitchedCallback;

        private void Awake()
        {
            playerStateTracker = GetComponent<PlayerStateTracker>();
        }

        public void SwitchToState(PlayerState newState)
        {
            playerStateTracker.CurrentState = newState;

            OnStateSwitchedCallback?.Invoke(newState);
        }
    }
}
