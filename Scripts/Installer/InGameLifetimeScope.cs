using Unity1week202412.Battle;
using Unity1week202412.Battle.Balances;
using Unity1week202412.Battle.Dish;
using Unity1week202412.Battle.Result;
using Unity1week202412.Battle.ScoreManual;
using Unity1week202412.Battle.Terminate;
using Unity1week202412.Battle.Throw;
using Unity1week202412.Battle.Throw.Result;
using Unity1week202412.Battle.Turn;
using Unity1week202412.Character;
using Unity1week202412.Dices;
using Unity1week202412.Field;
using Unity1week202412.Result;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Unity1week202412.Installer
{
    public class InGameLifetimeScope : LifetimeScope
    {
        [Header("Character")]
        [SerializeField]
        private Transform _characterParent;

        [Header("Dice")]
        [SerializeField]
        private Transform _diceParent;

        [SerializeField]
        private DiceMasterDataSource _diceMasterDataSource;

        [Header("Balance")]
        [SerializeField]
        private GameObject _balanceWeightObjectPrefab;

        [Header("ThrowPhase")]
        [SerializeField]
        private ThrowPhaseView _throwPhaseViewPrefab;

        [Header("Turn")]
        [SerializeField]
        private TurnPerformView _turnPerformViewPrefab;

        [Header("Result")]
        [SerializeField]
        private BattleResultView _battleResultViewPrefab;

        [FormerlySerializedAs("_resultCanvasViewPrefab")]
        [SerializeField]
        private ThrowResultCanvasView _throwResultCanvasViewPrefab;

        [Header("SceneData")]
        [SerializeField]
        private InGameSceneData _debugSceneData;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InGameInitializer>(Lifetime.Singleton);
            builder.RegisterEntryPoint<InGameLoop>();

            builder.Register<InGameSceneDataService>(Lifetime.Singleton)
                .WithParameter(_debugSceneData);
            builder.Register<InGamePrePerform>(Lifetime.Singleton);

            builder.RegisterEntryPoint<PlayerScoreResultPresenter>();
            builder.RegisterComponentInHierarchy<PlayerScoreView>();

            OpponentCharacterConfigure(builder);
            BattleConfigure(builder);
            DishConfigure(builder);
            BattleResultConfigure(builder);
            DiceConfigure(builder);
            FieldConfigure(builder);
            BalanceConfigure(builder);
            ThrowPhaseConfigure(builder);
            ResultConfigure(builder);
            TurnPerformConfigure(builder);
            ScoreManualConfigure(builder);
        }

        private void OpponentCharacterConfigure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<OpponentCharacterPresenter>()
                .WithParameter(typeof(Transform), _characterParent)
                .AsSelf();
            builder.Register<OpponentCharacterService>(Lifetime.Singleton);
        }

        private static void BattleConfigure(IContainerBuilder builder)
        {
            builder.Register<BattleInformation>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<BattleCameraController>();
            builder.Register<SettlementUseCase>(Lifetime.Singleton);
            builder.Register<BattleTerminateUseCase>(Lifetime.Singleton);
            builder.Register<SetGameResultUseCase>(Lifetime.Singleton);
        }

        private static void DishConfigure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<DishSwitcher>();
            builder.Register<DishPresenter>(Lifetime.Singleton);
        }

        private void BattleResultConfigure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab<BattleResultView>(_battleResultViewPrefab, Lifetime.Singleton);
            builder.RegisterEntryPoint<BattleResultPresenter>().AsSelf();
        }

        private void BalanceConfigure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<BalanceObject>();
            builder.RegisterEntryPoint<BalancePresenter>();
            builder.Register<Balance>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<BalanceBowlWeightCreatePoint>();
            builder.Register<BalanceWeightObjectFactory>(Lifetime.Singleton)
                .WithParameter(typeof(GameObject), _balanceWeightObjectPrefab);
        }

        private void DiceConfigure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_diceMasterDataSource);
            builder.Register<DiceMasterDataRepository>(Lifetime.Singleton);
            builder.RegisterFactory<DiceObjectView, Vector3, Quaternion, DiceObjectView>(resolver =>
            {
                return (prefab, position, rotation) =>
                {
                    var diceObject = Instantiate(prefab, position, rotation, _diceParent);
                    resolver.Inject(diceObject);
                    return diceObject;
                };
            }, Lifetime.Singleton);
            builder.Register<DiceObjectFactory>(Lifetime.Singleton);
            builder.Register<DiceContainer>(Lifetime.Singleton);
            builder.Register<AllDiceStopUseCase>(Lifetime.Singleton);
            builder.Register<DiceService>(Lifetime.Singleton);
            builder.Register<DiceFaceNumberCalculator>(Lifetime.Singleton);
        }

        private static void FieldConfigure(IContainerBuilder builder)
        {
            builder.Register<OutSideDiceContainer>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<OutSideField>();
            builder.RegisterEntryPoint<FieldPresenter>().AsSelf();
        }

        private void ThrowPhaseConfigure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab<ThrowPhaseView>(_throwPhaseViewPrefab, Lifetime.Singleton);
            builder.Register<ThrowPhaseUseCase>(Lifetime.Singleton);
            builder.RegisterEntryPoint<ThrowPhasePresenter>().AsSelf();
        }

        private void ResultConfigure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab<ThrowResultCanvasView>(_throwResultCanvasViewPrefab,
                Lifetime.Singleton);
            builder.RegisterEntryPoint<ResultPresenter>().AsSelf();
            builder.Register<ThrowableUseCase>(Lifetime.Singleton);
            builder.Register<ThrowResultContainer>(Lifetime.Singleton);
            builder.Register<RecordThrowResultUseCase>(Lifetime.Singleton);
            builder.Register<GetThrowResultUseCase>(Lifetime.Singleton);
        }

        private void TurnPerformConfigure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab<TurnPerformView>(_turnPerformViewPrefab, Lifetime.Singleton);
            builder.RegisterEntryPoint<TurnPresenter>().AsSelf();
            builder.Register<TurnStatePresenter>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<TurnStateView>();
        }

        private static void ScoreManualConfigure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ScoreManualView>();
            builder.RegisterEntryPoint<ScoreManualPresenter>();
        }
    }
}