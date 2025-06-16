using Exchange;
using UnityEngine;

namespace Zone
{
    [CreateAssetMenu(fileName = "SpecialZoneSettings", menuName = "ScriptableObjects/SpecialZoneSettings", order = 2)]
    public class GameSettings : ScriptableObject
    {
        public int SilverSpinPeriod = 5;
        public int GoldenSpinPeriod = 30;
        public int GoldenSpinRewardMultiplier = 10;
        public ExchangeData ReviveCost;
    }
}