using DG.Tweening;
using UnityEngine;

namespace UI.Popup
{
    public class BasePopup : MonoBehaviour
    {
        protected CanvasGroup _canvasGroup;
        private const Ease Ease = DG.Tweening.Ease.Linear;
        private const float Duration = 0.1f;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Init<T>(T data)
        {

        }

        public virtual void Show()
        {
            // Show anim
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 0.5f;
                DOTween.To(()=> _canvasGroup.alpha, x=> _canvasGroup.alpha = x, 1f, Duration).SetEase(Ease);
            }
        }

        public virtual void Hide()
        {
            // Hide anim
            
            if (_canvasGroup != null)
            {
                _canvasGroup.alpha = 1f;
                DOTween.To(()=> _canvasGroup.alpha, x=> _canvasGroup.alpha = x, 0.5f, Duration).SetEase(Ease).OnComplete(
                    () =>
                    {
                        Destroy(this.gameObject);
                    });
            }
        }

        public bool CheckData<T, T1>()
        {
            if(typeof(T) != typeof(T1))
            {
                Debug.Log("Invalid popupData:" + typeof(T) + " and " + typeof(T1));
                return false;
            }

            return true;
        }
    }

    public class BasePopupData
    {
        public BasePopupData()
        {
            //
        }
    }
}