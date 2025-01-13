using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202412.Battle.Result
{
    public class BattleResultView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Button _nextButton;

        [SerializeField]
        private GameObject _win;

        [SerializeField]
        private GameObject _lose;

        [SerializeField]
        private CanvasGroup _buttonCanvasGroup;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            _win.SetActive(false);
            _lose.SetActive(false);

            _buttonCanvasGroup.alpha = 0;
            _buttonCanvasGroup.interactable = false;
            _buttonCanvasGroup.blocksRaycasts = false;
        }

        public async UniTask ShowAsync(bool isWin, CancellationToken cancellationToken = default)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: cancellationToken);

            SetResult(isWin);

            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: cancellationToken);

            _buttonCanvasGroup.alpha = 1f;
            _buttonCanvasGroup.interactable = true;
            _buttonCanvasGroup.blocksRaycasts = true;
        }

        private void SetResult(bool isWin)
        {
            if (isWin)
            {
                _win.SetActive(true);
                _lose.SetActive(false);
            }
            else
            {
                _win.SetActive(false);
                _lose.SetActive(true);
            }
        }

        public async UniTask HideAsync(CancellationToken cancellationToken = default)
        {
            await LMotion.Create(1f, 0f, 1f)
                .BindToAlpha(_canvasGroup)
                .AddTo(this)
                .ToUniTask(cancellationToken: cancellationToken);
        }

        public Observable<Unit> OnNextAsObservable() => _nextButton.OnClickAsObservable();
    }
}