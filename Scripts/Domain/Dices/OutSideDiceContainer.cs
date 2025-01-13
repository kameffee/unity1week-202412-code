using System.Collections.Generic;
using System.Linq;

namespace Unity1week202412.Dices
{
    public class OutSideDiceContainer
    {
        private readonly List<DiceObjectView> _diceObjects = new();

        public void Add(DiceObjectView diceObjectView)
        {
            _diceObjects.Add(diceObjectView);
        }

        public bool HasOutSideDice() => _diceObjects.Any();

        public IEnumerable<BattleDiceId> GetDiceIds()
        {
            return _diceObjects.Select(diceObject => diceObject.UniqueId);
        }

        public void Clear() => _diceObjects.Clear();
    }
}