using UnityEngine;

namespace Unity1week.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SePlayer : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
        }

        public void PlayOneShot(AudioClip audioClip, float volumeScale = 1f)
        {
            _audioSource.PlayOneShot(audioClip, volumeScale);
        }

        public void SetVolume(float volume)
        {
            _audioSource.volume = volume;
        }
    }
}