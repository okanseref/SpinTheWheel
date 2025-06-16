using Game.Signal;
using UI.Popup;
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
        private int _currentZoneIndex = GameConstants.FirstLevelIndex;
        private int _rollResult = -1;
        private ZoneSettings _currentZoneSettings;
        private ZoneType _zoneType;
        
        private void Start()
        {
            PrepareForSpin();
            
            SignalBus.Instance.Subscribe<GameResetSignal>(ResetGame);
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
                    reward.Value *= GameInfoManager.Instance.GameSettings.GoldenSpinRewardMultiplier;
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

            SignalBus.Instance.Subscribe<RollCompletedSignal>(OnRollComplete);

            _rollResult = Random.Range(0, 8);
            SpinAreaRollManager.Instance.StartRoll(_rollResult);
        }

        private void OnRollComplete()
        {
            SignalBus.Instance.Unsubscribe<RollCompletedSignal>(OnRollComplete);

            if (_zoneType == ZoneType.Basic && _currentZoneSettings.BombIndex == _rollResult)
            {
                // Lose
                SignalBus.Instance.Subscribe<RevivedSignal>(OnRevived);
                PopupManager.Instance.Show<LosePopup>();
                return;
            }
            
            // Give rewards
            var reward = _currentZoneSettings.Rewards[_rollResult];
            SignalBus.Instance.Subscribe<RewardGivenSignal>(OnRewardGiven);
            PopupManager.Instance.Show<RewardPopup, RewardPopupData>(new RewardPopupData(reward));
        }

        private void OnRewardGiven(RewardGivenSignal rewardGivenSignal)
        {
            RewardAreaManager.Instance.AddReward(rewardGivenSignal.Reward);
            _currentZoneIndex++;
            PrepareForSpin();
            
            SignalBus.Instance.Unsubscribe<RewardGivenSignal>(OnRewardGiven);
        }

        private void OnRevived()
        {
            SignalBus.Instance.Unsubscribe<RevivedSignal>(OnRevived);
            _currentZoneIndex++;
            PrepareForSpin();
        }

        public void ResetGame(GameResetSignal signal)
        {
            _currentZoneIndex = GameConstants.FirstLevelIndex;
            RewardAreaManager.Instance.ResetArea(signal.IsRewardsClaimed);
            PrepareForSpin();
        }
    }
}