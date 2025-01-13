using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Unity1week202412.Battle.Turn
{
    public class TurnPerformView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private GameObject _userTurnView;

        [SerializeField]
        private GameObject _opponentTurnView;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        public UniTask ShowUserTurnAsync(CancellationToken cancellationToken = default)
        {
            _userTurnView.SetActive(true);
            _opponentTurnView.SetActive(false);

            _canvasGroup.alpha = 1;
            return UniTask.CompletedTask;
        }

        public UniTask ShowOpponentTurnAsync(CancellationToken cancellationToken = default)
        {
            _userTurnView.SetActive(false);
            _opponentTurnView.SetActive(true);

            _canvasGroup.alpha = 1;
            return UniTask.CompletedTask;
        }

        public async UniTask HideAsync()
        {
            await LMotion.Create(1f, 0f, 1f)
                .WithEase(Ease.Linear)
                .BindToAlpha(_canvasGroup)
                .AddTo(this);
        }
    }
}