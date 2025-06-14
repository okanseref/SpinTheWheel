using UI.SpinArea;
using UI.SpinArea.Signal;
using UnityEngine;
using Utils;
using Zone;

namespace Game
{
    public class GameStateManager : MonoBehaviour
    {
        private int _currentZone = 0;
        private int _rollResult = -1;
        private ZoneSettings _currentZoneSettings;
        private void Start()
        {
            PrepareForSpin();
        }

        private void PrepareForSpin()
        {
            var currentZone = GameInfoManager.Instance.GetZoneLooped(_currentZone);
            
            SpinAreaSlotManager.Instance.Init(currentZone);
            SpinAreaThemeManager.Instance.LoadSpinArea();
            
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
            // Give rewards
            PrepareForSpin();
        }
    }
}