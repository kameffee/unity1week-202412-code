using System;
using LitMotion;
using LitMotion.Extensions;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202412.Battle.Throw
{
    public class ThrowPhaseView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Button _throwButton;

        [SerializeField]
        private Button _throwAndTakeDiceButton;

        [SerializeField]
        private CanvasGroup _buttonCanvasGroup;

        private MotionHandle _motionHandle;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;

            if (_motionHandle.IsActive())
                _motionHandle.Cancel();
        }

        public void VisibleThrowAndTakeDiceButton(bool visible)
        {
            _throwAndTakeDiceButton.gameObject.SetActive(visible);
        }

        public Observable<Unit> OnClickThrowAsObservable() => _throwButton.OnClickAsObservable();
        public Observable<Unit> OnClickThrowAndTakeDiceAsObservable() => _throwAndTakeDiceButton.OnClickAsObservable();
    }
}