using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Unity1week.Scenes
{
    public abstract class SceneTransitionViewBase : MonoBehaviour, ISceneTransitionView
    {
        public abstract UniTask ShowAsync();

        public abstract UniTask HideAsync();
    }
}