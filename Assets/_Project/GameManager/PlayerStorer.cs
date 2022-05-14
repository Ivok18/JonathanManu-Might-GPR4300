using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.GameManager
{
    public class PlayerStorer : MonoBehaviour
    {
        [SerializeField] private Transform player;

        public delegate void PlayerStoredCallback(Transform player);
        public static event PlayerStoredCallback OnPlayerStoredCallback;

        private void OnEnable()
        {
            PlayerSpawner.OnPlayerSpawnCallback += StorePlayer;
        }

        private void OnDisable()
        {
            PlayerSpawner.OnPlayerSpawnCallback -= StorePlayer;
        }

        public void StorePlayer(Transform transform)
        {
            player = transform;

            OnPlayerStoredCallback?.Invoke(player);
        }
    }
}
