using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1week202412.Battle.Throw.Result
{
    public class DiceFaceIconView : MonoBehaviour
    {
        [Serializable]
        public class DiceFaceIcon
        {
            public DiceFaceIconType FaceType => _faceType;
            public GameObject IconObject => _iconObject;

            [SerializeField]
            private DiceFaceIconType _faceType;

            [SerializeField]
            private GameObject _iconObject;
        }

        [SerializeField]
        private List<DiceFaceIcon> _diceFaceIcons;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        public void SetFace(DiceFaceIconType faceType)
        {
            foreach (var diceFaceIcon in _diceFaceIcons)
            {
                diceFaceIcon.IconObject.SetActive(diceFaceIcon.FaceType == faceType);
            }
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
        }
    }
}