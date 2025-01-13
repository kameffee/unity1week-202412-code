using System;
using System.Collections.Generic;
using R3;
using Unity.Cinemachine;
using UnityEngine;

namespace Unity1week202412.Battle
{
    public enum CameraMode
    {
        Ready,
        Throwing,
        TakeDice,
    }

    public class BattleCameraController : MonoBehaviour
    {
        [Serializable]
        public class CameraProfile
        {
            public CameraMode CameraMode => _cameraMode;
            public CinemachineCamera Camera => _camera;

            [SerializeField]
            private CameraMode _cameraMode;
            
            [SerializeField]
            private CinemachineCamera _camera;
        }

        [SerializeField]
        private List<CameraProfile> _cameraProfiles;

        public void ChangeCamera(CameraMode cameraMode)
        {
            Debug.Log($"ChangeCamera: {cameraMode}");
            foreach (var cameraProfile in _cameraProfiles)
            {
                cameraProfile.Camera.Priority = cameraProfile.Camera == _cameraProfiles[(int)cameraMode].Camera ? 10 : 0;
            }
        }
    }
}