using Cysharp.Threading.Tasks;
using R3;
using Unity1week.Audio;
using Unity1week.Extensions;
using Unity1week.Scenes;
using VContainer.Unity;

namespace Unity1week202412.Title
{
    public class TitlePresenter : Presenter, IInitializable
    {
        private readonly TitleMenuView _titleMenuView;
        private readonly SceneLoader _sceneLoader;
        private readonly AudioPlayer _audioPlayer;

        public TitlePresenter(
            TitleMenuView titleMenuView,
            SceneLoader sceneLoader,
            AudioPlayer audioPlayer)
        {
            _titleMenuView = titleMenuView;
            _sceneLoader = sceneLoader;
            _audioPlayer = audioPlayer;
        }

        public void Initialize()
        {
            _titleMenuView.OnClickStartAsObservable()
                .Subscribe(_ => _sceneLoader.LoadAsync("StageSelect").Forget())
                .AddTo(this);

            _audioPlayer.PlayBgm("Main");
        }
    }
}