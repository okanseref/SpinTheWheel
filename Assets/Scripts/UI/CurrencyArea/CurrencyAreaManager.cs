using System.Collections;
using System.Collections.Generic;
using Exchange;
using Inventory;
using Inventory.Signal;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

public class CurrencyAreaManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currency1Text;
    [SerializeField] private TextMeshProUGUI currency2Text;
    void Start()
    {
        InitialRefresh();
        SignalBus.Instance.Subscribe<InventoryUpdatedSignal>(RefreshCurrencies);
    }

    private void InitialRefresh()
    {
        currency1Text.text = NumberFormatter.FormatCompact(InventoryManager.Instance.GetExchangeDataValue(ExchangeType.Currency, 1));
        currency2Text.text = NumberFormatter.FormatCompact(InventoryManager.Instance.GetExchangeDataValue(ExchangeType.Currency, 2));
    }

    private void RefreshCurrencies(InventoryUpdatedSignal signal)
    {
        if (signal.Type == ExchangeType.Currency)
        {
            if (signal.SubType == 1)
            {
                currency1Text.text = NumberFormatter.FormatCompact(signal.Value);
            }else if (signal.SubType == 2)
            {
                currency2Text.text = NumberFormatter.FormatCompact(signal.Value);
            }
        }
    }
}
