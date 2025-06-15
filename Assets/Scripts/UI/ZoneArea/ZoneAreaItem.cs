using TMPro;
using UI.Popup;
using UnityEngine;

namespace UI.ZoneArea
{
    public class ZoneAreaItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI zoneText;
        
        public void Init(string text, Color color)
        {
            zoneText.text = text;
            zoneText.color = color;
        }

        public void ChangeColor(Color color)
        {
            zoneText.color = color;
        }
    }
}
