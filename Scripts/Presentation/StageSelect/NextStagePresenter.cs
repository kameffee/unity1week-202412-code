using System.Threading;
using Cysharp.Threading.Tasks;

namespace Unity1week202412.StageSelect
{
    public class NextStagePresenter
    {
        private readonly NextStageView _view;

        public NextStagePresenter(NextStageView view)
        {
            _view = view;
        }

        public async UniTask ShowAsync(int stageId, CancellationToken cancellationToken = default)
        {
            _view.SetStageName($"{stageId}戦目");
            await _view.ShowAsync();
        }

        public async UniTask HideAsync(CancellationToken cancellationToken = default)
        {
            await _view.HideAsync(cancellationToken);
        }
    }
}