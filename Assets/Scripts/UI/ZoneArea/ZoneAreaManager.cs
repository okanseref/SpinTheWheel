using System.Collections.Generic;
using Game;
using Game.Signal;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.ZoneArea
{
    public class ZoneAreaManager : MonoBehaviour
    {
        [SerializeField] private int maxElementCount = 13;
        [SerializeField] private GridLayoutGroup zoneGridLayout;

        private int _centerLevel = -1;
        private LinkedListNode<ZoneAreaItem> _centerZoneAreaItem;
        private LinkedList<ZoneAreaItem> _zoneAreaItems = new();
        private int _lastCreatedLevel = GameConstants.FirstLevelIndex;
        
        private void Start()
        {
            // Must be odd
            if (maxElementCount % 2 == 0)
                maxElementCount++;
            
            var width = ((RectTransform)zoneGridLayout.transform).sizeDelta.x;
            var cellSize = width / maxElementCount;
            zoneGridLayout.cellSize = new Vector2(cellSize, cellSize);
            
            Initialize();
            
            SignalBus.Instance.Subscribe<RewardGivenSignal>(OnLevelCompleted);
            SignalBus.Instance.Subscribe<GameResetSignal>(Reset);
        }

        private void Initialize()
        {
            var initialCount = Mathf.CeilToInt(maxElementCount / 2f);
            
            for (int i = 0; i < initialCount; i++)
            {
                CreateZoneItemInternal();
            }

            _centerLevel = GameConstants.FirstLevelIndex;
            _centerZoneAreaItem = _zoneAreaItems.First;
            _centerZoneAreaItem.Value.ChangeColor(Color.black);
        }

        private void CreateZoneItemInternal()
        {
            var zoneAreaItem = ZoneAreaItemFactory.Instance.GetZoneAreaItem(_lastCreatedLevel, zoneGridLayout.transform);
            _zoneAreaItems.AddLast(zoneAreaItem);
            _lastCreatedLevel++;
        }

        private void OnLevelCompleted()
        {
            if (_zoneAreaItems.Count == maxElementCount)
            {
                // Return to pool left-most one
                ZoneAreaItemFactory.Instance.ReturnZoneAreaItem(_zoneAreaItems.First.Value);
                _zoneAreaItems.RemoveFirst();
            }

            _centerZoneAreaItem.Value.ChangeColor(ZoneAreaItemFactory.Instance.GetZoneColor(_centerLevel));
            _centerZoneAreaItem = _centerZoneAreaItem.Next;
            if (_centerZoneAreaItem != null) 
                _centerZoneAreaItem.Value.ChangeColor(Color.black);
            _centerLevel++;
            
            CreateZoneItemInternal();
        }

        private void Reset()
        {
            while (_zoneAreaItems.Count > 0)
            {
                ZoneAreaItemFactory.Instance.ReturnZoneAreaItem(_zoneAreaItems.First.Value);
                _zoneAreaItems.RemoveFirst();            }

            _lastCreatedLevel = GameConstants.FirstLevelIndex;
            
            Initialize();
        }
    }
}