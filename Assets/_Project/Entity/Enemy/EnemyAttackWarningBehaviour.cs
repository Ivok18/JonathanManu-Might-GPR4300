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
        [SerializeField] private float animationEndScale;
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
            CreateOrContinueWarningAnimation();
        }
     

        void Update()
        {
            #region Get enemy state tracker
            EnemyStateTracker enemyStateTracker = GetComponent<EnemyStateTracker>();
            #endregion
            //Check current enemy state
            if (enemyStateTracker.CurrentState == EnemyState.ChargingAttack)
            {
                //Make sure we can show animation or not
                if (enemyAIChargeAttack.timeUntilNextAttack <= timeForActivation) 
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
                //Make sure we can show animation or not
                if (enemyAI.remainingDistance > stopDistance + animActivationRange)
                {
                    attackWarningSprite.enabled = false;
                }
                else if(enemyAI.remainingDistance >= stopDistance &&
                enemyAI.remainingDistance < stopDistance + animActivationRange)
                {
                    attackWarningSprite.enabled = true;
                }
                
            }
        }

        private void CreateOrContinueWarningAnimation()
        {
            Tweener attackWarningSet = attackWarningSprite.transform.parent.DOScale(animationEndScale, 0.2f);
            attackWarningSet.ChangeStartValue(startScale);
            attackWarningAnimation = DOTween.Sequence(attackWarningSprite.transform.parent);
            attackWarningAnimation.Append(attackWarningSet);
            attackWarningAnimation.SetLoops(-1, LoopType.Yoyo);
        }

    }
}