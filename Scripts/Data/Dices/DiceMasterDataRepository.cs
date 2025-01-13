using System.Linq;

namespace Unity1week202412.Dices
{
    public class DiceMasterDataRepository
    {
        private readonly DiceMasterDataSource _dataSource;

        public DiceMasterDataRepository(DiceMasterDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public DiceMasterData Get(DiceId diceId)
        {
            return _dataSource.Datas.First(data => data.DiceId == diceId);
        }
    }
}