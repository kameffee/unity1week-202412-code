using Cysharp.Threading.Tasks;
using R3;
using Unity1week.Extensions;
using Unity1week202412.Battle;
using UnityEngine;
using UnityEngine.Assertions;

namespace Unity1week202412.Character
{
    public class OpponentCharacterPresenter : Presenter
    {
        private readonly OpponentCharacterService _opponentCharacterService;
        private readonly InGameSceneDataService _inGameSceneDataService;
        private readonly Transform _parent;
        private OpponentCharacterView _view;

        public OpponentCharacterPresenter(
            OpponentCharacterService opponentCharacterService,
            InGameSceneDataService inGameSceneDataService,
            Transform parent)
        {
            _opponentCharacterService = opponentCharacterService;
            _inGameSceneDataService = inGameSceneDataService;
            _parent = parent;
        }

        public async UniTask SetupAsync()
        {
            var data = _inGameSceneDataService.Get();
            var path = $"Character/{data.OpponentCharacterPath}";
            var asset = await Resources.LoadAsync<OpponentCharacterView>(path);
            var prefab = asset as OpponentCharacterView;
            Assert.IsNotNull(prefab);

            _view = Object.Instantiate(prefab, _parent);
            OnSubscribe(_view);
        }

        private void OnSubscribe(OpponentCharacterView view)
        {
            _opponentCharacterService.CurrentEmote
                .Subscribe(emoteType => view.SetEmote(emoteType))
                .AddTo(this);
        }

    }
}