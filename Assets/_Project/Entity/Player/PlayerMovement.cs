using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private Vector2 movement;
        private Rigidbody2D rb;
       
    
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            //Handle movement
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            movement.Normalize();

            //Handle rotation
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDirection = mousePosition - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;

        }

        private void FixedUpdate()
        {
            rb.velocity = movement * moveSpeed * Time.fixedDeltaTime;
        }
    }
}
