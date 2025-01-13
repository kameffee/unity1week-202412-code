using R3;
using Unity1week202412.Dices;
using UnityEngine;

namespace Unity1week202412.Field
{
    public class OutSideField : MonoBehaviour
    {
        private readonly Subject<DiceObjectView> _onOutSideDice = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out DiceObjectView diceObjectView))
            {
                diceObjectView.OnOutSideField();
                _onOutSideDice.OnNext(diceObjectView);
            }
        }

        public Observable<DiceObjectView> OnOutSideDiceAsObservable() => _onOutSideDice;
    }
}