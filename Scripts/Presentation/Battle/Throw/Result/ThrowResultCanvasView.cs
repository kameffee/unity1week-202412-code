using System;
using System.Collections.Generic;
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

namespace Unity1week202412.Battle.Throw.Result
{
    public class ThrowResultCanvasView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private TextMeshProUGUI _resultText;

        [SerializeField]
        private DiceFaceIconListView _diceFaceIconListView;

        [SerializeField]
        private Button _nextButton;

        [SerializeField]
        private GameObject _clickAnnotation;

        private AudioPlayer _audioPlayer;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            _resultText.gameObject.SetActive(false);
            _clickAnnotation.SetActive(false);
        }

        [Inject]
        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void Initialize(string message, IReadOnlyList<DiceFaceIconType> resultDiceFaceTypes)
        {
            _resultText.text = message;
            _diceFaceIconListView.SetFaces(resultDiceFaceTypes);
        }

        public async UniTask ShowAsync(bool visibleClickAnnotation, CancellationToken cancellationToken = default)
        {
            _resultText.gameObject.SetActive(false);
            _clickAnnotation.SetActive(false);

            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);

            await _diceFaceIconListView.ShowPerform(cancellationToken);

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: cancellationToken);

            _resultText.gameObject.SetActive(true);
            _audioPlayer.PlaySe("Battle/Result/ScoreType");
            
            _clickAnnotation.SetActive(visibleClickAnnotation);
        }

        public async UniTask HideAsync(CancellationToken cancellationToken = default)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            await LMotion.Create(1f, 0f, 0.5f)
                .BindToAlpha(_canvasGroup)
                .AddTo(this)
                .ToUniTask(cancellationToken);
        }

        public Observable<Unit> OnClickAsObservable() => _nextButton.OnClickAsObservable();
    }
}