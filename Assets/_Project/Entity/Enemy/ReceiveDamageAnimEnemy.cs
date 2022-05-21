using DG.Tweening;
using UnityEngine;

namespace Might.Entity.Enemy
{
    public class ReceiveDamageAnimEnemy : MonoBehaviour
    {
        [SerializeField] private float endColorAlpha;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float animDuration;
        private EnemyHealth enemyHealth;
        private Sequence receiveDamageAnimation;
        private Color endColor;
        private Color startColor;

        private void Awake()
        {
            enemyHealth = GetComponent<EnemyHealth>();
        }

        private void OnEnable()
        {          
            enemyHealth.OnEnemyReceivedDamageCallback += HandleEnemyReceivedDamage;
        }

        private void OnDisable()
        {
           
            enemyHealth.OnEnemyReceivedDamageCallback -= HandleEnemyReceivedDamage;
           
        }

        private void HandleEnemyReceivedDamage(float newHealth)
        {
             StartAnim();
        }

        void Start()
        {
            startColor = spriteRenderer.color;
            endColor = spriteRenderer.color;
            endColor.a = endColorAlpha;
        }


        public void StartAnim()
        {
            Tweener colorFade = spriteRenderer.DOColor(endColor, animDuration);
            colorFade.ChangeStartValue(startColor);
            receiveDamageAnimation = DOTween.Sequence(spriteRenderer);
            receiveDamageAnimation.Append(colorFade);
            receiveDamageAnimation.SetLoops(2, LoopType.Yoyo);
        }
    }
}
