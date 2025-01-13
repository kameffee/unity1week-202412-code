using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Unity1week202412.Battle.Balances;
using Unity1week202412.Dices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unity1week202412.Battle.Throw
{
    public class ThrowPhaseUseCase
    {
        private readonly ThrowPhasePresenter _presenter;
        private readonly DiceContainer _diceContainer;
        private readonly DiceObjectFactory _diceObjectFactory;
        private readonly BattleCameraController _battleCameraController;
        private readonly Balance _balance;

        public ThrowPhaseUseCase(
            ThrowPhasePresenter presenter,
            DiceContainer diceContainer,
            DiceObjectFactory diceObjectFactory,
            BattleCameraController battleCameraController,
            Balance balance)
        {
            _presenter = presenter;
            _diceContainer = diceContainer;
            _diceObjectFactory = diceObjectFactory;
            _battleCameraController = battleCameraController;
            _balance = balance;
        }

        public async UniTask WaitThrowInput(CancellationToken cancellationToken)
        {
            _presenter.Show();
            var throwType = await _presenter.WaitForChoice(cancellationToken);
            _presenter.Hide();

            switch (throwType)
            {
                case ThrowType.Throw:
                    Throw();
                    break;
                case ThrowType.ThrowAndTakeDice:
                    await TakeAndThrow();
                    break;
            }
        }

        public void Throw()
        {
            _diceContainer.AddDice(new[]
            {
                _diceObjectFactory.Create(new DiceId(1), new BattleDiceId(1), new Vector3(0, 0.7f, 0.2f),
                    RandomRotation()),
                _diceObjectFactory.Create(new DiceId(1), new BattleDiceId(2), new Vector3(-0.08f, 0.6f, 0.3f),
                    RandomRotation()),
                _diceObjectFactory.Create(new DiceId(1), new BattleDiceId(3), new Vector3(0.1f, 0.6f, -0.2f),
                    RandomRotation()),
            });
        }

        private async UniTask TakeAndThrow()
        {
            _battleCameraController.ChangeCamera(CameraMode.TakeDice);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));

            _balance.RemoveLeft(1);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _battleCameraController.ChangeCamera(CameraMode.Throwing);
            await UniTask.Delay(TimeSpan.FromSeconds(0.8f));

            _diceContainer.AddDice(new[]
            {
                _diceObjectFactory.Create(new DiceId(1), new BattleDiceId(1), new Vector3(0, 0.7f, 0.2f),
                    RandomRotation()),
                _diceObjectFactory.Create(new DiceId(1), new BattleDiceId(2), new Vector3(-0.08f, 0.6f, 0.3f),
                    RandomRotation()),
                _diceObjectFactory.Create(new DiceId(1), new BattleDiceId(3), new Vector3(0.1f, 0.6f, -0.2f),
                    RandomRotation()),
                _diceObjectFactory.Create(new DiceId(1), new BattleDiceId(4), new Vector3(-0.1f, 0.6f, -0.2f),
                    RandomRotation()),
            });
        }

        private static Quaternion RandomRotation()
        {
            return Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }
    }
}