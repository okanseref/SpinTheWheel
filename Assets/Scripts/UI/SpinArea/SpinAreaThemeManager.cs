using Game.Signal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zone;

namespace UI.SpinArea
{
    public class SpinAreaThemeManager : MonoBehaviour
    {
        [SerializeField] private Image SpinAreaCursorImage;
        [SerializeField] private Image SpinAreaBaseImage;
        [SerializeField] private TextMeshProUGUI SpinAreaTitleText;
        [SerializeField] private TextMeshProUGUI SpinAreaBonusText;
        [SerializeField] private Sprite SpinBronzeBase;
        [SerializeField] private Sprite SpinBronzeCursor;
        [SerializeField] private Sprite SpinSilverBase;
        [SerializeField] private Sprite SpinSilverCursor;
        [SerializeField] private Sprite SpinGoldenBase;
        [SerializeField] private Sprite SpinGoldenCursor;

        private void Start()
        {
            SignalBus.Instance.Subscribe<PrepareSpinSignal>(LoadSpinArea);
        }

        public void LoadSpinArea(PrepareSpinSignal signal)
        {
            switch (signal.ZoneType)
            {
                case ZoneType.Basic:
                    SpinAreaCursorImage.sprite = SpinBronzeCursor;
                    SpinAreaBaseImage.sprite = SpinBronzeBase;
                    SpinAreaTitleText.text = "SPIN";
                    SpinAreaBonusText.text = "";
                    break;
                case ZoneType.Silver:
                    SpinAreaCursorImage.sprite = SpinSilverCursor;
                    SpinAreaBaseImage.sprite = SpinSilverBase;
                    SpinAreaTitleText.text = "SILVER SPIN";
                    SpinAreaBonusText.text = "Safe Spin!";
                    SpinAreaBonusText.color = Color.green;
                    break;
                case ZoneType.Golden:
                    SpinAreaCursorImage.sprite = SpinGoldenCursor;
                    SpinAreaBaseImage.sprite = SpinGoldenBase;
                    SpinAreaTitleText.text = "GOLDEN SPIN";
                    SpinAreaBonusText.text = "Up To x10 Rewards!";
                    SpinAreaBonusText.color = Color.yellow;
                    break;
            }
        }
    }
}
