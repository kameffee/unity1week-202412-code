using System.Collections.Generic;
using Unity1week202412.Battle;

namespace Unity1week202412.StageSelect
{
    public class StageSelectLogic
    {
        public int CurrentIndex => _currentStageIndex;

        private readonly IReadOnlyList<InGameSceneData> _inGameSceneDataList;
        private int _currentStageIndex;

        public StageSelectLogic(IReadOnlyList<InGameSceneData> inGameSceneDataList)
        {
            _inGameSceneDataList = inGameSceneDataList;
            _currentStageIndex = -1;
        }

        public void Next()
        {
            _currentStageIndex++;
        }

        public bool HasNext()
        {
            return _currentStageIndex + 1 < _inGameSceneDataList.Count;
        }

        public InGameSceneData GetCurrent()
        {
            return _inGameSceneDataList[_currentStageIndex];
        }
    }
}