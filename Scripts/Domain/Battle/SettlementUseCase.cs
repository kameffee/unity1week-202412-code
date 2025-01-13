using System;
using System.Linq;
using Unity1week202412.Battle.Balances;
using Unity1week202412.Battle.Throw;
using Unity1week202412.Scores;

namespace Unity1week202412.Battle
{
    /// <summary>
    /// 精算
    /// </summary>
    public class SettlementUseCase
    {
        private readonly ThrowResultContainer _throwResultContainer;
        private readonly Balance _balance;
        
        public SettlementUseCase(ThrowResultContainer throwResultContainer, Balance balance)
        {
            _throwResultContainer = throwResultContainer;
            _balance = balance;
        }

        public void Settlement()
        {
            var playerThrowResult = _throwResultContainer.GetResults(UserId.Player).GetBest();
            var opponentThrowResult = _throwResultContainer.GetResults(UserId.Opponent).GetBest();

            if (playerThrowResult.Type > opponentThrowResult.Type)
            {
                var leftAddValue = ThrowResultToWeight(playerThrowResult.Type);
                _balance.AddLeft(leftAddValue);
            }
            else if (playerThrowResult.Type < opponentThrowResult.Type)
            {
                var rightAddValue = ThrowResultToWeight(opponentThrowResult.Type);
                _balance.AddRight(rightAddValue);
            }
        }

        private static int ThrowResultToWeight(ScoreType type)
        {
            switch (type)
            {
                case ScoreType.Hifumi:
                    return -2;
                case ScoreType.Shonben:
                case ScoreType.MeNashi:
                    return 0;
                case ScoreType.One:
                case ScoreType.Two:
                case ScoreType.Three:
                case ScoreType.Four:
                case ScoreType.Five:
                case ScoreType.Six:
                    return 1;
                case ScoreType.Shigoro:
                    return 2;
                case ScoreType.ZoromeTwo:
                case ScoreType.ZoromeThree:
                case ScoreType.ZoromeFour:
                case ScoreType.ZoromeFive:
                case ScoreType.ZoromeSix:
                    return 3;
                case ScoreType.PinZorome:
                    return 5;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}