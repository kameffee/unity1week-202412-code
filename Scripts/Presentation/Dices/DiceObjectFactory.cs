using System;
using Unity1week202412.Battle;
using UnityEngine;

namespace Unity1week202412.Dices
{
    public class DiceObjectFactory
    {
        private readonly DiceMasterDataRepository _diceMasterDataRepository;
        private readonly Func<DiceObjectView, Vector3, Quaternion, DiceObjectView> _factory;

        public DiceObjectFactory(
            DiceMasterDataRepository diceMasterDataRepository,
            Func<DiceObjectView, Vector3, Quaternion, DiceObjectView> factory)
        {
            _diceMasterDataRepository = diceMasterDataRepository;
            _factory = factory;
        }

        public DiceObjectView Create(DiceId diceId, BattleDiceId uniqueId, Vector3 worldPosition, Quaternion rotation)
        {
            var diceMasterData = _diceMasterDataRepository.Get(diceId);
            var diceObject = _factory.Invoke(diceMasterData.Prefab.GetComponent<DiceObjectView>(), worldPosition, rotation);
            diceObject.Initialize(diceId, uniqueId);
            return diceObject;
        }
    }
}