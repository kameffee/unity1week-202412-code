using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity1week.Extensions;
using Unity1week.Scenes;
using Unity1week202412.Battle;
using Unity1week202412.Battle.Balances;
using Unity1week202412.Battle.Result;
using Unity1week202412.Battle.Terminate;
using Unity1week202412.Battle.Throw;
using Unity1week202412.Battle.Throw.Result;
using Unity1week202412.Battle.Turn;
using Unity1week202412.Dices;
using Unity1week202412.Result;
using Unity1week202412.Scores;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Unity1week202412
{
    public class InGameLoop : Presenter, IAsyncStartable
    {
        private readonly InGameInitializer _inGameInitializer;
        private readonly DiceObjectFactory _diceObjectFactory;
        private readonly DiceContainer _diceContainer;
        private readonly AllDiceStopUseCase _allDiceStopUseCase;
        private readonly DiceService _diceService;
        private readonly ResultPresenter _resultPresenter;
        private readonly OutSideDiceContainer _outSideDiceContainer;
        private readonly TurnPresenter _turnPresenter;
        private readonly BattleInformation _battleInformation;
        private readonly ThrowableUseCase _throwableUseCase;
        private readonly RecordThrowResultUseCase _recordThrowResultUseCase;
        private readonly TurnStatePresenter _turnStatePresenter;
        private readonly BattleCameraController _battleCameraController;
        private readonly ThrowPhaseUseCase _throwPhaseUseCase;
        private readonly SettlementUseCase _settlementUseCase;
        private readonly BattleTerminateUseCase _battleTerminateUseCase;
        private readonly BattleResultPresenter _battleResultPresenter;
        private readonly SceneLoader _sceneLoader;
        private readonly InGameSceneDataService _inGameSceneDataService;
        private readonly SetGameResultUseCase _setGameResultUseCase;
        private readonly InGamePrePerform _inGamePrePerform;

        public InGameLoop(
            InGameInitializer inGameInitializer,
            DiceObjectFactory diceObjectFactory,
            DiceContainer diceContainer,
            AllDiceStopUseCase allDiceStopUseCase,
            DiceService diceService,
            ResultPresenter resultPresenter,
            OutSideDiceContainer outSideDiceContainer,
            TurnPresenter turnPresenter,
            BattleInformation battleInformation,
            ThrowableUseCase throwableUseCase,
            RecordThrowResultUseCase recordThrowResultUseCase,
            TurnStatePresenter turnStatePresenter,
            BattleCameraController battleCameraController,
            ThrowPhaseUseCase throwPhaseUseCase,
            SettlementUseCase settlementUseCase,
            BattleTerminateUseCase battleTerminateUseCase,
            BattleResultPresenter battleResultPresenter,
            SceneLoader sceneLoader,
            InGameSceneDataService inGameSceneDataService,
            SetGameResultUseCase setGameResultUseCase,
            InGamePrePerform inGamePrePerform)
        {
            _inGameInitializer = inGameInitializer;
            _diceObjectFactory = diceObjectFactory;
            _diceContainer = diceContainer;
            _allDiceStopUseCase = allDiceStopUseCase;
            _diceService = diceService;
            _resultPresenter = resultPresenter;
            _outSideDiceContainer = outSideDiceContainer;
            _turnPresenter = turnPresenter;
            _battleInformation = battleInformation;
            _throwableUseCase = throwableUseCase;
            _recordThrowResultUseCase = recordThrowResultUseCase;
            _turnStatePresenter = turnStatePresenter;
            _battleCameraController = battleCameraController;
            _throwPhaseUseCase = throwPhaseUseCase;
            _settlementUseCase = settlementUseCase;
            _battleTerminateUseCase = battleTerminateUseCase;
            _battleResultPresenter = battleResultPresenter;
            _sceneLoader = sceneLoader;
            _inGameSceneDataService = inGameSceneDataService;
            _setGameResultUseCase = setGameResultUseCase;
            _inGamePrePerform = inGamePrePerform;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _battleCameraController.ChangeCamera(CameraMode.Ready);

            await _inGameInitializer.Initialize();

            await _sceneLoader.WaitTransitionOut();

            _battleInformation.SetFirstTurn(false);

            await _inGamePrePerform.PerformAsync(cancellation);

            while (!cancellation.IsCancellationRequested)
            {
                _battleCameraController.ChangeCamera(CameraMode.Throwing);

                const int playerCount = 2;
                for (int i = 0; i < playerCount; i++)
                {
                    _turnStatePresenter.Update();
                    await _turnPresenter.ShowAsync(cancellation);

                    if (_battleInformation.IsMyTurn())
                    {
                        await UserTurn(cancellation);
                    }
                    else if (_battleInformation.IsOpponentTurn())
                    {
                        await OpponentTurn(cancellation);
                    }

                    _battleInformation.NextTurn();
                }

                _battleCameraController.ChangeCamera(CameraMode.Ready);

                await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: cancellation);

                // 精算する
                _settlementUseCase.Settlement();

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellation);

                _recordThrowResultUseCase.Reset();

                // 勝敗判定
                if (_battleTerminateUseCase.IsTerminate())
                {
                    break;
                }
            }

            await ResultAsync(cancellation);
        }

        private async UniTask ResultAsync(CancellationToken cancellationToken)
        {
            Debug.Log("Result");
            var isWin = _battleTerminateUseCase.IsWin();
            _setGameResultUseCase.Set(isWin);
            await _battleResultPresenter.ShowAsync(isWin, cancellationToken);

            // シーンをアンロード
            await _sceneLoader.UnloadAsync();
        }

        private async UniTask OpponentTurn(CancellationToken cancellation)
        {
            Debug.Log("相手のターン");

            while (_throwableUseCase.Throwable(false))
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellation);

                var data = _inGameSceneDataService.Get();
                _diceContainer.AddDice(new[]
                {
                    _diceObjectFactory.Create(data.OpponentDiceId, new BattleDiceId(1), new Vector3(0, 0.7f, 0.2f),
                        RandomRotation()),
                    _diceObjectFactory.Create(data.OpponentDiceId, new BattleDiceId(2), new Vector3(-0.08f, 0.6f, 0.3f),
                        RandomRotation()),
                    _diceObjectFactory.Create(data.OpponentDiceId, new BattleDiceId(3), new Vector3(0.1f, 0.6f, -0.2f),
                        RandomRotation()),
                });

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellation);

                await _allDiceStopUseCase.WaitAllDiceStop(cancellation);

                var outSideDiceIds = _outSideDiceContainer.GetDiceIds().ToArray();
                var allNumbers = _diceService.GetAllNumbers(outSideDiceIds);
                var result = ScoreCalculator.Calculate(allNumbers);
                Debug.Log($"出目: {string.Join(",", allNumbers)}, 結果:{result.Type}");

                if (result.Type is ScoreType.MeNashi or ScoreType.Shonben)
                {
                    // メなしまたはションベンの場合はリトライ
                    _battleInformation.NextThrowPhase();
                    Debug.Log("振り直し");
                }

                await _resultPresenter.ShowAsync(result, result.IsScoreHand(), cancellation);

                _recordThrowResultUseCase.Record(UserId.Opponent, result);

                if (result.IsScoreHand())
                {
                    await _resultPresenter.WaitSubmitAsync(cancellation);
                }
                else
                {
                    await UniTask.WhenAny(
                        UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellation),
                        UserInput.WaitAnyInput(cancellation)
                    );
                }

                await _resultPresenter.HideAsync(cancellation);

                _diceContainer.DestroyAllDice();
                _outSideDiceContainer.Clear();

                _turnStatePresenter.Update();
            }
        }

        private async UniTask UserTurn(CancellationToken cancellation)
        {
            Debug.Log("自分のターン");

            while (_throwableUseCase.Throwable(true))
            {
                await _throwPhaseUseCase.WaitThrowInput(cancellation);

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellation);

                await _allDiceStopUseCase.WaitAllDiceStop(cancellation);

                var outSideDiceIds = _outSideDiceContainer.GetDiceIds().ToArray();
                var allNumbers = _diceService.GetAllNumbers(outSideDiceIds);
                var result = ScoreCalculator.Calculate(allNumbers);
                Debug.Log($"出目: {string.Join(",", allNumbers)}, 結果:{result.Type}");

                if (result.Type is ScoreType.MeNashi or ScoreType.Shonben)
                {
                    // メなしまたはションベンの場合はリトライ
                    _battleInformation.NextThrowPhase();
                }

                await _resultPresenter.ShowAsync(result, true, cancellation);
                _recordThrowResultUseCase.Record(UserId.Player, result);

                await _resultPresenter.WaitSubmitAsync(cancellation);

                await _resultPresenter.HideAsync(cancellation);

                _diceContainer.DestroyAllDice();
                _outSideDiceContainer.Clear();

                _turnStatePresenter.Update();
            }
        }

        private static Quaternion RandomRotation()
        {
            return Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }
    }
}