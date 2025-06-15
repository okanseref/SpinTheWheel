using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Tween
{
    public class TweenInfiniteZRotation : MonoBehaviour
    {
        [SerializeField] private float _duration = 3f;
        
        private DG.Tweening.Tween _tween;
        
        private void Start()
        {
            _tween = transform.DOLocalRotate(new Vector3(0, 0, 360f), _duration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

        private void OnDestroy()
        {
            _tween.Kill();
        }
    }
}