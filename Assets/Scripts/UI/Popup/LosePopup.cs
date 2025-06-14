using System;
using System.Linq;
using DG.Tweening;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class LosePopup : MonoBehaviour
    {
        [SerializeField] private Button ReviveButton;
        [SerializeField] private Button ExitButton;

        private void Start()
        {

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