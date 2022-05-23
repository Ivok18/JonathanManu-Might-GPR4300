using UnityEngine;
using Might.Entity.Player.States;
using System;

namespace Might.Entity.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private float currentMoveSpeed;
        private Vector2 movement;
        private Rigidbody2D rb;

        private void OnEnable()
        {
            PlayerHealth.OnPlayerDeath += DisablePlayerMovement;
        }

        private void OnDisable()
        {
            PlayerHealth.OnPlayerDeath -= DisablePlayerMovement;
        }
      

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            currentMoveSpeed = moveSpeed;
            EnablePlayerMovement();
        }

        private void Update()
        {
            #region Get player state tracker
            PlayerStateTracker playerStateTracker = GetComponent<PlayerStateTracker>();
            #endregion

            //Handle movement          
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            movement.Normalize();
            
               
            //Handle rotation
            if(playerStateTracker.CurrentState != PlayerState.Attacking)
            {         
                //Make sure sword rotation is 0 when not attacking
                AttackStateBehaviour attackStateBehaviour;
                attackStateBehaviour = GetComponent<AttackStateBehaviour>();
                attackStateBehaviour.SetSwordRotation(0);

                //Make the player rotation follow the mouse
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 aimDirection = mousePosition - rb.position;
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = aimAngle;
            }
           
        }

      

        private void FixedUpdate()
        {
            rb.velocity = movement * currentMoveSpeed * Time.fixedDeltaTime;                    
        }

        public void ImmobilizePlayer()
        {
            currentMoveSpeed = 0;
        }

        public void RestoreMovement()
        {
            currentMoveSpeed = moveSpeed;
        }

        private void DisablePlayerMovement()
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        private void EnablePlayerMovement()
        {
            rb.bodyType = RigidbodyType2D.Dynamic; 
        }
    }
}
