using System.Collections.Generic;
using Exchange;
using Inventory.Signal;
using Utils;

namespace Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        private Dictionary<ExchangeType, Dictionary<int, int>> _inventoryDictionary = new();

        public void AddExchangeDatas(List<ExchangeData> exchangeDatas)
        {
            foreach (var exchangeData in exchangeDatas)
            {
                AddExchangeData(exchangeData);
            }
        }
        
        public void AddExchangeData(ExchangeData exchangeData)
        {
            if (_inventoryDictionary.TryGetValue(exchangeData.Type, out var subTypeDictionary))
            {
                if (subTypeDictionary.ContainsKey(exchangeData.Subtype))
                {
                    subTypeDictionary[exchangeData.Subtype] += exchangeData.Value;
                    SignalBus.Instance.Fire(new InventoryUpdatedSignal(exchangeData.Type, exchangeData.Subtype, subTypeDictionary[exchangeData.Subtype]));
                    return;
                }

                subTypeDictionary[exchangeData.Subtype] = exchangeData.Value;
            }
            else
            {
                _inventoryDictionary[exchangeData.Type] = new Dictionary<int, int>
                {
                    [exchangeData.Subtype] = exchangeData.Value
                };
            }
            
            SignalBus.Instance.Fire(new InventoryUpdatedSignal(exchangeData.Type, exchangeData.Subtype, exchangeData.Value));
        }

        public bool HasExchangeData(ExchangeData exchangeData)
        {
            if (_inventoryDictionary.TryGetValue(exchangeData.Type, out var subTypeDictionary))
            {
                if (subTypeDictionary.ContainsKey(exchangeData.Subtype))
                {
                    return subTypeDictionary[exchangeData.Subtype] >= exchangeData.Value;
                }
            }

            return false;
        }

        public int GetExchangeDataValue(ExchangeType type, int subType)
        {
            if (_inventoryDictionary.TryGetValue(type, out var subTypeDictionary))
            {
                if (subTypeDictionary.TryGetValue(subType, out var value))
                {
                    return value;
                }
            }

            return 0;
        }

        public bool RemoveExchangeData(ExchangeData exchangeData)
        {
            if (_inventoryDictionary.TryGetValue(exchangeData.Type, out var subTypeDictionary))
            {
                if (subTypeDictionary.ContainsKey(exchangeData.Subtype))
                {
                    if (subTypeDictionary[exchangeData.Subtype] >= exchangeData.Value)
                    {
                        subTypeDictionary[exchangeData.Subtype] -= exchangeData.Value;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}