using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Unity1week.Extensions;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Unity1week202412.Battle.Balances
{
    public class BalancePresenter : Presenter, IInitializable, ITickable
    {
        private readonly Balance _balance;
        private readonly BalanceObject _balanceObject;
        private readonly BalanceWeightObjectFactory _balanceWeightObjectFactory;
        private readonly List<GameObject> _leftDiceObjectViews = new();
        private readonly List<GameObject> _rightDiceObjectViews = new();

        public BalancePresenter(
            Balance balance,
            BalanceObject balanceObject,
            BalanceWeightObjectFactory balanceWeightObjectFactory)
        {
            _balance = balance;
            _balanceObject = balanceObject;
            _balanceWeightObjectFactory = balanceWeightObjectFactory;
        }

        public void Initialize()
        {
            _balance.Value
                .SubscribeAwait(async (value, token) => await Update(value, token))
                .AddTo(this);

            _balance.OnAddLeftAsObservable()
                .Subscribe(count =>
                {
                    var dices = _balanceWeightObjectFactory.Create(WeightCreatePointType.Left, count);
                    _leftDiceObjectViews.AddRange(dices);
                })
                .AddTo(this);

            _balance.OnAddRightAsObservable()
                .Subscribe(count =>
                {
                    var dices = _balanceWeightObjectFactory.Create(WeightCreatePointType.Right, count);
                    _rightDiceObjectViews.AddRange(dices);
                })
                .AddTo(this);

            _balance.OnRemoveLeftAsObservable()
                .Subscribe(count =>
                {
                    var dices = _leftDiceObjectViews.TakeLast(count).ToArray();
                    foreach (var dice in dices)
                    {
                        _leftDiceObjectViews.Remove(dice);
                        Object.Destroy(dice);
                    }
                })
                .AddTo(this);
        }

        private async UniTask Update(ScaleValue scaleValue, CancellationToken cancellationToken)
        {
            // 重しが乗っかってから更新するために少し待つ
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: cancellationToken);

            _balanceObject.SetScale(scaleValue.Value / (float)scaleValue.Length + 0.5f);
        }

        public void Tick()
        {
            if (!Debug.isDebugBuild)
            {
                return;
            }

            var isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _balance.AddLeft(isShift ? 3 : 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _balance.AddRight(isShift ? 3 : 1);
            }
        }
    }
}