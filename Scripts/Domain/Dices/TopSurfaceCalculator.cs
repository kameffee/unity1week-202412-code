using UnityEngine;

namespace Unity1week202412.Dices
{
    public static class TopSurfaceCalculator
    {
        public static Vector3 Calculate(Transform diceTransform)
        {
            var innerProductX = Vector3.Dot(diceTransform.right, Vector3.up);
            var innerProductY = Vector3.Dot(diceTransform.up, Vector3.up);
            var innerProductZ = Vector3.Dot(diceTransform.forward, Vector3.up);

            // X軸が最も垂直な場合
            if ((Mathf.Abs(innerProductX) > Mathf.Abs(innerProductY)) &&
                (Mathf.Abs(innerProductX) > Mathf.Abs(innerProductZ)))
            {
                return innerProductX > 0f ? Vector3.right : Vector3.left;
            }

            // Y軸が最も垂直な場合
            if ((Mathf.Abs(innerProductY) > Mathf.Abs(innerProductX)) &&
                (Mathf.Abs(innerProductY) > Mathf.Abs(innerProductZ)))
            {
                return innerProductY > 0f ? Vector3.up : Vector3.down;
            }

            // Z軸が最も垂直な場合
            return innerProductZ > 0f ? Vector3.forward : Vector3.back;
        }
    }
}