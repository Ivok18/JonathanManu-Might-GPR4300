using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Might.Entity.Player.States
{
    public class DefendStateBehaviour : MonoBehaviour
    {

        [SerializeField] private Transform shield;
        [SerializeField] private Vector3 startScale;
        private Sequence shieldAnimation;

        public Transform Shield
        {
            get => shield;
            set => shield = value;
        }

        public Sequence ShieldAnimation
        {
            get => shieldAnimation;
            set => shieldAnimation = value;
        }


        private void OnEnable()
        {
            PlayerStateSwitcher.OnStateSwitchedCallback += HandleStateSwitch;
        }

        private void HandleStateSwitch(PlayerState newState)
        {
            if (newState == PlayerState.Defending)
            {
                //Activate Shield
                ActivateShield();
                
                //Stop player movement (à voir..)
                //PlayerMovement playerMovement = GetComponent<PlayerMovement>();
                //playerMovement.ImmobilizePlayer();
            
                //Put sword on player back
                AttackStateBehaviour attackStateBehaviour = GetComponent<AttackStateBehaviour>();
                attackStateBehaviour.PutSwordOnPlayerBack();               
            }
        }

        private void OnDisable()
        {
            PlayerStateSwitcher.OnStateSwitchedCallback -= HandleStateSwitch;
        }

        void Update()
        {
            #region Get player state tracker
            PlayerStateTracker playerStateTracker;
            playerStateTracker = GetComponent<PlayerStateTracker>();
            #endregion
            #region Get player state switcher 
            PlayerStateSwitcher playerStateSwitcher;
            playerStateSwitcher = GetComponent<PlayerStateSwitcher>();
            #endregion

            //Trigger defense when right mouse is pressed
            if (Input.GetMouseButtonDown(1))
            {
                playerStateSwitcher.SwitchToState(PlayerState.Defending);
            }

            //Stop update method if player is not defending
            if (playerStateTracker.CurrentState != PlayerState.Defending) return;

        }

        public void ActivateShield()
        {
            //Enable game object
            Shield.gameObject.SetActive(true);

            //Show animation
            Tweener shieldAnimSet = Shield.DOScale(0.9f, 0.2f);
            shieldAnimSet.ChangeStartValue(startScale);
            shieldAnimation = DOTween.Sequence(Shield);
            shieldAnimation.Append(shieldAnimSet);
            shieldAnimation.SetLoops(-1, LoopType.Yoyo);
        }

        public void DesactivateShield()
        {
            //Hide game object and hide animation too
            Shield.gameObject.SetActive(false);           
        }

       
    }
}
