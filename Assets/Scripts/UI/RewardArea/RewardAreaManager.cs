using System.Collections.Generic;
using Exchange;
using Game;
using Game.Signal;
using Reward;
using UI.Exchange;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.RewardArea
{
    public class RewardAreaManager : MonoBehaviour
    {
        [SerializeField] private Button ExitButton;
        [SerializeField] private Transform RewardRootTransform;
        
        private Dictionary<ExchangeData, ExchangeView> RewardViewDictionary = new();

        private void Start()
        {
            ExitButton.onClick.AddListener(OnExitClicked);
            
            SignalBus.Instance.Subscribe<AddRewardSignal>(AddReward);
            SignalBus.Instance.Subscribe<ResetRewardAreaSignal>(ResetArea);
        }

        public void AddReward(AddRewardSignal signal)
        {
            if (RewardViewDictionary.TryGetValue(signal.Reward, out var exchangeView))
            {
                exchangeView.SetValueText(signal.Reward.Value);
            }
            else
            {
                var exchangeViewNew = ExchangeViewFactory.Instance.CreateExchangeView(signal.Reward, RewardRootTransform);
                RewardViewDictionary.Add(signal.Reward, exchangeViewNew);
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(RewardRootTransform as RectTransform);
        }

        public void ResetArea(ResetRewardAreaSignal signal)
        {
            foreach (var exchangeView in RewardViewDictionary.Values)
            {
                ExchangeViewFactory.Instance.ReturnExchangeView(exchangeView);
            }
            
            RewardViewDictionary.Clear();
        }

        private void OnExitClicked()
        {
            if(!GameStateManager.Instance.IsFreeState())
                return;

            var rewards = RewardManager.Instance.GetRewards();
            
            if (rewards.Count > 0)
            {
                PopupManager.Instance.Show<ExitPopup, ExitPopupData>(new ExitPopupData(rewards));
            }
        }

        private void OnValidate()
        {
            ExitButton = GameObject.Find("ui_button_exit").GetComponent<Button>();
        }
    }
}