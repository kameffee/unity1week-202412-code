using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202412.Talk
{
    public class TalkWindowView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Button _clickButton;
        
        [SerializeField]
        private TextMeshProUGUI _messageText;

        private void Awake()
        {
            Hide();
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        public Observable<Unit> OnSubmitAsObservable() => _clickButton.OnClickAsObservable();

        public async UniTask PlayTalk(string message, CancellationToken cancellationToken = default)
        {
            var duration = message.Length * 0.03f;
            await LMotion.String.Create128Bytes(string.Empty, message, duration)
                .WithRichText()
                .WithOnComplete(() => _messageText.SetText(message))
                .BindToText(_messageText)
                .AddTo(this)
                .ToUniTask(cancellationToken);
        } 
    }
}