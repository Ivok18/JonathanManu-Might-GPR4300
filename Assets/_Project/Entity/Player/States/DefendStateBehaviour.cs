using UnityEngine;
using DG.Tweening;

namespace Might.Entity.Player.States
{
    public class DefendStateBehaviour : MonoBehaviour
    {

        [SerializeField] private Transform shield;
        [SerializeField] private Vector3 startScale;

        public Transform Shield
        {
            get => shield;
            set => shield = value;
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

                //Set starting scale
                //Shield.localScale = startScale;

                //Stop player movement
                PlayerMovement playerMovement = GetComponent<PlayerMovement>();
                playerMovement.ImmobilizePlayer();
            
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

            //Shield animation
            #region Prevent capacity overload (just some details dw)
            DOTween.SetTweensCapacity(10000, 10000);
            #endregion
            //Shield.DOScale(1,0.5f);
           

        }

        public void ActivateShield()
        {
            Shield.gameObject.SetActive(true);
        }

        public void DesactivateShield()
        {
            Shield.gameObject.SetActive(false);
        }
   
    }
}
