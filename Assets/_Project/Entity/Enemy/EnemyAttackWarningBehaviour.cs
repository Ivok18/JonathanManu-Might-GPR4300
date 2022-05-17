using DG.Tweening;
using Might.Entity.Enemy.States;
using Pathfinding;
using UnityEngine;

namespace Might.Entity.Enemy
{
    public class EnemyAttackWarningBehaviour : MonoBehaviour
    {    
        [SerializeField] private SpriteRenderer attackWarningSprite;
        [SerializeField] private Vector3 startScale;
        [SerializeField] private float timeForActivation;
        [SerializeField] private float animActivationRange;
        private EnemyAIChargeAttackState enemyAIChargeAttack;
        private Sequence attackWarningAnimation;
       
       

        public SpriteRenderer AttackWarningSprite
        {
            get => attackWarningSprite;
            set => attackWarningSprite = value;
        }

        

        private void Awake()
        {
            enemyAIChargeAttack = GetComponent<EnemyAIChargeAttackState>();        
        }

        private void Start()
        {
            //Create Or Show animation 
            Tweener attackWarningSet = attackWarningSprite.transform.parent.DOScale(0.9f, 0.2f);
            attackWarningSet.ChangeStartValue(startScale);
            attackWarningAnimation = DOTween.Sequence(attackWarningSprite.transform.parent);
            attackWarningAnimation.Append(attackWarningSet);
            attackWarningAnimation.SetLoops(-1, LoopType.Yoyo);
        }

        void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            if (enemyStateTracker.CurrentState == EnemyState.ChargingAttack ||
                enemyStateTracker.CurrentState == EnemyState.Attacking)
            {

                #region Get enemy AI path
                AIPath enemyAI = GetComponent<AIPath>();
                #endregion
                #region Get enemy stop distance
                EnemyAIFollowPlayerState followPlayerState;
                followPlayerState = GetComponent<EnemyAIFollowPlayerState>();
                float stopDistance = followPlayerState.StopDistance;
                #endregion
                if (enemyAIChargeAttack.timeUntilNextAttack <= timeForActivation ||
                    enemyAI.remainingDistance > stopDistance &&
                    enemyAI.remainingDistance < stopDistance + animActivationRange
                    )
                {
                    attackWarningSprite.enabled = true;
                }
            }


            else if(enemyStateTracker.CurrentState == EnemyState.FollowingPlayer)
            {
                #region Get enemy AI path
                AIPath enemyAI = GetComponent<AIPath>();
                #endregion
                #region Get enemy stop distance
                EnemyAIFollowPlayerState followPlayerState;
                followPlayerState = GetComponent<EnemyAIFollowPlayerState>();
                float stopDistance = followPlayerState.StopDistance;
                #endregion
                if (enemyAI.remainingDistance > stopDistance + animActivationRange)
                {
                    Debug.Log("nani");
                    attackWarningSprite.enabled = false;
                }
                
            }
        }

    }
}