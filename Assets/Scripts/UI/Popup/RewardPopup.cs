using DG.Tweening;
using Exchange;
using UI.Exchange;
using UI.Popup;
using UnityEngine;

public class RewardPopup : BasePopup
{
    [SerializeField] private Transform rewardRootTransform;

    private RewardPopupData _rewardPopupData;
    private ExchangeView _exchangeView;
    
    public override void Init<T>(T data)
    {
        _rewardPopupData = data as RewardPopupData;
    }

    void Start()
    {
        _exchangeView = ExchangeViewFactory.Instance.CreateExchangeView(_rewardPopupData.GetReward(), rewardRootTransform);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rewardRootTransform.transform.DOScale(1.2f, 1f));
        sequence.Append(rewardRootTransform.transform.DOScale(0.5f, 0.3f));
        sequence.OnComplete(OnAnimationEnded);
    }

    private void OnAnimationEnded()
    {
        ExchangeViewFactory.Instance.ReturnExchangeView(_exchangeView);
        Hide();
    }
}

public class RewardPopupData
{
    private ExchangeData _reward;

    public RewardPopupData(ExchangeData reward)
    {
        _reward = reward;
    }

    public ExchangeData GetReward()
    {
        return _reward;
    }
}
