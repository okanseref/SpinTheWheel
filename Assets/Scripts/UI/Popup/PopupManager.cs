using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace UI.Popup
{
    public class PopupManager : Singleton<PopupManager>
    {
        [SerializeField] private Transform _popupRoot;

        List<BasePopup> activePopups = new();

        public T GetActivePopup<T>() where T : class
        {
            foreach (var item in activePopups)
            {
                if (item is T item1)
                {
                    return item1;
                }
            }

            return default;
        }

        public List<BasePopup> GetActivePopups()
        {
            return activePopups;
        }
        
        public void Show<T>()
        {
            var basePopup = TryShowInternal<T>();
            
            if(basePopup == null)
                return;
            
            basePopup.Show();
            activePopups.Add(basePopup);
        }
        
        public void Show<T, T1>(T1 popupData)
        {
            var basePopup = TryShowInternal<T>();

            if(basePopup == null)
                return;
            
            basePopup.Init(popupData);
            activePopups.Add(basePopup);
        }

        public void Hide<T>()
        {
            foreach (var item in activePopups)
            {
                if (item is T)
                {
                    HideInternal(item);
                    break;
                }
            }
        }

        public void Hide(BasePopup basePopup)
        {
            foreach (var item in activePopups)
            {
                if (item == basePopup)
                {
                    HideInternal(item);
                    break;
                }
            }
        }

        private void HideInternal(BasePopup item)
        {
            activePopups.Remove(item);
            item.Hide();
        }

        private BasePopup TryShowInternal<T>()
        {
            var prefab = GetPopup<T>();

            if (prefab == null)
            {
                Debug.LogError("Popup prefab not found: " + typeof(T).Name);
                return null;
            }

            if (IsActive<T>())
            {
                Debug.LogWarning("Popup already active: " + typeof(T).Name);
                return null;
            }

            var popup = Instantiate(prefab, _popupRoot.transform);
            
            popup.TryGetComponent<BasePopup>(out var basePopup);

            if (basePopup == null)
            {
                Debug.Log("BasePopup Component missing!");
                return null;
            }

            basePopup.Show();
            return basePopup;
        }

        private bool IsActive<T>()
        {
            return activePopups.Find((x)=> x.GetType().Name == typeof(T).Name);
        }

        public GameObject GetPopup<T>()
        {
            var popup = Resources.Load<GameObject>("Popups/" + typeof(T).Name);
            return popup;
        }

        private void OnValidate()
        {
            _popupRoot = GameObject.Find("ui_transform_popups_root").transform;
        }
    }
}
