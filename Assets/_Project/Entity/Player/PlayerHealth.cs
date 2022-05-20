using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void ReceiveDamage(float damage)
        {
            currentHealth -= damage;

            #region Get receive damage anim script
            ReceiveDamageAnim receiveDamageAnim = GetComponent<ReceiveDamageAnim>();
            #endregion
            //Start anim
            receiveDamageAnim.StartAnim();

        }
    }
}
