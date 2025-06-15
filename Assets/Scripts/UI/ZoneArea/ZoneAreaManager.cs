using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.ZoneArea
{
    public class ZoneAreaManager : Singleton<ZoneAreaManager>
    {
        [SerializeField] private int maxElementCount = 13;
        [SerializeField] private GridLayoutGroup zoneGridLayout;

        private List<ZoneAreaItem> _zoneAreaItems = new();
        private int _lastCreatedLevel = 1;
        private void Start()
        {
            // Must be odd
            if (maxElementCount % 2 == 0)
                maxElementCount++;
            
            var width = ((RectTransform)zoneGridLayout.transform).sizeDelta.x;
            var cellSize = width / maxElementCount;
            zoneGridLayout.cellSize = new Vector2(cellSize, cellSize);

            Initialize();
        }

        private void Initialize()
        {
            var initialCount = Mathf.CeilToInt(maxElementCount / 2f);
            
            for (int i = 0; i < initialCount; i++)
            {
                var zoneAreaItem = ZoneAreaItemFactory.Instance.GetZoneAreaItem(_lastCreatedLevel, zoneGridLayout.transform);
                _zoneAreaItems.Add(zoneAreaItem);
                _lastCreatedLevel++;
            }
        }
    }
}