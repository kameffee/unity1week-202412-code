using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202412.Title
{
    public class TitleMenuView : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;

        public Observable<Unit> OnClickStartAsObservable() => _startButton.OnClickAsObservable();
    }
}