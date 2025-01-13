using Unity1week202412.Battle.Balances;

namespace Unity1week202412.Battle.Terminate
{
    public class BattleTerminateUseCase
    {
        private readonly Balance _balance;

        public BattleTerminateUseCase(Balance balance)
        {
            _balance = balance;
        }

        public bool IsTerminate()
        {
            return BattleTerminateCalculator.IsTerminate(_balance);
        }

        public bool IsWin()
        {
            return _balance.IsLeftMax();
        }
    }
}