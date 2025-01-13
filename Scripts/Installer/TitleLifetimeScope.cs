using Unity1week202412.Title;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202412.Installer
{
    public class TitleLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<TitleMenuView>();
            builder.RegisterEntryPoint<TitlePresenter>();
        }
    }
}