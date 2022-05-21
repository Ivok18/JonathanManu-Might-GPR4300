using DG.Tweening;
using Might.Entity.Enemy;
using UnityEngine;

namespace Might.Entity.Player
{
    public class ReceiveDamageAnimPlayer : MonoBehaviour
    {
        [SerializeField] private float endColorAlpha;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float animDuration;
        private Sequence receiveDamageAnimation;
        private Color endColor;
        private Color startColor;

        private void OnEnable()
        {         
            PlayerHealth.OnPlayerReceivedDamageCallback += HandlePlayerReceivedDamage;
        }

        private void OnDisable()
        {   
            PlayerHealth.OnPlayerReceivedDamageCallback -= HandlePlayerReceivedDamage;          
        }
             
        private void HandlePlayerReceivedDamage(float newHealth, float maxHealth)
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
