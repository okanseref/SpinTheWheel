using System.Collections.Generic;
using Exchange;
using UnityEngine;

namespace Spin
{
    [CreateAssetMenu(fileName = "SpecialZoneSettings", menuName = "ScriptableObjects/SpecialZoneSettings", order = 2)]
    public class SpecialZoneSettings : ScriptableObject
    {
        public int SilverSpinPeriod = 5;
        public int GoldenSpinPeriod = 30;
        public ExchangeData SafeSpinFillerReward;
    }
}