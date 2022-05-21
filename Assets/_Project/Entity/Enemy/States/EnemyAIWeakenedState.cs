using DG.Tweening;
using Pathfinding;
using UnityEngine;

namespace Might.Entity.Enemy.States
{
    public class EnemyAIWeakenedState : MonoBehaviour
    {
        Rigidbody2D rb;
        [SerializeField] private StunAnimation stunAnim;
        [SerializeField] private EnemyAttackWarningBehaviour attackWarningAnim;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float knockbackForce;
        [SerializeField] private float stateDuration;
        [SerializeField] private float timeUntilEndOfState;
 
 

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void OnEnable()
        {
            EnemyStateSwitcher enemyStateSwitcher;
            enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();

            enemyStateSwitcher.OnEnemyStateSwitched += HandleStateSwitch;
        }

        private void OnDisable()
        {
            EnemyStateSwitcher enemyStateSwitcher;
            enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();

            enemyStateSwitcher.OnEnemyStateSwitched -= HandleStateSwitch;
        }


        private void HandleStateSwitch(EnemyState newState)
        {
            if(newState == EnemyState.IsBeingWeakened)
            {
                #region Get enemy AI 
                AIPath enemyAI = GetComponent<AIPath>();
                #endregion
                //The code below executes itself ponly when player could move before entering weak state
                //(#hardcoding #sorry)
                if (enemyAI.canMove)
                {
                    //Apply knockback 
                    rb.AddForce(-transform.up * knockbackForce);
                    GetComponent<AIPath>().canMove = false;

                    //Stun
                    timeUntilEndOfState = stateDuration;
                    StunAnimation stunAnim = GetComponent<StunAnimation>();
                    stunAnim.StartAnim();
                }          
            }
        }

        private void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState != EnemyState.IsBeingWeakened) return;

            attackWarningAnim.AttackWarningSprite.enabled = false;

            timeUntilEndOfState -= Time.deltaTime;

            if(timeUntilEndOfState <= 0)
            {

                spriteRenderer.color = stunAnim.StartColor;

                stunAnim.StunSequence.Kill(false);

                #region Get enemy AI 
                AIPath enemyAI = GetComponent<AIPath>();
                #endregion
                enemyAI.canMove = true;

                #region Get enemy state switcher
                EnemyStateSwitcher enemyStateSwitcher;
                enemyStateSwitcher = GetComponent<EnemyStateSwitcher>();
                #endregion
                enemyStateSwitcher.SwitchToState(EnemyState.FollowingPlayer);
            }
        }


        

        
    }
}
