using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zone;

public class SpinAreaThemeManager : Singleton<SpinAreaThemeManager>
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

    public void LoadSpinArea(ZoneType zoneType)
    {
        //
        switch (zoneType)
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
