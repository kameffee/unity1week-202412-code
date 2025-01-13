using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Unity1week.Extensions;
using Unity1week202412.Scores;

namespace Unity1week202412.Battle.Throw.Result
{
    public class ResultPresenter : Presenter
    {
        private readonly ThrowResultCanvasView _view;

        public ResultPresenter(ThrowResultCanvasView view)
        {
            _view = view;
        }

        public async UniTask ShowAsync(ThrowResult throwResult, bool visibleClickAnnotation, CancellationToken cancellationToken)
        {
            _view.Initialize(throwResult.ToMessage(), ToDiceFaceIconType(throwResult).ToArray());
            await _view.ShowAsync(visibleClickAnnotation, cancellationToken);
        }

        private IEnumerable<DiceFaceIconType> ToDiceFaceIconType(ThrowResult throwResult)
        {
            return throwResult.FaceNumbers
                .OrderBy(number => number)
                .Select(DiceFaceIconTypeExtensions.ToDiceFaceIconType);
        }

        public async UniTask WaitSubmitAsync(CancellationToken cancellationToken)
        {
            await UniTask.WhenAny(
                _view.OnClickAsObservable().FirstAsync(cancellationToken: cancellationToken).AsUniTask(),
                UserInput.WaitAnyInput(cancellationToken)
            );
        }

        public async UniTask HideAsync(CancellationToken cancellation)
        {
            await _view.HideAsync(cancellation);
        }
    }
}