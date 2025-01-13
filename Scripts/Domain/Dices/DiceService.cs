using System.Linq;

namespace Unity1week202412.Dices
{
    public class DiceService
    {
        private readonly DiceContainer _diceContainer;
        private readonly DiceFaceNumberCalculator _diceFaceNumberCalculator;

        public DiceService(
            DiceContainer diceContainer,
            DiceFaceNumberCalculator diceFaceNumberCalculator)
        {
            _diceContainer = diceContainer;
            _diceFaceNumberCalculator = diceFaceNumberCalculator;
        }

        public int[] GetAllNumbers(BattleDiceId[] outsideDiceIds)
        {
            return _diceContainer.All()
                .Where(view => !outsideDiceIds.Contains(view.UniqueId))
                .Select(x =>
                {
                    var topSurfaceVector = TopSurfaceCalculator.Calculate(x.Transform);
                    var diceFaceNumber = _diceFaceNumberCalculator.Calculate(x.DiceId, topSurfaceVector);
                    return diceFaceNumber;
                })
                .ToArray();
        }
    }
}