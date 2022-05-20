using DG.Tweening;
using UnityEngine;

namespace Might.Entity.Enemy
{
    public class EnemyHealth : MonoBehaviour
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
