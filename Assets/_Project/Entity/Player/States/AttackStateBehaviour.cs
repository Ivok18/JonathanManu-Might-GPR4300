using UnityEngine;
using DG.Tweening;
using System;

namespace Might.Entity.Player.States
{
    public class AttackStateBehaviour : MonoBehaviour
    {
        [SerializeField] private float swordStartRotation;
        [SerializeField] private float swordRotationModifier;
        [SerializeField] private float attackDuration;
        [SerializeField] private Transform sword;
        private float timeUntilNextAttack;
       
        
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
            PlayerStateSwitcher.OnStateSwitchedCallback += HandleStateSwitch;
        }

        private void OnDisable()
        {
            PlayerStateSwitcher.OnStateSwitchedCallback -= HandleStateSwitch;
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

            //Ensure cps limit
            if (timeUntilNextAttack > 0)
            {
                timeUntilNextAttack -= Time.deltaTime;
            }             
            else
            {
                //Trigger attack when key is pressed
                if (Input.GetMouseButtonDown(0))
                {
                    timeUntilNextAttack = 1f / 2.7f;
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

        private void HandleStateSwitch(PlayerState newState)
        {
            if (newState == PlayerState.Attacking)
            {               
                SetSwordRotation(SwordStartRotation);
            }       
        }

    }
}