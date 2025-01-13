using Cysharp.Threading.Tasks;
using R3;
using Unity1week.Extensions;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202412.Talk
{
    public class TalkPresenter : Presenter
    {
        private readonly TalkWindowView _talkWindowView;

        public TalkPresenter(TalkWindowView talkWindowView)
        {
            _talkWindowView = talkWindowView;
        }

        public async UniTask PlayTalk(string message)
        {
            _talkWindowView.Show();
            await _talkWindowView.PlayTalk(message);
            await _talkWindowView.OnSubmitAsObservable().FirstAsync();
        }

        public UniTask HideAsync()
        {
            _talkWindowView.Hide();
            return UniTask.CompletedTask;
        }
    }
}