using System;
using System.Collections.Generic;
using Exchange;
using UnityEngine.Serialization;

namespace Spin
{
    [Serializable]
    public class ZoneSettings
    {
        public List<ExchangeData> Rewards = new();
    }
}
