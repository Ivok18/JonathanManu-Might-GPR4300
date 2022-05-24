using Pathfinding;
using UnityEngine;

namespace Might.Entity.Enemy
{

    public class EnemyAimHandler : MonoBehaviour
    {
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.ChargingAttack &&
                enemyStateTracker.CurrentState != EnemyState.Attacking) return;

            #region Get enemy AI
            AIDestinationSetter enemyAI = GetComponent<AIDestinationSetter>();
            #endregion
            //Store target position if it exists
            Vector2 targetPosition = new Vector2();
            if (enemyAI.target !=null)
            {
              targetPosition = enemyAI.target.position;
            }
            

            //Update Aim
            Vector2 aimDirection = targetPosition - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }

       
    }
}
