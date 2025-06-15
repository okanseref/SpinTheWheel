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
        [SerializeField] private Button GiveUpButton;

        private void Start()
        {
            GiveUpButton.onClick.AddListener(OnExitClicked);
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
            
            var buttons = GetComponentsInChildren<Button>(true);
            
            if (buttons == null || buttons.Length <= 0) return;
            var buttonsList = buttons.ToList();
            var reviveButtonReference = buttonsList.Find((x) => x.name.Equals("ui_button_lose_popup_revive"));
            var giveUpButtonReference = buttonsList.Find((x) => x.name.Equals("ui_button_lose_popup_give_up"));
            ReviveButton = reviveButtonReference != null ? reviveButtonReference : ReviveButton;
            GiveUpButton = giveUpButtonReference != null ? giveUpButtonReference : GiveUpButton;
        }
    }
}