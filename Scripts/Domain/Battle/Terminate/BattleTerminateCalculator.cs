using Unity1week202412.Battle.Balances;

namespace Unity1week202412.Battle.Terminate
{
    public static class BattleTerminateCalculator
    {
        public static bool IsTerminate(Balance balance)
        {
            return balance.IsLeftMax() || balance.IsRightMax();
        }
    }
}