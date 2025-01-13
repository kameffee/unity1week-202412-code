using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Unity1week202412.Battle.Result
{
    public class BattleResultPresenter
    {
        private readonly BattleResultView _battleResultView;

        public BattleResultPresenter(BattleResultView battleResultView)
        {
            _battleResultView = battleResultView;
        }

        public async UniTask ShowAsync(bool isWin, CancellationToken cancellationToken = default)
        {
            Debug.Log("isWin: " + isWin);
            await _battleResultView.ShowAsync(isWin, cancellationToken);

            await _battleResultView.OnNextAsObservable().FirstAsync(cancellationToken: cancellationToken);

            await _battleResultView.HideAsync(cancellationToken);
        }
    }
}