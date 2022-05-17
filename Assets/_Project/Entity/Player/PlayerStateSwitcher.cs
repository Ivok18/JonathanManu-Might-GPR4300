using UnityEngine;
using Might.Entity.Player.States;

namespace Might.Entity.Player
{
    public class PlayerStateSwitcher : MonoBehaviour
    {
        private PlayerStateTracker playerStateTracker;

        public delegate void PlayerStateSwitched(PlayerState state);
        public static event PlayerStateSwitched OnPlayerStateSwitched;

        private void Awake()
        {
            playerStateTracker = GetComponent<PlayerStateTracker>();
        }

        public void SwitchToState(PlayerState newState)
        {
            playerStateTracker.CurrentState = newState;

            OnPlayerStateSwitched?.Invoke(newState);
        }
    }
}
