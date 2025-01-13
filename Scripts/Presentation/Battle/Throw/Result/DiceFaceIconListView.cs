using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity1week.Audio;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202412.Battle.Throw.Result
{
    public class DiceFaceIconListView : MonoBehaviour
    {
        [SerializeField]
        private DiceFaceIconView _diceFaceIconViewPrefab;

        [SerializeField]
        private Transform _contentHolder;
        
        private AudioPlayer _audioPlayer;

        private readonly List<DiceFaceIconView> _diceFaceIconViews = new();

        private void Start()
        {
            var lifetimeScope = LifetimeScope.Find<LifetimeScope>();
            lifetimeScope.Container.Inject(this);
        }

        [Inject]
        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void SetFaces(IReadOnlyList<DiceFaceIconType> faceTypes)
        {
            _diceFaceIconViews.ForEach(view => Destroy(view.gameObject));
            _diceFaceIconViews.Clear();

            for (var i = 0; i < faceTypes.Count; i++)
            {
                var iconView = Instantiate(_diceFaceIconViewPrefab, _contentHolder);
                iconView.Hide();
                iconView.SetFace(faceTypes[i]);
                _diceFaceIconViews.Add(iconView);
            }
        }

        public async UniTask ShowPerform(CancellationToken cancellationToken = default)
        {
            foreach (var iconView in _diceFaceIconViews)
            {
                iconView.Show();
                _audioPlayer.PlaySe("Battle/Result/Dice");
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: cancellationToken);
            }
        }
    }
}