using System.Collections;
using UnityEngine;

namespace ShoesFactory
{
    public class PanelUI<T> : Singleton<T> where T : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _panelCanvasGroup = null;
        [SerializeField] private protected bool _panelEnabledOnStart = false;
        [SerializeField] private float _animationSpeed = 1;

        private bool _panelUnderAnimation = false;

        private void Start()
        {
            Init();
        }

        private protected virtual void Init()
        {
            if (_panelEnabledOnStart)
            {
                OpenPanelInstantly();
            }
            else
            {
                ClosePanelInstantly();
            }
        }

        private protected void OpenPanelWithAnimation()
        {
            if (!_panelUnderAnimation)
            {
                _panelUnderAnimation = true;
                StartCoroutine(OpenPanelAnimation());
            }
        }

        private protected void ClosePanelWithAnimation()
        {
            if (!_panelUnderAnimation)
            {
                _panelUnderAnimation = true;
                StartCoroutine(ClosePanelAnimation());
            }
        }

        private protected void OpenPanelInstantly()
        {
            _panelCanvasGroup.alpha = 1;
            _panelCanvasGroup.interactable = true;
            _panelCanvasGroup.blocksRaycasts = true;
        }

        private protected void ClosePanelInstantly()
        {
            _panelCanvasGroup.alpha = 0;
            _panelCanvasGroup.interactable = false;
            _panelCanvasGroup.blocksRaycasts = false;
        }

        private IEnumerator OpenPanelAnimation()
        {
            _panelCanvasGroup.interactable = true;
            _panelCanvasGroup.blocksRaycasts = true;

            float animationStep = Time.fixedDeltaTime * _animationSpeed;

            while (_panelCanvasGroup.alpha < 1)
            {
                float newAlpha = Mathf.Clamp01(_panelCanvasGroup.alpha + animationStep);
                _panelCanvasGroup.alpha = newAlpha;
                yield return new WaitForFixedUpdate();
            }

            _panelUnderAnimation = false;
            yield break;
        }

        private IEnumerator ClosePanelAnimation()
        {
            _panelCanvasGroup.interactable = false;
            _panelCanvasGroup.blocksRaycasts = false;

            float animationStep = Time.fixedDeltaTime * _animationSpeed;

            while (_panelCanvasGroup.alpha > 0)
            {
                float newAlpha = Mathf.Clamp01(_panelCanvasGroup.alpha - animationStep);
                _panelCanvasGroup.alpha = newAlpha;
                yield return new WaitForFixedUpdate();
            }

            _panelUnderAnimation = false;
            yield break;
        }
    }
}
