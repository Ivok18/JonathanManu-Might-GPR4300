using Might.Entity.Player.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Player
{
    public class ShieldRegenBehaviour : MonoBehaviour
    {

        [SerializeField] private float regenSpeed;

        private PlayerStateTracker stateTracker;
        private DefendStateBehaviour defendState;

        private void Awake()
        {
            stateTracker = GetComponent<PlayerStateTracker>();
            defendState = GetComponent<DefendStateBehaviour>();
        }
        private void Update()
        {
            if (stateTracker.CurrentState == PlayerState.Defending) return;

            if(defendState.ShieldRemainingTime < defendState.ShieldMaxTime)
            {
                defendState.ShieldRemainingTime += Time.deltaTime * regenSpeed;
            }
            
        }
    }
}
