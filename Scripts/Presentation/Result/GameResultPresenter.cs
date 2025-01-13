using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Unity1week.Extensions;
using Unity1week.Scenes;
using VContainer.Unity;

namespace Unity1week202412.Result
{
    public class GameResultPresenter : Presenter, IInitializable
    {
        private readonly GameResultView _view;
        private readonly GameResultService _gameResultService;
        private readonly SceneLoader _sceneLoader;

        public GameResultPresenter(
            GameResultView view,
            GameResultService gameResultService,
            SceneLoader sceneLoader)
        {
            _view = view;
            _gameResultService = gameResultService;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _view.OnReturnButtonClickAsObservable()
                .Subscribe(_ => _sceneLoader.LoadAsync("Title").Forget())
                .AddTo(this);
        }

        public async UniTask ShowAsync(CancellationToken cancellationToken)
        {
            _view.Set(CreateViewModel());
            await _view.ShowAsync(cancellationToken);
        }

        private GameResultListView.ViewModel CreateViewModel()
        {
            var list = _gameResultService.GetAll()
                .Select(result =>
                    new GameResultListView.ViewModel.ResultData(
                        result.StageId,
                        result.GameResultType == GameResultType.Win))
                .ToArray();
            return new GameResultListView.ViewModel(list);
        }
    }
}