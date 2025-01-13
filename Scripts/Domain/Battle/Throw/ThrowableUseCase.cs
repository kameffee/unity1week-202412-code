namespace Unity1week202412.Battle.Throw
{
    public class ThrowableUseCase
    {
        private readonly ThrowResultContainer _throwResultContainer;

        public ThrowableUseCase(ThrowResultContainer throwResultContainer)
        {
            _throwResultContainer = throwResultContainer;
        }

        public bool Throwable(bool isPlayer)
        {
            var throwResults = _throwResultContainer.GetResults(isPlayer ? UserId.Player : UserId.Opponent);
            return ThrowableCalculator.Throwable(throwResults.GetAll());
        }
    }
}