using TMPro;
using UnityEngine;

namespace Unity1week202412.Battle.Turn
{
    public class TurnStateView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _turnUserNameText;

        [SerializeField]
        private TextMeshProUGUI _throwCountText;

        [SerializeField]
        private Color _defaultThrowCountColor;

        [SerializeField]
        private Color _lastThrowCountColor;

        public void SetTurnUserName(string userName)
        {
            _turnUserNameText.text = $"{userName}の番";
            SetThrowCount(1, false);
        }

        public void SetThrowCount(int count, bool isLast)
        {
            var color = isLast ? _lastThrowCountColor : _defaultThrowCountColor;
            var colorRGBA = ColorUtility.ToHtmlStringRGBA(color);
            _throwCountText.text = $"<b><color=#{colorRGBA}>{count}</color></b>振り目";
        }
    }
}