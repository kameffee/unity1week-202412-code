using UnityEngine;

namespace Unity1week202412.Dices
{
    [CreateAssetMenu(fileName = "DiceMasterData_", menuName = "Dice/DiceMasterData")]
    public class DiceMasterData : ScriptableObject
    {
        public DiceId DiceId => new(_diceId);
        public string DiceName => _diceName;
        public DiceFaceData DiceFaceData => _diceFaceData;
        public GameObject Prefab => _dicePrefab;

        [SerializeField]
        private int _diceId;

        [SerializeField]
        private string _diceName;

        [SerializeField]
        private DiceFaceData _diceFaceData;

        [SerializeField]
        private GameObject _dicePrefab;
    }
}