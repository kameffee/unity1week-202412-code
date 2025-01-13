using System.Threading;
using Cysharp.Threading.Tasks;

namespace Unity1week202412.Dices
{
    public class AllDiceStopUseCase
    {
        private readonly DiceContainer _diceContainer;

        public AllDiceStopUseCase(DiceContainer diceContainer)
        {
            _diceContainer = diceContainer;
        }

        public async UniTask WaitAllDiceStop(CancellationToken cancellationToken)
        {
            await UniTask.WaitUntil(() => _diceContainer.AllStopped(), cancellationToken: cancellationToken);
        }
    }
}