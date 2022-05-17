using UnityEngine;
using DG.Tweening;


namespace Might.Entity.Player.States
{
    public class AttackStateBehaviour : MonoBehaviour
    {
        [SerializeField] private float swordStartRotation;
        [SerializeField] private float swordRotationModifier;
        [SerializeField] private float attackDuration;
        [SerializeField] private Transform sword;
        [SerializeField] private Vector3 swordOnBackAngle;
        [SerializeField] private Vector3 swordDrawPosition;

        private float attackCooldown;   
        public float SwordStartRotation
        {
            get => swordStartRotation;
            set => swordStartRotation = value;
        }

        public float SwordRotationModifier
        {
            get => swordRotationModifier;
            set => swordRotationModifier = value;
        }
        public float EndRotation { get; set; }
        public Transform Sword
        {
            get => sword;
            set => sword = value;
        }


        private void OnEnable()
        {
            PlayerStateSwitcher.OnPlayerStateSwitched += HandlePlayerStateSwitched;
        }
        private void HandlePlayerStateSwitched(PlayerState newState)
        {
            if (newState == PlayerState.Attacking)
            {
                //Desactivate player shield
                DefendStateBehaviour defendStateBehaviour = GetComponent<DefendStateBehaviour>();
                defendStateBehaviour.DesactivateShield();

                //Allow player to move
                //PlayerMovement playerMovement = GetComponent<PlayerMovement>();
                //playerMovement.RestoreMovement();

                //Draw player sword
                DrawSword();

                //Set sword angle before slash attack
                SetSwordRotation(SwordStartRotation);
            }
        }

        private void OnDisable()
        {
            PlayerStateSwitcher.OnPlayerStateSwitched -= HandlePlayerStateSwitched;
        }

        void Update()
        {
          
            #region Get player state tracker
            PlayerStateTracker playerStateTracker = GetComponent<PlayerStateTracker>();
            #endregion
            #region Get player state switcher 
            PlayerStateSwitcher playerStateSwitcher;
            playerStateSwitcher = GetComponent<PlayerStateSwitcher>();
            #endregion

            //Update timer for attack cooldown
            if (attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }             
            else
            {
                //Trigger attack when left mouse click
                if (Input.GetMouseButtonDown(0))
                {
                    //Ensure cps limit
                    attackCooldown = 1f / 2.7f;
                    playerStateSwitcher.SwitchToState(PlayerState.Attacking);
                }
            }
            

            //Stop update method if player is not attacking
            if (playerStateTracker.CurrentState != PlayerState.Attacking) return;

            //Slash with sword
            #region Prevent capacity overload (just some details dw)
            DOTween.SetTweensCapacity(10000, 10000);
            #endregion
            EndRotation = GetSwordRotation() + SwordRotationModifier;
            Sword.DORotate(new Vector3(0, 0, EndRotation), attackDuration, RotateMode.Fast);

            //Switch state at the end of slash attack
            if (SlashIsCompleted())
            {
                if (!Input.GetMouseButtonDown(0))
                {
                    playerStateSwitcher.SwitchToState(PlayerState.None);
                }
                else
                {
                    SetSwordRotation(SwordStartRotation);
                }               
            }
   
        }

        public void SetSwordRotation(float rotation)
        {
            Sword.localEulerAngles = new Vector3(0,0, rotation);
        }

        public float GetSwordRotation()
        {
            return transform.localEulerAngles.z;
        }

        public bool SlashIsCompleted()
        {
            return Sword.localEulerAngles.z >= EndRotation;
        }

        public void PutSwordOnPlayerBack()
        {
            SpriteRenderer spriteRenderer = Sword.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.transform.localPosition = Vector3.zero;
            spriteRenderer.transform.localEulerAngles = swordOnBackAngle;
           
        }

        public void DrawSword()
        {
            SpriteRenderer spriteRenderer = Sword.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.transform.localPosition = swordDrawPosition;
            spriteRenderer.transform.localEulerAngles = Vector3.zero;
        }



       

    }
}