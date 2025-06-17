using System.Collections.Generic;
using Game.Signal;
using UI.Exchange;
using UI.SpinArea.Signal;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zone;

namespace UI.SpinArea
{
    public class SpinAreaSlotManager : MonoBehaviour
    {
        [SerializeField] public List<Transform> SpinSlotRoots;
        [SerializeField] public GameObject DeathSlotView;
        [SerializeField] public Button SpinButton;

        private List<ExchangeView> _usedViews = new();

        private void Awake()
        {
            DeathSlotView.gameObject.SetActive(false);
            
            SpinButton.onClick.AddListener(OnSpinClicked);
        
            SignalBus.Instance.Subscribe<PrepareSpinSignal>(OnPrepareSpin);
        }

        public void OnPrepareSpin(PrepareSpinSignal signal)
        {
            ClearArea();
            SpinButton.gameObject.SetActive(true);

            for (int i = 0; i < signal.ZoneSettings.Rewards.Count; i++)
            {
                if (i == signal.ZoneSettings.BombIndex && signal.ZoneType == ZoneType.Basic)
                {
                    // Put bomb
                    DeathSlotView.transform.SetParent(SpinSlotRoots[i]);
                    DeathSlotView.gameObject.SetActive(true);
                    DeathSlotView.transform.localPosition = Vector3.zero;
                    DeathSlotView.transform.localRotation = Quaternion.identity;
                    continue;
                }

                var exchangeView = ExchangeViewFactory.Instance.CreateExchangeView(signal.ZoneSettings.Rewards[i], SpinSlotRoots[i]);
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

        private void OnSpinClicked()
        {
            SpinButton.gameObject.SetActive(false);
            SignalBus.Instance.Fire(new SpinClickedSignal());
        }

        private void OnValidate()
        {
            SpinButton = GameObject.Find("ui_button_spin").GetComponent<Button>();
        }
    }
}
