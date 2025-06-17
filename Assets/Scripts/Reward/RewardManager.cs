using System.Collections.Generic;
using Exchange;
using Game.Signal;
using Inventory;
using Utils;

namespace Reward
{
    public class RewardManager : Singleton<RewardManager>
    {
        private List<ExchangeData> Rewards = new();

        public void AddReward(ExchangeData exchangeData)
        {
            var itemInRewards = Rewards.Find((x) => x.Type == exchangeData.Type && x.Subtype == exchangeData.Subtype);

            if (itemInRewards != null)
            {
                // Reward already exists add
                itemInRewards.Value += exchangeData.Value;
                SignalBus.Instance.Fire(new AddRewardSignal(itemInRewards));
            }
            else
            {
                // New reward
                var newReward = exchangeData.Clone();
                Rewards.Add(newReward);
                SignalBus.Instance.Fire(new AddRewardSignal(newReward));
            }
        }

        public void ResetRewards(bool giveRewards)
        {
            if(giveRewards)
                InventoryManager.Instance.AddExchangeDatas(Rewards);
            
            Rewards.Clear();
            SignalBus.Instance.Fire(new ResetRewardAreaSignal());
        }

        public List<ExchangeData> GetRewards()
        {
            return Rewards;
        }
    }
}