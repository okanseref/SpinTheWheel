using System;
using DG.Tweening;
using Game.Signal;
using UI.SpinArea.Signal;
using UnityEngine;
using Utils;

namespace UI.SpinArea
{
    public class SpinAreaRollManager : MonoBehaviour
    {
        [SerializeField] private Transform _rollAreaRoot;

        [SerializeField] private float duration = 2f; // Duration in seconds
        [SerializeField] private float delayAtEnd = 0.5f; // Duration in seconds
        [SerializeField] private int fullTurns = 2;   // Number of full 360° turns
        [SerializeField] private Ease easeType = Ease.Linear;

        private void Start()
        {
            SignalBus.Instance.Subscribe<PrepareSpinSignal>(ResetRoll);
            SignalBus.Instance.Subscribe<StartRollSignal>(StartRoll);
        }

        public void StartRoll(StartRollSignal signal)
        {
            // Calculate total rotation angle
            float totalAngle = (fullTurns * 360f) + signal.RollResult * 45;
        
            // Rotate around Z-axis (change the vector for other axes)
            _rollAreaRoot.DORotate(new Vector3(0, 0, totalAngle), duration, RotateMode.FastBeyond360)
                .SetEase(easeType)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(delayAtEnd, () => SignalBus.Instance.Fire<RollCompletedSignal>());
                });
        }

        public void ResetRoll()
        {
            _rollAreaRoot.transform.localRotation = Quaternion.identity;
        }
    }
}