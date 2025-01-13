using R3;
using Unity1week.Extensions;
using Unity1week202412.Battle.Throw;
using VContainer.Unity;

namespace Unity1week202412.Battle.Result
{
    public class PlayerScoreResultPresenter : Presenter, IInitializable
    {
        private readonly PlayerScoreView _playerScoreView;
        private readonly GetThrowResultUseCase _getThrowResultUseCase;

        public PlayerScoreResultPresenter(
            PlayerScoreView playerScoreView,
            GetThrowResultUseCase getThrowResultUseCase)
        {
            _playerScoreView = playerScoreView;
            _getThrowResultUseCase = getThrowResultUseCase;
        }

        public void Initialize()
        {
            _playerScoreView.ClearAll();
            _getThrowResultUseCase.OnBestRecordedAsObservable()
                .Subscribe(x => _playerScoreView.SetScore(x.Item1, x.Item2))
                .AddTo(this);

            _getThrowResultUseCase.OnClearAsObservable()
                .Subscribe(_ => _playerScoreView.ClearAll())
                .AddTo(this);
        }
    }
}