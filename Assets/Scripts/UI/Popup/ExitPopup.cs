using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exchange;
using Game.Signal;
using UI.Exchange;
using UI.Popup;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class ExitPopup : BasePopup
{
    [SerializeField] private Button ClaimButton;
    [SerializeField] private Button KeepPlayingButton;
    [SerializeField] private Transform RewardsRootTransform;

    private ExitPopupData _exitPopupData;
    private List<ExchangeView> _exchangeViews = new();

    public override void Init<T>(T data)
    {
        _exitPopupData = data as ExitPopupData;
    }

    void Start()
    {
        ClaimButton.onClick.AddListener(OnClaimClicked);
        KeepPlayingButton.onClick.AddListener(OnKeepPlayingClicked);

        foreach (var reward in _exitPopupData.Rewards)
        {
            var rewardVisual = ExchangeViewFactory.Instance.CreateExchangeView(reward, RewardsRootTransform);
            _exchangeViews.Add(rewardVisual);
        }
    }

    private void OnClaimClicked()
    {
        // TODO Give resources to user data for extra
        SignalBus.Instance.Fire<GameResetSignal>();
        Hide();
    }

    private void OnKeepPlayingClicked()
    {
        Hide();
    }

    private void OnDestroy()
    {
        foreach (var exchangeView in _exchangeViews)
        {
            ExchangeViewFactory.Instance.ReturnExchangeView(exchangeView);
        }
    }

    private void OnValidate()
    {
        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if(prefabStage == null)
            return;

        var buttons = GetComponentsInChildren<Button>(true);
            
        if (buttons == null || buttons.Length <= 0) return;
        var buttonsList = buttons.ToList();
        var claimButtonReference = buttonsList.Find((x) => x.name.Equals("ui_button_exit_popup_claim"));
        var keepPlayingButtonReference = buttonsList.Find((x) => x.name.Equals("ui_button_exit_popup_keep_playing"));
        ClaimButton = claimButtonReference != null ? claimButtonReference : ClaimButton;
        KeepPlayingButton = keepPlayingButtonReference != null ? keepPlayingButtonReference : KeepPlayingButton;
        
        UnityEditor.EditorUtility.SetDirty(this);
    }
}

public class ExitPopupData
{
    public List<ExchangeData> Rewards { get; }
    
    public ExitPopupData( List<ExchangeData> rewards)
    {
        Rewards = rewards;
    }
}