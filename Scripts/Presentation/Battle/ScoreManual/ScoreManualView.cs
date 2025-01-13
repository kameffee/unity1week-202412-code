using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202412.Battle
{
    public class ScoreManualView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _scoreManual;

        [SerializeField]
        private Button _toggleButton;

        public void Hide()
        {
            _scoreManual.SetActive(false);
        }
        
        public void Switch()
        {
            _scoreManual.SetActive(!_scoreManual.activeSelf);
        }

        public Observable<Unit> OnClickToggleAsObservable() => _toggleButton.OnClickAsObservable();
    }
}