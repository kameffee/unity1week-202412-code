using R3;
using Unity1week.Extensions;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202412.Battle.ScoreManual
{
    public class ScoreManualPresenter : Presenter, IInitializable
    {
        private readonly ScoreManualView _view;

        public ScoreManualPresenter(ScoreManualView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _view.Hide();

            _view.OnClickToggleAsObservable()
                .Merge(UserInput.GetKeyDownAsObservable(KeyCode.Q))
                .Subscribe(_ => _view.Switch())
                .AddTo(this);
        }
    }
}