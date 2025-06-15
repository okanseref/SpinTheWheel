using Exchange;
using UnityEngine;
using Utils;

namespace UI.Exchange
{
    public class ExchangeViewFactory : Singleton<ExchangeViewFactory>
    {
        [SerializeField] private ExchangeView exchangeViewPrefab;

        private GenericObjectPool<ExchangeView> _objectPool;

        private void Awake()
        {
            _objectPool = new GenericObjectPool<ExchangeView>(exchangeViewPrefab, 8);
        }

        public ExchangeView CreateExchangeView(ExchangeData exchangeData, Transform customParent)
        {
            ExchangeView viewInstance = _objectPool.Get();
            Transform exchangeTransform;
            (exchangeTransform = viewInstance.transform).SetParent(customParent);
            exchangeTransform.localScale = Vector3.one;
            exchangeTransform.localPosition = Vector3.zero;
            exchangeTransform.localRotation = Quaternion.identity;
            viewInstance.Init(ExchangeVisualManager.Instance.GetExchangeSprite(exchangeData), exchangeData.Value);
            return viewInstance;
        }

        public void ReturnExchangeView(ExchangeView view)
        {
            _objectPool.Return(view);
        }
    }
}