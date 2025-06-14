using Exchange;
using UnityEngine;
using Utils;

namespace UI.Exchange
{
    public class ExchangeViewFactory : Singleton<ExchangeViewFactory>
    {
        [SerializeField] private ExchangeView exchangeViewPrefab;

        private GenericObjectPool<ExchangeView> _objectPool;

        private void Start()
        {
            _objectPool = new GenericObjectPool<ExchangeView>(exchangeViewPrefab, 8);
        }

        public ExchangeView CreateExchangeView(ExchangeData exchangeData, Transform customParent)
        {
            ExchangeView viewInstance = _objectPool.Get();
            viewInstance.transform.SetParent(customParent);
            viewInstance.Init(ExchangeVisualManager.Instance.GetExchangeSprite(exchangeData), exchangeData.Value);
            return viewInstance;
        }

        public void ReturnExchangeView(ExchangeView view)
        {
            _objectPool.Return(view);
        }
    }
}