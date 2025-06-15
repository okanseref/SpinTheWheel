using System.Linq;
using Game.Signal;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Popup
{
    public class LosePopup : BasePopup
    {
        [SerializeField] private Button ReviveButton;
        [SerializeField] private Button ExitButton;

        private void Start()
        {
            ExitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnExitClicked()
        {
            SignalBus.Instance.Fire<GameResetSignal>();
            Hide();
        }
        
        private void OnValidate()
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if(prefabStage == null)
                return;
            
            var buttons = prefabStage.FindComponentsOfType<Button>();
            
            if (buttons == null || buttons.Length <= 0) return;
            var buttonsList = buttons.ToList();
            ReviveButton = buttonsList.Find((x)=> x.name.Equals("ui_button_lose_popup_revive"));
            ExitButton = buttonsList.Find((x)=> x.name.Equals("ui_button_lose_popup_exit"));
        }
    }
}