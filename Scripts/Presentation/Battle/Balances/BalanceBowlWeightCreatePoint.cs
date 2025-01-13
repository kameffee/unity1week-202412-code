using UnityEngine;

namespace Unity1week202412.Battle.Balances
{
    public class BalanceBowlWeightCreatePoint : MonoBehaviour
    {
        public Transform LeftPoint => _leftPoint;
        public Transform RightPoint => _rightPoint;

        [SerializeField]
        private Transform _leftPoint;

        [SerializeField]
        private Transform _rightPoint;
    }
}