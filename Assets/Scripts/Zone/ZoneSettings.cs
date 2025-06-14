using System;
using System.Collections.Generic;
using Exchange;

namespace Zone
{
    [Serializable]
    public class ZoneSettings
    {
        public int BombIndex;
        public List<ExchangeData> Rewards = new();

        public ZoneSettings Clone()
        {
            var clonedZoneSettings = new ZoneSettings();
            clonedZoneSettings.BombIndex = BombIndex;

            foreach (var reward in Rewards)
            {
                clonedZoneSettings.Rewards.Add(reward.Clone());
            }

            return clonedZoneSettings;
        }
    }
}
