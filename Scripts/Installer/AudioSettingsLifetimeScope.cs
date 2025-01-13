using Unity1week.Audio;
using VContainer;
using VContainer.Unity;

namespace Unity1week202412.Installer
{
    public class AudioSettingsLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<AudioSettingView>();
            builder.RegisterEntryPoint<AudioSettingPresenter>();
        }
    }
}