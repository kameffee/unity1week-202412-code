using System.Collections.Generic;
using Unity1week.Extensions;
using Unity1week.Scenes;
using Unity1week202412.Result;
using Unity1week202412.Talk;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private SceneTransitionViewBase _sceneTransitionView;

        [SerializeField]
        private TalkWindowView _talkWindowViewPrefab;

        [SerializeField]
        private List<LifetimeScopeBuilder> _lifetimeScopes;

        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureScene(builder);
            TalkConfigure(builder);
            builder.Register<GameResultService>(Lifetime.Singleton);

            foreach (var lifetimeScopeBuilder in _lifetimeScopes)
            {
                lifetimeScopeBuilder.Configure(builder);
            }
        }

        private void ConfigureScene(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<SceneArgsContainer>(Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab(_sceneTransitionView, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .AsImplementedInterfaces();
        }

        private void TalkConfigure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab<TalkWindowView>(_talkWindowViewPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad();
            builder.RegisterEntryPoint<TalkPresenter>();
        }
    }
}