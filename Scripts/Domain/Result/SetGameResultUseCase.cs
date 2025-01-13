using Unity1week202412.Battle;

namespace Unity1week202412.Result
{
    public class SetGameResultUseCase
    {
        private readonly InGameSceneDataService _inGameSceneDataService;
        private readonly GameResultService _gameResultService;

        public SetGameResultUseCase(
            InGameSceneDataService inGameSceneDataService,
            GameResultService gameResultService)
        {
            _inGameSceneDataService = inGameSceneDataService;
            _gameResultService = gameResultService;
        }

        public void Set(bool isWin)
        {
            var sceneData = _inGameSceneDataService.Get();
            var stageId = sceneData.StageId;
            _gameResultService.Add(stageId, isWin ? GameResultType.Win : GameResultType.Lose);
        }
    }
}