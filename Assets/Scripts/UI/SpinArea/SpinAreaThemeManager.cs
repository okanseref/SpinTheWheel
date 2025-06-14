using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class SpinAreaThemeManager : Singleton<SpinAreaThemeManager>
{
    [SerializeField] private Image SpinAreaCursorImage;
    [SerializeField] private Image SpinAreaBaseImage;
    [SerializeField] private TextMeshProUGUI SpinAreaTitleText;
    [SerializeField] private TextMeshProUGUI SpinAreaBonusText;

    public void LoadSpinArea()
    {
        //
    }
}
