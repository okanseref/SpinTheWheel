using System;
using System.Linq;
using Game;
using Game.Signal;
using Inventory;
using UI.Exchange;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Popup
{
    public class LosePopup : BasePopup
    {
        [SerializeField] private Transform ReviveExchangeRoot;
        [SerializeField] private Button ReviveButton;
        [SerializeField] private Button GiveUpButton;

        private ExchangeView _reviveExchangeVisual;
        
        private void Start()
        {
            GiveUpButton.onClick.AddListener(OnExitClicked);
            ReviveButton.onClick.AddListener(OnReviveClicked);

            _reviveExchangeVisual = ExchangeViewFactory.Instance.CreateExchangeView(GameInfoManager.Instance.GameSettings.ReviveCost, ReviveExchangeRoot);
            
            ReviveButton.interactable =
                InventoryManager.Instance.HasExchangeData(GameInfoManager.Instance.GameSettings.ReviveCost);
        }

        private void OnReviveClicked()
        {
            if (InventoryManager.Instance.HasExchangeData(GameInfoManager.Instance.GameSettings.ReviveCost))
            {
                InventoryManager.Instance.RemoveExchangeData(GameInfoManager.Instance.GameSettings.ReviveCost);
                SignalBus.Instance.Fire<RevivedSignal>();
                Hide();
            }
        }

        private void OnExitClicked()
        {
            SignalBus.Instance.Fire<GameResetSignal>();
            Hide();
        }

        private void OnDestroy()
        {
            ExchangeViewFactory.Instance.ReturnExchangeView(_reviveExchangeVisual);
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