using R3;
using Unity1week.Extensions;
using Unity1week202412.Dices;
using VContainer.Unity;

namespace Unity1week202412.Field
{
    public class FieldPresenter : Presenter, IInitializable
    {
        private readonly OutSideField _outSideField;
        private readonly OutSideDiceContainer _outSideDiceContainer;

        public FieldPresenter(
            OutSideField outSideField,
            OutSideDiceContainer outSideDiceContainer)
        {
            _outSideField = outSideField;
            _outSideDiceContainer = outSideDiceContainer;
        }

        public void Initialize()
        {
            _outSideField.OnOutSideDiceAsObservable()
                .Subscribe(diceObjectView => _outSideDiceContainer.Add(diceObjectView))
                .AddTo(this);
        }
    }
}