using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using TMPro;
using Unity1week.Audio;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Unity1week202412.Result
{
    public class GameResultView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private GameResultListView _gameResultListView;

        [SerializeField]
        private TextMeshProUGUI _thankyouText;

        [SerializeField]
        private Button _returnButton;

        private AudioPlayer _audioPlayer;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _thankyouText.gameObject.SetActive(false);
            _returnButton.gameObject.SetActive(false);
            LifetimeScope.Find<LifetimeScope>().Container.Inject(this);
        }

        [Inject]
        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void Set(GameResultListView.ViewModel viewModel)
        {
            _gameResultListView.Set(viewModel);
        }

        public async UniTask ShowAsync(CancellationToken cancellationToken = default)
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            await LMotion.Create(0f, 1f, 1f)
                .WithEase(Ease.Linear)
                .BindToAlpha(_canvasGroup)
                .AddTo(this)
                .ToUniTask(cancellationToken);

            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: cancellationToken);

            await _gameResultListView.ShowPerformAsync(cancellationToken);

            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: cancellationToken);

            _thankyouText.gameObject.SetActive(true);
            _returnButton.gameObject.SetActive(true);
            _audioPlayer.PlaySe("Result/Thankyou");
        }

        public UniTask HideAsync(CancellationToken cancellationToken = default)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            return UniTask.CompletedTask;
        }

        public Observable<Unit> OnReturnButtonClickAsObservable() => _returnButton.OnClickAsObservable();
    }
}