using DG.Tweening;
using Might.Entity.Enemy;
using Might.Entity.Player;
using System;
using UnityEngine;

namespace Might.Entity
{
    public class ReceiveDamageAnim : MonoBehaviour
    {
        [SerializeField] private float fadeIntensity;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float animDuration;
        private EnemyHealth enemyHealth;
        private Sequence receiveDamageAnimation;
        private Color fadeColor;
        private Color startColor;

        private void Awake()
        {
            enemyHealth = GetComponent<EnemyHealth>();
        }

        private void OnEnable()
        {
            if (enemyHealth != null)
            {
                enemyHealth.OnEnemyReceivedDamageCallback += HandleEnemyReceivedDamage;
            }
            PlayerHealth.OnPlayerReceivedDamageCallback += HandlePlayerReceivedDamage;

        }

        private void OnDisable()
        {
            if(enemyHealth != null)
            {
                enemyHealth.OnEnemyReceivedDamageCallback -= HandleEnemyReceivedDamage;
            }
            PlayerHealth.OnPlayerReceivedDamageCallback -= HandlePlayerReceivedDamage;          
        }
             
        private void HandlePlayerReceivedDamage(float newHealth)
        {
            StartAnim();
        }

        private void HandleEnemyReceivedDamage(float newHealth)
        {
            StartAnim();
        }

        void Start()
        {
            startColor = spriteRenderer.color;
            fadeColor = spriteRenderer.color;
            fadeColor.a = fadeIntensity;
        }


        public void StartAnim()
        {
            Tweener colorFade = spriteRenderer.DOColor(fadeColor, animDuration);
            colorFade.ChangeStartValue(startColor);
            receiveDamageAnimation = DOTween.Sequence(spriteRenderer);
            receiveDamageAnimation.Append(colorFade);
            receiveDamageAnimation.SetLoops(2, LoopType.Yoyo);
        }
    }
}
