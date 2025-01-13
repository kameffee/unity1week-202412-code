using Cysharp.Threading.Tasks;

namespace Unity1week.Scenes
{
    public interface ISceneTransitionView
    {
        UniTask ShowAsync();

        UniTask HideAsync();
    }
}