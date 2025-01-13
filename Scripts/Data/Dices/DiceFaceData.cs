using System;
using UnityEngine;

namespace Unity1week202412.Dices
{
    [Serializable]
    public class DiceFaceData
    {
        [SerializeField]
        private int _right;

        [SerializeField]
        private int _left;

        [SerializeField]
        private int _up;

        [SerializeField]
        private int _down;

        [SerializeField]
        private int _forward;

        [SerializeField]
        private int _back;
        
        public int ToNumber(Vector3 direction)
        {
            return direction switch
            {
                _ when direction == Vector3.right => _right,
                _ when direction == Vector3.left => _left,
                _ when direction == Vector3.up => _up,
                _ when direction == Vector3.down => _down,
                _ when direction == Vector3.forward => _forward,
                _ when direction == Vector3.back => _back,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}