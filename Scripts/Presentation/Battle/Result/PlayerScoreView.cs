using Unity1week202412.Scores;
using UnityEngine;

namespace Unity1week202412.Battle.Result
{
    public class PlayerScoreView : MonoBehaviour
    {
        [SerializeField]
        private PlayerScoreDetailView _opponentScoreDetailView;

        [SerializeField]
        private PlayerScoreDetailView _playerScoreDetailView;

        public void SetScore(UserId playerId, ThrowResult throwResult)
        {
            var message = throwResult.ToMessage();

            switch (playerId.AsPrimitive())
            {
                case 1:
                    _playerScoreDetailView.SetScore(message);
                    break;
                case 2:
                    _opponentScoreDetailView.SetScore(message);
                    break;
            }
        }

        public void ClearAll()
        {
            _opponentScoreDetailView.SetScore(string.Empty);
            _playerScoreDetailView.SetScore(string.Empty);
        }
    }
}