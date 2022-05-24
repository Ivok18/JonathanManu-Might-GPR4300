using DG.Tweening;
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
            PlayerHealth.OnPlayerHealthChangedCallback += HandlePlayerHealthChanged;
        }

        private void OnDisable()
        {   
            PlayerHealth.OnPlayerHealthChangedCallback -= HandlePlayerHealthChanged;          
        }
             
        private void HandlePlayerHealthChanged(float oldHealth, float newHealth, float maxHealth)
        {
            if(newHealth < oldHealth)
            {
                StartAnim();
            }
           
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
