using R3;
using Unity1week.Audio;
using UnityEngine;
using VContainer;

namespace Unity1week202412.Dices
{
    public class DiceObjectView : MonoBehaviour
    {
        public BattleDiceId UniqueId { get; private set; }
        public DiceId DiceId { get; private set; }
        public Transform Transform => transform;

        [SerializeField]
        private Rigidbody _rigidbody;

        [Header("SE")]
        [SerializeField]
        private string _hitSePath;

        private AudioPlayer _audioPlayer;

        private readonly Subject<Unit> _onOutSide = new();

        [Inject]
        public void Construct(AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
        }

        public void Initialize(DiceId diceId, BattleDiceId uniqueId)
        {
            DiceId = diceId;
            UniqueId = uniqueId;
        }

        public bool IsStopped()
        {
            return _rigidbody.IsSleeping();
        }

        public void OnOutSideField() => _onOutSide.OnNext(Unit.Default);

        public Observable<Unit> OnOutSideAsObservable() => _onOutSide;

        private void OnCollisionEnter(Collision collision)
        {
            _audioPlayer.PlaySe(_hitSePath, Mathf.Clamp01(collision.impulse.magnitude / 3f));
        }
    }
}