using UI.RewardArea;
using UI.SpinArea;
using UI.SpinArea.Signal;
using UnityEngine;
using Utils;
using Zone;

namespace Game
{
    public class GameStateManager : MonoBehaviour
    {
        private int _currentZoneIndex = 0;
        private int _rollResult = -1;
        private ZoneSettings _currentZoneSettings;
        private ZoneType _zoneType;
        
        private void Start()
        {
            PrepareForSpin();
        }

        private void PrepareForSpin()
        {
            // Clone because we might modify if its golden
            _currentZoneSettings = GameInfoManager.Instance.GetZoneLooped(_currentZoneIndex).Clone();
            _zoneType = GameInfoManager.Instance.GetZoneType(_currentZoneIndex);

            if (_zoneType == ZoneType.Golden)
            {
                foreach (var reward in _currentZoneSettings.Rewards)
                {
                    reward.Value *= GameInfoManager.Instance.SpecialZoneSettings.GoldenSpinRewardMultiplier;
                }
            }
            
            SpinAreaSlotManager.Instance.Init(_currentZoneSettings, _zoneType);
            SpinAreaThemeManager.Instance.LoadSpinArea(_zoneType);
            SpinAreaRollManager.Instance.ResetRoll();

            SignalBus.Instance.Subscribe<SpinClickedSignal>(StartSpin);
        }

        private void StartSpin()
        {
            SignalBus.Instance.Unsubscribe<SpinClickedSignal>(StartSpin);

            _rollResult = Random.Range(0, 8);
            SpinAreaRollManager.Instance.StartRoll(_rollResult, OnRollComplete);
        }

        private void OnRollComplete()
        {
            // if (_zoneType == ZoneType.Basic && _currentZoneSettings.BombIndex == _rollResult)
            // {
            //     // Lose
            //     ////////////////////
            //     return;
            // }
            //
            // Give rewards
            RewardAreaManager.Instance.AddReward(_currentZoneSettings.Rewards[_rollResult]);

            _currentZoneIndex++;
            PrepareForSpin();
        }
    }
}