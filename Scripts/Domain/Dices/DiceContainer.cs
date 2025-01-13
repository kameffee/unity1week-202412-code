using System.Collections.Generic;

namespace Unity1week202412.Dices
{
    public class DiceContainer
    {
        public int Count => _diceObjects.Count;

        private readonly List<DiceObjectView> _diceObjects = new();

        public void AddDice(DiceObjectView diceObjectView)
        {
            _diceObjects.Add(diceObjectView);
        }

        public void AddDice(DiceObjectView[] diceObjectViews)
        {
            _diceObjects.AddRange(diceObjectViews);
        }

        public bool AllStopped()
        {
            return _diceObjects.TrueForAll(diceObject => diceObject.IsStopped());
        }

        public IEnumerable<DiceObjectView> All() => _diceObjects;

        public void DestroyAllDice()
        {
            foreach (var diceObject in _diceObjects)
            {
                UnityEngine.Object.Destroy(diceObject.gameObject);
            }

            _diceObjects.Clear();
        }
    }
}