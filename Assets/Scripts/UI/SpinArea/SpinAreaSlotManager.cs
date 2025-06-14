using System;
using System.Collections;
using System.Collections.Generic;
using UI.Exchange;
using UI.SpinArea.Signal;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zone;

public class SpinAreaSlotManager : Singleton<SpinAreaSlotManager>
{
    [SerializeField] public List<Transform> SpinSlotRoots;
    [SerializeField] public GameObject DeathSlotView;
    [SerializeField] public Button SpinButton;

    private List<ExchangeView> _usedViews = new();

    private void Awake()
    {
        DeathSlotView.gameObject.SetActive(false);
    }

    private void Start()
    {
        SpinButton.onClick.AddListener(OnSpinClicked);
    }

    public void Init(ZoneSettings zoneSettings, ZoneType zoneType)
    {
        ClearArea();
        
        for (int i = 0; i < zoneSettings.Rewards.Count; i++)
        {
            if (i == zoneSettings.BombIndex && zoneType == ZoneType.Basic)
            {
                // Put bomb
                DeathSlotView.transform.SetParent(SpinSlotRoots[i]);
                DeathSlotView.gameObject.SetActive(true);
                continue;
            }

            var exchangeView = ExchangeViewFactory.Instance.CreateExchangeView(zoneSettings.Rewards[i], SpinSlotRoots[i]);
            _usedViews.Add(exchangeView);
        }
    }

    public void ClearArea()
    {
        DeathSlotView.gameObject.SetActive(false);

        foreach (var exchangeView in _usedViews)
        {
            ExchangeViewFactory.Instance.ReturnExchangeView(exchangeView);
        }
        
        _usedViews.Clear();
    }

    private void OnSpinClicked()
    {
        SignalBus.Instance.Fire(new SpinClickedSignal());
    }

    private void OnValidate()
    {
        SpinButton = GameObject.Find("ui_button_spin").GetComponent<Button>();
    }
}
