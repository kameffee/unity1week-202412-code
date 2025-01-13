using TMPro;
using UnityEngine;

namespace Unity1week202412.Battle.Result
{
    public class PlayerScoreDetailView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreText;

        public void SetScore(string scoreText)
        {
            _scoreText.text = scoreText;
        }
    }
}