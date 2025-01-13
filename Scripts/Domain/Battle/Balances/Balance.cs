using R3;

namespace Unity1week202412.Battle.Balances
{
    public class Balance
    {
        public ReadOnlyReactiveProperty<ScaleValue> Value => _value.ToReadOnlyReactiveProperty();

        private readonly ReactiveProperty<ScaleValue> _value;
        private readonly Subject<int> _onAddLeft = new();
        private readonly Subject<int> _onAddRight = new();
        private readonly Subject<int> _onRemoveLeft = new();

        public Balance()
        {
            var initialValue = new ScaleValue(10, 0, 0);
            _value = new ReactiveProperty<ScaleValue>(initialValue);
        }

        public void AddLeft(int value)
        {
            _value.Value = _value.CurrentValue.AddLeft(value);
            _onAddLeft.OnNext(value);
        }

        public void RemoveLeft(int value)
        {
            _value.Value = _value.CurrentValue.RemoveLeft(value);
            _onRemoveLeft.OnNext(value);
        }

        public void AddRight(int value)
        {
            _value.Value = _value.CurrentValue.AddRight(value);
            _onAddRight.OnNext(value);
        }

        public Observable<int> OnAddLeftAsObservable() => _onAddLeft;
        public Observable<int> OnAddRightAsObservable() => _onAddRight;
        public Observable<int> OnRemoveLeftAsObservable() => _onRemoveLeft;

        public void Reset()
        {
            _value.Value = ScaleValue.Zero(_value.CurrentValue.Length);
        }

        public bool IsLeftMax() => _value.CurrentValue.IsLeftMax();
        public bool IsRightMax() => _value.CurrentValue.IsRightMax();

        public bool AnyLeft() => _value.CurrentValue.LeftValue > 0;
    }
}