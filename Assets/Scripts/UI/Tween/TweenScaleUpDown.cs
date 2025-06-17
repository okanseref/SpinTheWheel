using DG.Tweening;
using UnityEngine;

namespace UI.Tween
{
    public class TweenScaleUpDown : MonoBehaviour
    {
        [SerializeField] private float _scaleUpSize = 1.1f;
        [SerializeField] private float _duration = 0.7f;
        private Vector3 startScale;
        
        private Sequence sequence;

        private void Start()
        {
            startScale = transform.localScale;
            sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(startScale * (_scaleUpSize), _duration).SetEase(Ease.Linear));
            sequence.Append(transform.DOScale(startScale, _duration).SetEase(Ease.Linear));
            sequence.SetLoops(-1, LoopType.Restart);
        }

        private void OnDestroy()
        {
            sequence.Kill();
        }
    }
}