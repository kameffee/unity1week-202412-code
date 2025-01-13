using System.Collections.Generic;
using Unity1week202412.Battle;
using Unity1week202412.Result;
using Unity1week202412.StageSelect;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202412.Installer
{
    public class StageSelectLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private Transform _root;

        [SerializeField]
        private List<InGameSceneData> _inGameSceneDataList;

        [SerializeField]
        private GameResultView _gameResultViewPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<StageSelectLoop>();
            builder.Register<StageSelectLogic>(Lifetime.Singleton);
            builder.RegisterInstance<IReadOnlyList<InGameSceneData>>(_inGameSceneDataList);

            builder.RegisterComponentInNewPrefab<GameResultView>(_gameResultViewPrefab, Lifetime.Singleton)
                .UnderTransform(_root);
            builder.RegisterEntryPoint<GameResultPresenter>().AsSelf();

            NextStageConfigure(builder);
        }

        private static void NextStageConfigure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<NextStageView>();
            builder.Register<NextStagePresenter>(Lifetime.Singleton);
        }
    }
}