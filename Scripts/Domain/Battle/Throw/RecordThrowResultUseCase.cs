using Unity1week202412.Scores;

namespace Unity1week202412.Battle.Throw
{
    public class RecordThrowResultUseCase
    {
        private readonly ThrowResultContainer _throwResultContainer;

        public RecordThrowResultUseCase(ThrowResultContainer throwResultContainer)
        {
            _throwResultContainer = throwResultContainer;
        }

        public void Record(UserId playerId, ThrowResult throwResult)
        {
            _throwResultContainer.Add(playerId, throwResult);
        }
        
        public void Reset()
        {
            _throwResultContainer.Clear();
        }
    }
}