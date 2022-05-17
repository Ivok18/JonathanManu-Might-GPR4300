using DG.Tweening;
using Pathfinding;
using UnityEngine;

namespace Might.Entity.Enemy.States
{
    public class EnemyAIAttackState : MonoBehaviour
    {

        [SerializeField] private float swordStartRotation;
        [SerializeField] private float swordRotationOffset;
        [SerializeField] private Transform sword;

        public Transform Sword
        {
            get => sword;
            set => sword = value;
        }

        public float SwordStartRotation
        {
            get => swordStartRotation;
        }

        public float SwordRotationOffset
        {
            get => swordRotationOffset;
            set => swordRotationOffset = value;
        }

        public float EndRotation { get; set; }
        private void OnEnable()
        {
            EnemyStateSwitcher enemyStateSwitcher;
            enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();

            enemyStateSwitcher.OnEnemyStateSwitched += HandleEnemyStateSwitched;
        }
        private void OnDisable()
        {
            EnemyStateSwitcher enemyStateSwitcher;
            enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();

            enemyStateSwitcher.OnEnemyStateSwitched -= HandleEnemyStateSwitched;
        }

        private void HandleEnemyStateSwitched(EnemyState newState)
        {
            if (newState == EnemyState.Attacking)
            {              
                #region Get enemy AI
                AIPath enemyAI = GetComponent<AIPath>();
                #endregion
                //I disable AStar rotation because in another script I enable my custom rotation 
                //I do this because I need a certain behaviour that I cannot handle with Astar
                enemyAI.enableRotation = false;

                //I put the enemy sword in a certain before it starts attacking
                SetSwordRotation(SwordStartRotation);
                
            }
        }


        private void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.Attacking) return;

          



            //Perform attack until the move is complete
            #region Prevent capacity overload (just some details dw)
            DOTween.SetTweensCapacity(10000, 10000);
            #endregion
            EndRotation = GetSwordRotation() + SwordRotationOffset;
            Sword.DORotate(new Vector3(0, 0, EndRotation), 0.25f, RotateMode.Fast);



            if (AttackIsCompleted())
            {
                #region Get enemy AI
                AIPath enemyAI = GetComponent<AIPath>();
                #endregion
                if(enemyAI.reachedDestination)
                {
                    #region Get enemy state switcher
                    EnemyStateSwitcher enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();
                    #endregion
                    StartCoroutine(enemyStateSwitcher.SwitchToStateWithDelay(EnemyState.ChargingAttack));
                   
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

        public bool AttackIsCompleted()
        {
            return Sword.localEulerAngles.z >= EndRotation;
        }

        public void SetSwordRotation(float rotation)
        {
            Sword.localEulerAngles = new Vector3(0, 0, rotation);
        }

        public float GetSwordRotation()
        {
            return transform.localEulerAngles.z;
        }




    }
}
