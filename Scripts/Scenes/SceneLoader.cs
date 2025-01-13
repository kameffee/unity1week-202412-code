using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Unity1week202412.StageSelect;
using UnityEngine.SceneManagement;

namespace Unity1week.Scenes
{
    public class SceneLoader : IDisposable
    {
        public bool IsTransitioning { get; private set; }
        private readonly ISceneTransitionView _sceneTransitionView;
        private readonly Subject<Unit> _onTransitionOut = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private readonly Subject<Scene> _unloadedScene = new();

        public SceneLoader(ISceneTransitionView sceneTransitionView)
        {
            _sceneTransitionView = sceneTransitionView;
        }

        public async UniTask LoadAsync(string sceneName, CancellationToken cancellationToken = default)
        {
            var nextSceneIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
            await LoadAsync(nextSceneIndex, cancellationToken);
        }

        public async UniTask LoadAsync(int nextSceneIndex, CancellationToken cancellationToken = default)
        {
            IsTransitioning = true;
            var linkedCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(
                _cancellationTokenSource.Token,
                cancellationToken).Token;

            await _sceneTransitionView.ShowAsync();

            var currentScene = SceneManager.GetActiveScene();

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: linkedCancellationToken);

            // 遷移先のシーンを読み込み
            await SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Additive)
                .ToUniTask(cancellationToken: linkedCancellationToken);

            // アクティブ化
            var loadedScene = SceneManager.GetSceneByBuildIndex(nextSceneIndex);
            SceneManager.SetActiveScene(loadedScene);

            // 前のシーンをアンロード
            await SceneManager.UnloadSceneAsync(currentScene)
                .ToUniTask(cancellationToken: linkedCancellationToken);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: linkedCancellationToken);

            await _sceneTransitionView.HideAsync();

            _onTransitionOut.OnNext(Unit.Default);
            IsTransitioning = false;
        }

        public async UniTask<Scene> LoadAdditiveAsync(string sceneName, CancellationToken cancellationToken = default)
        {
            var nextSceneIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
            return await LoadAdditiveAsync(nextSceneIndex, cancellationToken);
        }

        public async UniTask<Scene> LoadAdditiveAsync(int nextSceneIndex, CancellationToken cancellationToken = default)
        {
            IsTransitioning = true;
            var linkedCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(
                _cancellationTokenSource.Token,
                cancellationToken).Token;
            
            var currentScene = SceneManager.GetActiveScene();
            var sceneRoot = currentScene.GetRootGameObjects()
                .Select(x => x.GetComponent<SceneRoot>())
                .FirstOrDefault();
            if (sceneRoot != null)
            {
                sceneRoot.SetActive(false);
            }

            await _sceneTransitionView.ShowAsync();

            // 遷移先のシーンを読み込み
            await SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Additive)
                .ToUniTask(cancellationToken: linkedCancellationToken);

            var loadedScene = SceneManager.GetSceneByBuildIndex(nextSceneIndex);
            SceneManager.SetActiveScene(loadedScene);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: linkedCancellationToken);

            await _sceneTransitionView.HideAsync();

            IsTransitioning = false;
            _onTransitionOut.OnNext(Unit.Default);

            return loadedScene;
        }

        public async UniTask UnloadAsync()
        {
            IsTransitioning = true;
            await _sceneTransitionView.ShowAsync();
            
            var currentScene = SceneManager.GetActiveScene();
            await SceneManager.UnloadSceneAsync(currentScene).ToUniTask();
            
            var activeScene = SceneManager.GetActiveScene();
            var sceneRoot = activeScene.GetRootGameObjects()
                .Select(x => x.GetComponent<SceneRoot>())
                .FirstOrDefault();
            if (sceneRoot != null)
            {
                sceneRoot.SetActive(true);
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            
            await _sceneTransitionView.HideAsync();
            
            IsTransitioning = false;
            _onTransitionOut.OnNext(Unit.Default);
        }

        public async UniTask WaitForUnloaded(Scene scene, CancellationToken cancellationToken = default)
        {
            var linkedCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(
                _cancellationTokenSource.Token,
                cancellationToken).Token;

            SceneManager.sceneUnloaded += OnSceneUnloaded;

            await _unloadedScene.Where(x => x == scene)
                .FirstAsync(cancellationToken: linkedCancellationToken);

            void OnSceneUnloaded(Scene unloadedScene)
            {
                if (unloadedScene == scene)
                {
                    SceneManager.sceneUnloaded -= OnSceneUnloaded;
                    _unloadedScene.OnNext(unloadedScene);
                }
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }

        public async UniTask WaitTransitionOut()
        {
            if (!IsTransitioning)
            {
                return;
            }

            await _onTransitionOut.FirstAsync();
        }
    }
}