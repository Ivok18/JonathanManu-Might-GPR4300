using Might.Entity.Player;
using UnityEngine;

namespace Might.GameManager
{
    public class PlayerDestroyer : MonoBehaviour
    {
        private void OnEnable()
        {
            PlayerHealth.OnPlayerDiedCallback += HandlePlayerDied;
            
        }

        private void OnDisable()
        {
            PlayerHealth.OnPlayerDiedCallback -= HandlePlayerDied;
        }

        private void HandlePlayerDied(GameObject player)
        {
            Destroy(player);
        }
    }
}
