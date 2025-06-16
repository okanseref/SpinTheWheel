using System;
using System.Collections.Generic;
using Exchange;
using Game;
using Game.Signal;
using Inventory;
using UI.Exchange;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.RewardArea
{
    public class RewardAreaManager : Singleton<RewardAreaManager>
    {
        [SerializeField] private Button ExitButton;
        [SerializeField] private Transform RewardRootTransform;
        
        private Dictionary<ExchangeData, ExchangeView> RewardViewDictionary = new();
        private List<ExchangeData> Rewards = new();

        private void Start()
        {
            ExitButton.onClick.AddListener(OnExitClicked);
        }

        public void AddReward(ExchangeData exchangeData)
        {
            var itemInRewards = Rewards.Find((x) => x.Type == exchangeData.Type && x.Subtype == exchangeData.Subtype);

            if (itemInRewards != null)
            {
                // Reward already exists add
                itemInRewards.Value += exchangeData.Value;
                
                RewardViewDictionary.TryGetValue(itemInRewards, out var view);
                if (view != null)
                {
                    view.SetValueText(itemInRewards.Value);
                }
            }
            else
            {
                // New reward
                var newReward = exchangeData.Clone();
                Rewards.Add(newReward);
                var exchangeView = ExchangeViewFactory.Instance.CreateExchangeView(newReward, RewardRootTransform);
                RewardViewDictionary.Add(newReward, exchangeView);
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(RewardRootTransform as RectTransform);
        }

        public void ResetArea(bool giveRewards)
        {
            if(giveRewards)
                InventoryManager.Instance.AddExchangeDatas(Rewards);
            
            foreach (var exchangeView in RewardViewDictionary.Values)
            {
                ExchangeViewFactory.Instance.ReturnExchangeView(exchangeView);
            }
            
            RewardViewDictionary.Clear();
            Rewards.Clear();
        }

        private void OnExitClicked()
        {
            if(!GameStateManager.Instance.IsFreeState())
                return;
            
            if (Rewards.Count > 0)
            {
                PopupManager.Instance.Show<ExitPopup, ExitPopupData>(new ExitPopupData(Rewards));
            }
        }

        private void OnValidate()
        {
            ExitButton = GameObject.Find("ui_button_exit").GetComponent<Button>();
        }
    }
}