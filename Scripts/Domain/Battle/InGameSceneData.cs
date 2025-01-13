using Unity1week.Scenes;
using Unity1week202412.Dices;
using UnityEngine;

namespace Unity1week202412.Battle
{
    [CreateAssetMenu(menuName = "SceneData/InGameSceneData")]
    public class InGameSceneData : ScriptableObject, ISceneArgs
    {
        public int StageId => _stageId;
        public int DishId => _dishId;
        public DiceId OpponentDiceId => _opponentDiceMasterData.DiceId;
        public string OpponentCharacterPath => _opponentCharacterPath;
        public int LeftHandicapWeight => _leftHandicapWeight;

        [SerializeField]
        private int _stageId;

        [SerializeField]
        private int _dishId;

        [SerializeField]
        private DiceMasterData _opponentDiceMasterData;

        [SerializeField]
        private string _opponentCharacterPath;

        [SerializeField]
        private int _leftHandicapWeight;
    }
}