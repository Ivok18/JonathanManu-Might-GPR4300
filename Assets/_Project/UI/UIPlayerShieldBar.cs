using UnityEngine;
using Might.GameManager;
using Might.Entity.Player;
using Might.Entity.Player.States;
using UnityEngine.UI;

namespace Might.UI
{
    public class UIPlayerShieldBar : MonoBehaviour
    {
        private PlayerStateTracker playerStateTracker;
        private DefendStateBehaviour defendState;
        private Slider slider;



        private void Awake()
        {
            slider = GetComponent<Slider>();
        }
        private void OnEnable()
        {
            PlayerSpawner.OnPlayerSpawnCallback += HandlePlayerSpawn;
            
        }

        private void OnDisable()
        {
            PlayerSpawner.OnPlayerSpawnCallback -= HandlePlayerSpawn;
        }

        private void HandlePlayerSpawn(Transform player)
        {
            
            playerStateTracker = player.GetComponent<PlayerStateTracker>();
            defendState = player.GetComponent<DefendStateBehaviour>();
        }

        private void Update()
        {
            if (playerStateTracker.CurrentState != PlayerState.Defending) return;

            float shieldRemainingTime = defendState.ShieldRemainingTime;
            float shieldMaxTime = defendState.ShieldMaxTime;
            slider.value = shieldRemainingTime / shieldMaxTime;
        }
    }
}
