using System.Collections;
using System.Collections.Generic;
using UI.Exchange;
using UnityEngine;
using Utils;
using Zone;

public class SpinAreaSlotManager : Singleton<SpinAreaSlotManager>
{
    [SerializeField] public List<Transform> SpinSlotRoots;
    [SerializeField] public GameObject DeathSlotView;

    private List<ExchangeView> _usedViews = new();
    
    public void Init(ZoneSettings zoneSettings)
    {
        ClearArea();
        
        for (int i = 0; i < zoneSettings.Rewards.Count; i++)
        {
            if (i == zoneSettings.BombIndex)
            {
                // Put bomb
                DeathSlotView.transform.SetParent(SpinSlotRoots[i]);
                DeathSlotView.gameObject.SetActive(true);
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
}
