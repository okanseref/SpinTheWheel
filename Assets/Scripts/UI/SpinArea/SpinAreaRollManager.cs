using System;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace UI.SpinArea
{
    public class SpinAreaRollManager : Singleton<SpinAreaRollManager>
    {
        [SerializeField] private Transform _rollAreaRoot;

        [SerializeField] private float duration = 2f; // Duration in seconds
        [SerializeField] private int fullTurns = 2;   // Number of full 360° turns
        [SerializeField] private RotateMode rotateMode = RotateMode.FastBeyond360;
        [SerializeField] private Ease easeType = Ease.Linear;
        
        public void StartRoll(int winIndex, Action onComplete)
        {
            // Calculate total rotation angle
            float totalAngle = (fullTurns * 360f) + winIndex * 45;
        
            // Rotate around Z-axis (change the vector for other axes)
            _rollAreaRoot.DORotate(new Vector3(0, 0, totalAngle), duration, rotateMode)
                .SetEase(easeType)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(0.5f, () => onComplete?.Invoke());
                });
        }

        public void ResetRoll()
        {
            _rollAreaRoot.transform.localRotation = Quaternion.identity;
        }
    }
}