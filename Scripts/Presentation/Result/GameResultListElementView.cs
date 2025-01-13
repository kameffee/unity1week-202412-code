using System;
using TMPro;
using UnityEngine;

namespace Unity1week202412.Result
{
    public class GameResultListElementView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        [SerializeField]
        private TextMeshProUGUI _stageNameText;

        [SerializeField]
        private TextMeshProUGUI _resultText;

        private void Awake()
        {
            Hide();
        }

        public void Set(int stageId, bool isWin)
        {
            _stageNameText.text = $"{stageId}戦目";
            _resultText.text = isWin ? "勝利" : "敗北";
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