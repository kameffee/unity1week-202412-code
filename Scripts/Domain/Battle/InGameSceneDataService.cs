using Unity1week.Scenes;

namespace Unity1week202412.Battle
{
    public class InGameSceneDataService
    {
        private readonly InGameSceneData _inGameDefaultSceneData;
        private readonly SceneArgsContainer _sceneArgsContainer;

        public InGameSceneDataService(
            InGameSceneData defaultSceneData,
            SceneArgsContainer sceneArgsContainer)
        {
            _inGameDefaultSceneData = defaultSceneData;
            _sceneArgsContainer = sceneArgsContainer;
        }

        public InGameSceneData Get()
        {
            if (_sceneArgsContainer.TryGet<InGameSceneData>(out var sceneData))
            {
                return sceneData;
            }
            
            // なかったらdefaultを返す
            return _inGameDefaultSceneData;
        }
    }
}