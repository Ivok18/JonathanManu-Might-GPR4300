using UnityEngine;
using Might.Entity.Player.States;

namespace Might.Entity.Player
{
    public class PlayerStateSwitcher : MonoBehaviour
    {
        private PlayerStateTracker playerStateTracker;

        private void Awake()
        {
            playerStateTracker = GetComponent<PlayerStateTracker>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                SwitchTo(PlayerState.Attacking);
            }
        }

        public void SwitchTo(PlayerState newState)
        {
            if (newState == PlayerState.Attacking)
            {
                AttackStateBehaviour attackStateBehaviour = GetComponent<AttackStateBehaviour>();
                attackStateBehaviour.SwordStartRotation = transform.eulerAngles.z;
            }
            playerStateTracker.CurrentState = newState;       
        }
    }
}
