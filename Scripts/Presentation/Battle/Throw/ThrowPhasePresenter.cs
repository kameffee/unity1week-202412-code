using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Unity1week202412.Battle.Balances;

namespace Unity1week202412.Battle.Throw
{
    public class ThrowPhasePresenter
    {
        private readonly ThrowPhaseView _view;
        private readonly Balance _balance;

        public ThrowPhasePresenter(ThrowPhaseView view, Balance balance)
        {
            _view = view;
            _balance = balance;
        }

        public void Show()
        {
            _view.VisibleThrowAndTakeDiceButton(_balance.AnyLeft());
            _view.Show();
        }

        public async UniTask<ThrowType> WaitForChoice(CancellationToken cancellation)
        {
            var tuple = await UniTask.WhenAny(
                _view.OnClickThrowAsObservable().FirstAsync(cancellationToken: cancellation).AsUniTask(),
                _view.OnClickThrowAndTakeDiceAsObservable().FirstAsync(cancellationToken: cancellation).AsUniTask()
            );
            return tuple.winArgumentIndex switch
            {
                0 => ThrowType.Throw,
                1 => ThrowType.ThrowAndTakeDice,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}