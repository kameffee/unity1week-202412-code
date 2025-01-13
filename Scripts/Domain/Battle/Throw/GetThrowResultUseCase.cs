using R3;
using Unity1week202412.Scores;

namespace Unity1week202412.Battle.Throw
{
    public class GetThrowResultUseCase
    {
        private readonly ThrowResultContainer _throwResultContainer;

        public GetThrowResultUseCase(ThrowResultContainer throwResultContainer)
        {
            _throwResultContainer = throwResultContainer;
        }

        public Observable<(UserId, ThrowResult)> OnBestRecordedAsObservable()
        {
            return _throwResultContainer.OnBestRecordedAsObservable();
        }

        public Observable<Unit> OnClearAsObservable()
        {
            return _throwResultContainer.OnClearAsObservable();
        }
    }
}