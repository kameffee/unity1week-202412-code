using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Unity1week202412.Battle.Turn
{
    public class TurnPresenter : IInitializable
    {
        private readonly TurnPerformView _turnPerformView;
        private readonly BattleInformation _battleInformation;

        public TurnPresenter(
            TurnPerformView turnPerformView,
            BattleInformation battleInformation)
        {
            _turnPerformView = turnPerformView;
            _battleInformation = battleInformation;
        }

        public void Initialize()
        {
        }

        public async UniTask ShowAsync(CancellationToken cancellationToken)
        {
            if (_battleInformation.IsMyTurn())
            {
                await _turnPerformView.ShowUserTurnAsync(cancellationToken);
            }
            else if (_battleInformation.IsOpponentTurn())
            {
                await _turnPerformView.ShowOpponentTurnAsync(cancellationToken);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

            await _turnPerformView.HideAsync();
        }
    }
}