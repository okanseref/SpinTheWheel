using System.Collections.Generic;
using Exchange;
using Utils;

namespace Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        private Dictionary<ExchangeType, Dictionary<int, int>> _inventoryDictionary = new();
        
        public void AddExchangeData(ExchangeData exchangeData)
        {
            if (_inventoryDictionary.TryGetValue(exchangeData.Type, out var subTypeDictionary))
            {
                if (subTypeDictionary.ContainsKey(exchangeData.Subtype))
                {
                    subTypeDictionary[exchangeData.Subtype] += exchangeData.Value;
                }
                else
                {
                    subTypeDictionary[exchangeData.Subtype] = exchangeData.Value;
                }
            }
            else
            {
                _inventoryDictionary[exchangeData.Type] = new Dictionary<int, int>
                {
                    [exchangeData.Subtype] = exchangeData.Value
                };
            }
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