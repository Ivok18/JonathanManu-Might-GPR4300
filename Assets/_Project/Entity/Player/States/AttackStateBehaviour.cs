using UnityEngine;
using DG.Tweening;

namespace Might.Entity.Player.States
{
    public class AttackStateBehaviour : MonoBehaviour
    {
        [SerializeField] private float swordStartRotation;
        [SerializeField] private float swordEndRotation;
        [SerializeField] private float attackSpeed;
        [SerializeField] private Transform sword;
       

        public float SwordStartRotation
        {
            get => swordStartRotation;
            set => swordStartRotation = value;
        }

        public float SwordEndRotation
        {
            get => swordEndRotation;
            set => swordEndRotation = value;
        }

      
        void Update()
        {
            PlayerStateTracker playerStateTracker;
            playerStateTracker = GetComponent<PlayerStateTracker>();
            if (playerStateTracker.CurrentState != PlayerState.Attacking) return;

            DOTween.SetTweensCapacity(1000, 1000);
            sword.DORotate(new Vector3(0, 0, SwordStartRotation - 30), 0.2f, RotateMode.Fast);   
        }



        public void SetSwordRotation(float rotation)
        {
            sword.localEulerAngles = new Vector3(0,0, rotation);
        }
    }
}