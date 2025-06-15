using Exchange;
using Game;
using UI.Exchange;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Zone;

namespace UI.ZoneArea
{
    public class ZoneAreaItemFactory : Singleton<ZoneAreaItemFactory>
    {
        [SerializeField] private ZoneAreaItem zoneAreaItemPrefab;

        private GenericObjectPool<ZoneAreaItem> _objectPool;

        private void Awake()
        {
            _objectPool = new GenericObjectPool<ZoneAreaItem>(zoneAreaItemPrefab, 8);
        }

        public ZoneAreaItem GetZoneAreaItem(int level, Transform customParent)
        {
            ZoneAreaItem viewInstance = _objectPool.Get();
            Transform exchangeTransform;
            (exchangeTransform = viewInstance.transform).SetParent(customParent);
            exchangeTransform.localScale = Vector3.one;
            exchangeTransform.localPosition = Vector3.zero;
            exchangeTransform.localRotation = Quaternion.identity;
            viewInstance.Init(level.ToString(), GetZoneColor(level));
            return viewInstance;
        }

        public Color GetZoneColor(int level)
        {
            var zoneType = GameInfoManager.Instance.GetZoneType(level);
            switch (zoneType)
            {
                case ZoneType.Basic:
                    return Color.white;
                case ZoneType.Silver:
                    return Color.green;
                case ZoneType.Golden:
                    return Color.yellow;
            }

            return Color.white;
        }

        public void ReturnZoneAreaItem(ZoneAreaItem view)
        {
            _objectPool.Return(view);
        }
    }
}