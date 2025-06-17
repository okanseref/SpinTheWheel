using Game;
using UnityEngine;

namespace Spin
{
    public class RiggedSpinRule : ISpinRule
    {
        public int GetResult()
        {
            var currentIndex = GameStateManager.Instance.GetCurrentZoneIndex();
            var silverZonePeriod = GameInfoManager.Instance.GameSettings.SilverSpinPeriod;

            if (currentIndex % silverZonePeriod == silverZonePeriod - 1)
            {
                // 1 Zone before silver always fail :)
                return GameInfoManager.Instance.GetZoneLooped(currentIndex).BombIndex;
            }
            
            return Random.Range(0, 8);
        }
    }
}