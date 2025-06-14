using System.Collections.Generic;
using Exchange;
using UI.Exchange;
using UnityEngine;
using Utils;

namespace UI.RewardArea
{
    public class RewardAreaManager : Singleton<RewardAreaManager>
    {
        [SerializeField] private Transform RewardRootTransform;
        
        private Dictionary<ExchangeData, ExchangeView> RewardViewDictionary = new();
        private List<ExchangeData> Rewards = new();
        
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
        }

        public void Reset()
        {
            foreach (var exchangeView in RewardViewDictionary.Values)
            {
                ExchangeViewFactory.Instance.ReturnExchangeView(exchangeView);
            }
            
            RewardViewDictionary.Clear();
            Rewards.Clear();
        }
    }
}