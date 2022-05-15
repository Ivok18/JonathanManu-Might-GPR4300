using Pathfinding;
using UnityEngine;

namespace Might.Entity.Enemy.States
{
    public class AttackState : MonoBehaviour
    {

        [SerializeField] private float swordStartRotation;
        [SerializeField] private float swordRotationModifier;
        [SerializeField] private Transform sword;

        

        private void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.Attacking) return;

            //Perform attack until the move is completed
            Debug.Log("Attacking");

        
            
            if(AttackIsCompleted())
            {             
                #region Get enemy AI
                AIPath enemyAI = GetComponent<AIPath>();
                #endregion
                if(enemyAI.reachedDestination)
                {
                    #region Get enemy state switcher
                    EnemyStateSwitcher enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();
                    #endregion
                    enemyStateSwitcher.SwitchToState(EnemyState.ChargingAttack);
                }
                else
                {
                    #region Get enemy state switcher
                    EnemyStateSwitcher enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();
                    #endregion
                    enemyStateSwitcher.SwitchToState(EnemyState.FollowingPlayer);
                }
            }
        }

        private bool AttackIsCompleted()
        {
            return true;
        }

        

        
    }
}
