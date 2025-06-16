using DG.Tweening;
using Exchange;
using Game.Signal;
using UI.Exchange;
using UnityEngine;
using Utils;

namespace UI.Popup
{
    public class RewardPopup : BasePopup
    {
        [SerializeField] private Transform rewardRootTransform;
        [SerializeField] private Transform cardRootTransform;

        private RewardPopupData _rewardPopupData;
        private ExchangeView _exchangeView;
    
        public override void Init<T>(T data)
        {
            _rewardPopupData = data as RewardPopupData;
        }

        void Start()
        {
            _exchangeView = ExchangeViewFactory.Instance.CreateExchangeView(_rewardPopupData.Reward, rewardRootTransform);
        
            var startScale = cardRootTransform.transform.localScale;
        
            Sequence sequence = DOTween.Sequence();
            sequence.Append(cardRootTransform.transform.DOScale(startScale * 1.2f, 0.2f));
            sequence.Append(cardRootTransform.transform.DOScale(startScale * 0.5f, 0.1f));
            sequence.OnComplete(OnAnimationEnded);
        }

        private void OnAnimationEnded()
        {
            ExchangeViewFactory.Instance.ReturnExchangeView(_exchangeView);
            SignalBus.Instance.Fire(new RewardGivenSignal(_rewardPopupData.Reward));
            Hide();
        }

        public override void Show()
        {
            //
        }
    }

    public class RewardPopupData
    {
        public ExchangeData Reward { get; }
    
        public RewardPopupData(ExchangeData reward)
        {
            Reward = reward;
        }
    }
}