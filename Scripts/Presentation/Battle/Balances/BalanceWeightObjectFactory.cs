using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Unity1week202412.Battle.Balances
{
    public class BalanceWeightObjectFactory
    {
        private readonly GameObject _weightObjectPrefab;
        private readonly BalanceBowlWeightCreatePoint _balanceBowlWeightCreatePoint;

        public BalanceWeightObjectFactory(
            GameObject weightObjectPrefab,
            BalanceBowlWeightCreatePoint balanceBowlWeightCreatePoint)
        {
            _weightObjectPrefab = weightObjectPrefab;
            _balanceBowlWeightCreatePoint = balanceBowlWeightCreatePoint;
        }

        public IReadOnlyList<GameObject> Create(WeightCreatePointType type, int count)
        {
            var createPoint = type switch
            {
                WeightCreatePointType.Left => _balanceBowlWeightCreatePoint.LeftPoint.position,
                WeightCreatePointType.Right => _balanceBowlWeightCreatePoint.RightPoint.position,
                _ => throw new ArgumentOutOfRangeException()
            };

            List<GameObject> weightObjects = new();

            for (var i = 0; i < count; i++)
            {
                var randomRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                var point = createPoint + new Vector3(Random.Range(-0.05f, 0.05f), 0, Random.Range(-0.05f, 0.05f));
                var weightObject = Object.Instantiate(_weightObjectPrefab, point, randomRotation);
                weightObjects.Add(weightObject);
            }

            return weightObjects;
        }
    }
}