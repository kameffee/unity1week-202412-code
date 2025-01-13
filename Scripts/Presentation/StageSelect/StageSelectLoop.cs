using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity1week.Extensions;
using Unity1week.Scenes;
using Unity1week202412.Installer;
using Unity1week202412.Result;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202412.StageSelect
{
    public class StageSelectLoop : Presenter, IAsyncStartable
    {
        private readonly StageSelectLogic _stageSelectLogic;
        private readonly SceneLoader _sceneLoader;
        private readonly SceneArgsContainer _sceneArgsContainer;
        private readonly GameResultPresenter _gameResultPresenter;
        private readonly GameResultService _gameResultService;
        private readonly NextStagePresenter _nextStagePresenter;

        public StageSelectLoop(
            StageSelectLogic stageSelectLogic,
            SceneLoader sceneLoader,
            SceneArgsContainer sceneArgsContainer,
            GameResultPresenter gameResultPresenter,
            GameResultService gameResultService,
            NextStagePresenter nextStagePresenter)
        {
            _stageSelectLogic = stageSelectLogic;
            _sceneLoader = sceneLoader;
            _sceneArgsContainer = sceneArgsContainer;
            _gameResultPresenter = gameResultPresenter;
            _gameResultService = gameResultService;
            _nextStagePresenter = nextStagePresenter;
        }

        public async UniTask StartAsync(CancellationToken cancellation = new())
        {
            await _sceneLoader.WaitTransitionOut();

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellation);

            while (!cancellation.IsCancellationRequested && _stageSelectLogic.HasNext())
            {
                _stageSelectLogic.Next();

                var inGameSceneData = _stageSelectLogic.GetCurrent();
                Debug.Log("Stage: " + inGameSceneData.name);
                _sceneArgsContainer.Set(inGameSceneData);

                await _nextStagePresenter.ShowAsync(_stageSelectLogic.CurrentIndex + 1, cancellation);
                await UniTask.Delay(TimeSpan.FromSeconds(1.6f), cancellationToken: cancellation);
                await _nextStagePresenter.HideAsync(cancellation);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellation);

                using (LifetimeScope.EnqueueParent(LifetimeScope.Find<StageSelectLifetimeScope>()))
                {
                    var scene = await _sceneLoader.LoadAdditiveAsync("InGame", cancellation);

                    // シーンがアンロードされるまで待機
                    await _sceneLoader.WaitForUnloaded(scene, cancellation);
                }


                await _sceneLoader.WaitTransitionOut();

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellation);
            }

            await GameClear(cancellation);
        }

        private async UniTask GameClear(CancellationToken cancellation)
        {
            await _gameResultPresenter.ShowAsync(cancellation);
        }
    }
}