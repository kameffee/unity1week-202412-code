using System.Collections.Generic;
using UnityEngine;

namespace Unity1week202412.Dices
{
    [CreateAssetMenu(fileName = "DiceMasterDataSource", menuName = "Dice/DataSource", order = 0)]
    public class DiceMasterDataSource : ScriptableObject
    {
        public IReadOnlyList<DiceMasterData> Datas => _datas;

        [SerializeField]
        private List<DiceMasterData> _datas;
    }
}