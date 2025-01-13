using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using Unity1week202412.Scores;

namespace Unity1week202412.Battle.Throw
{
    public class ThrowResultCollection
    {
        private readonly List<ThrowResult> _throwResults = new();

        public bool HasResult() => _throwResults.Count > 0;

        public void Add(ThrowResult throwResult)
        {
            _throwResults.Add(throwResult);
        }

        public void Clear()
        {
            _throwResults.Clear();
        }

        public ThrowResult GetBest()
        {
            return _throwResults.OrderByDescending(result => result.Type).First();
        }

        public IReadOnlyList<ThrowResult> GetAll()
        {
            if (_throwResults.Count == 0)
            {
                return Array.Empty<ThrowResult>();
            }

            return _throwResults;
        }

        public bool IsBest(ThrowResult throwResult)
        {
            if (_throwResults.Count == 0)
                return true;

            return _throwResults
                .Where(result => result != throwResult)
                .All(result => result.CompareTo(throwResult) < 0);
        }
    }

    public class ThrowResultContainer
    {
        private readonly Dictionary<UserId, ThrowResultCollection> _throwResults = new();
        private readonly Dictionary<UserId, ThrowResult> _bestThrowResults = new();
        private readonly Subject<(UserId, ThrowResult)> _onRecorded = new();
        private readonly Subject<Unit> _onClear = new();
        private readonly Subject<(UserId, ThrowResult)> _onBestRecorded = new();

        public void Add(UserId playerId, ThrowResult throwResult)
        {
            if (!_throwResults.ContainsKey(playerId))
            {
                _throwResults[playerId] = new ThrowResultCollection();
            }

            _throwResults[playerId].Add(throwResult);
            _onRecorded.OnNext((playerId, throwResult));

            var isBest = _throwResults[playerId].IsBest(throwResult);
            if (isBest)
            {
                _bestThrowResults[playerId] = throwResult;
                _onBestRecorded.OnNext((playerId, throwResult));
            }
        }

        public ThrowResultCollection GetResults(UserId playerId)
        {
            if (!_throwResults.ContainsKey(playerId))
            {
                _throwResults[playerId] = new ThrowResultCollection();
            }

            return _throwResults[playerId];
        }

        public void Clear()
        {
            _throwResults.Clear();
            _bestThrowResults.Clear();
            _onClear.OnNext(Unit.Default);
        }

        public Observable<(UserId userId, ThrowResult throwResult)> OnBestRecordedAsObservable() => _onBestRecorded;

        public Observable<Unit> OnClearAsObservable() => _onClear;
    }
}