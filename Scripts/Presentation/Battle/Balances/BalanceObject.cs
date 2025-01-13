using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Unity1week202412.Battle.Balances
{
    public class BalanceObject : MonoBehaviour
    {
        [SerializeField]
        private Transform _scalePivot;

        [SerializeField]
        private float _defaultAngle = 180f;

        [SerializeField]
        private float _maxAngle = 90f;

        private float _currentScale = 0.5f;
        private MotionHandle _motionHandle;

        public void SetScale(float scale)
        {
            _currentScale = Mathf.Clamp01(scale);

            if (_motionHandle.IsActive())
                _motionHandle.Cancel();

            var angle = Mathf.LerpAngle(_defaultAngle - _maxAngle, _defaultAngle + _maxAngle, _currentScale);
            _motionHandle = LMotion.Create(_scalePivot.localEulerAngles.z, angle, 0.5f)
                .WithEase(Ease.InOutSine)
                .WithScheduler(MotionScheduler.FixedUpdate)
                .BindToLocalEulerAnglesZ(_scalePivot)
                .AddTo(this);
        }
    }
}