using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Tween
{
    public class TweenInfiniteZRotation : MonoBehaviour
    {
        [SerializeField] private float _duration = 3f;

        private void Start()
        {
            transform.DOLocalRotate(new Vector3(0, 0, 360f), _duration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}