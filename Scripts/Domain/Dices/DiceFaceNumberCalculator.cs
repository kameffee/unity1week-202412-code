using UnityEngine;

namespace Unity1week202412.Dices
{
    public class DiceFaceNumberCalculator
    {
        private readonly DiceMasterDataRepository _diceMasterDataRepository;

        public DiceFaceNumberCalculator(
            DiceMasterDataRepository diceMasterDataRepository)
        {
            _diceMasterDataRepository = diceMasterDataRepository;
        }

        public int Calculate(DiceId diceId, Vector3 topSurfaceVector)
        {
            var masterData = _diceMasterDataRepository.Get(diceId);
            return masterData.DiceFaceData.ToNumber(topSurfaceVector);
        }
    }
}