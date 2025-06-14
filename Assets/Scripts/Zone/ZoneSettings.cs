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
    }
}
