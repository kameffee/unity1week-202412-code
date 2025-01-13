using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1week202412.Character
{
    public class OpponentCharacterView : MonoBehaviour
    {
        [Serializable]
        public class CharacterEmoteData
        {
            public EmoteType EmoteType => _emoteType;
            public GameObject EmoteObject => _emoteObject;

            [SerializeField]
            private EmoteType _emoteType;

            [SerializeField]
            private GameObject _emoteObject;
        }

        [SerializeField]
        private List<CharacterEmoteData> _characterEmoteDatas;

        private void Awake()
        {
            SetEmote(EmoteType.Normal);
        }

        public void SetEmote(EmoteType emoteType)
        {
            foreach (var characterEmoteData in _characterEmoteDatas)
            {
                var isActive = characterEmoteData.EmoteType == emoteType;
                if (characterEmoteData.EmoteObject.activeSelf != isActive)
                {
                    characterEmoteData.EmoteObject.SetActive(isActive);
                }
            }
        }
    }
}