using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Might.Entity
{
    public class ReceiveDamageAnim : MonoBehaviour
    {
        [SerializeField] private float fadeIntensity;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float animDuration;
        private Sequence receiveDamageAnimation;
        private Color fadeColor;
        private Color startColor;
        
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
