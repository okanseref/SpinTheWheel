using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Exchange
{
    public class ExchangeView : MonoBehaviour
    {
        [SerializeField] private Image ExchangeVisualImage;
        [SerializeField] private TextMeshProUGUI ValueText;

        public void Init(Sprite sprite, int value)
        {
            ExchangeVisualImage.sprite = sprite;
            ValueText.text = "x" + NumberFormatter.FormatCompact(value);
        }
    }
}
