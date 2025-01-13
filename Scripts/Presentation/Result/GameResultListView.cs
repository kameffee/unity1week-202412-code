using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity1week.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202412.Result
{
    public class GameResultListView : MonoBehaviour
    {
        [SerializeField]
        private GameResultListElementView _gameResultListElementViewPrefab;

        [SerializeField]
        private Transform _content;

        private readonly List<GameResultListElementView> _elements = new();
        private AudioPlayer _audioPlayer;
        
        private void Awake()
        {
            LifetimeScope.Find<LifetimeScope>().Container.Inject(this);
        }

        [Inject]
        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void Set(ViewModel viewModel)
        {
            foreach (var element in _elements)
            {
                Destroy(element.gameObject);
            }

            _elements.Clear();

            foreach (var resultData in viewModel.ResultDataList)
            {
                var gameResultElementView = Instantiate(_gameResultListElementViewPrefab, _content);
                _elements.Add(gameResultElementView);
                gameResultElementView.Set(resultData.StageId, resultData.IsWin);
            }
        }

        public async UniTask ShowPerformAsync(CancellationToken cancellationToken = default)
        {
            foreach (var element in _elements)
            {
                element.Show();
                _audioPlayer.PlaySe("Result/Element");
                await UniTask.Delay(TimeSpan.FromSeconds(0.8f), cancellationToken: cancellationToken);
            }
        }

        public class ViewModel
        {
            public class ResultData
            {
                public int StageId { get; }
                public bool IsWin { get; }

                public ResultData(int stageId, bool isWin)
                {
                    StageId = stageId;
                    IsWin = isWin;
                }
            }

            public IReadOnlyList<ResultData> ResultDataList { get; }

            public ViewModel(IReadOnlyList<ResultData> resultDataList)
            {
                ResultDataList = resultDataList;
            }
        }
    }
}