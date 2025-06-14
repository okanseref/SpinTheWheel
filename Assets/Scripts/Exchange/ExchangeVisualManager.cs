using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Exchange
{
    public class ExchangeVisualManager : Singleton<ExchangeVisualManager>
    {
        private Dictionary<ExchangeType, Dictionary<int, Sprite>> _exchangeVisualDictionary = new();

        void Awake()
        {
            var allExchangeVisuals = Resources.LoadAll<Sprite>("ExchangeVisuals");
            
            foreach (var sprite in allExchangeVisuals)
            {
                var splittedName = sprite.name.Split('_');
                var exchangeType = (ExchangeType)Enum.Parse(typeof(ExchangeType), splittedName[1]);
                var exchangeSubtype = int.Parse(splittedName[2]);

                if (_exchangeVisualDictionary.TryGetValue(exchangeType, out var value))
                {
                    value.TryAdd(exchangeSubtype, sprite);
                }
                else
                {
                    _exchangeVisualDictionary[exchangeType] = new() { {exchangeSubtype, sprite} };
                }
            }
        }

        public Sprite GetExchangeSprite(ExchangeData exchangeData)
        {
            if (_exchangeVisualDictionary.TryGetValue(exchangeData.Type, out var value))
            {
                if (value.TryGetValue(exchangeData.Subtype, out var sprite))
                {
                    return sprite;
                }

                return null;
            }
            
            return null;
        }
    }
}
