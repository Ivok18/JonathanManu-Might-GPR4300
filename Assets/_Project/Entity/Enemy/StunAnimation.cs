using DG.Tweening;
using UnityEngine;

namespace Might.Entity.Enemy
{
    public class StunAnimation : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color endColor;
        [SerializeField] private float animDuration;

        public Sequence StunSequence { get; set; }
        public Color StartColor { get; set; }

        private void Start()
        {
            StartColor = spriteRenderer.color;
        }

        public void StartAnim()
        {
            Tweener colorFade = spriteRenderer.DOColor(endColor, animDuration);
            colorFade.ChangeStartValue(spriteRenderer.color);
            StunSequence = DOTween.Sequence(spriteRenderer);
            StunSequence.Append(colorFade);
            StunSequence.SetLoops(-1, LoopType.Yoyo);
        }

    }
}
