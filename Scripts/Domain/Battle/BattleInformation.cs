using Unity1week202412.Battle.Balances;

namespace Unity1week202412.Battle
{
    public class BattleInformation
    {
        public int Turn { get; private set; }
        public int ThrowPhaseCount { get; private set; }
        
        public Balance Balance { get; }

        public BattleInformation(Balance balance)
        {
            Balance = balance;
            Turn = 0;
        }

        public bool IsMyTurn() => Turn % 2 == 0;
        public bool IsOpponentTurn() => !IsMyTurn();

        public void SetFirstTurn(bool isMyTurn)
        {
            Turn = isMyTurn ? 0 : 1;
            ThrowPhaseCount = 0;
        }

        public void NextTurn()
        {
            Turn++;
            ThrowPhaseCount = 0;
        }

        public void NextThrowPhase()
        {
            ThrowPhaseCount++;
        }

        public bool IsLastThrowPhase()
        {
            return ThrowPhaseCount == 2;
        }
    }
}