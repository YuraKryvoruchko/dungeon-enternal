using System.Collections;
using UnityEngine;

namespace DungeonEternal.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip _startMusic;

        [SerializeField] private float _speedChange = 2.5f;
        [SerializeField] private float _volumeReductionLimit = 0.1f;

        private const float MAX_VOLUME = 1f;
        private const float MIN_VOLUME = 0f;

        private AudioSource _audioSource;

        private AudioClip _presentMusic;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            _presentMusic = _startMusic;

            _audioSource.clip = _presentMusic;

            _audioSource.Play();
        }

        public IEnumerator SetMusic(AudioClip audioClip)
        {
            if (audioClip != _presentMusic)
            {
                float audio1Volume = MAX_VOLUME;
                float audio2Volume = MIN_VOLUME;

                bool track2Playing = false;

                _presentMusic = audioClip;

                while (true)
                {
                    if (audio1Volume > _volumeReductionLimit)
                    {
                        audio1Volume -= _speedChange * Time.deltaTime;
                        _audioSource.volume = audio1Volume;
                    }
                    else if (audio1Volume <= _volumeReductionLimit)
                    {
                        if (track2Playing == false)
                        {
                            track2Playing = true;
                            _audioSource.clip = audioClip;
                            _audioSource.Play();
                        }

                        if (audio2Volume < MAX_VOLUME)
                        {
                            audio2Volume += _speedChange * Time.deltaTime;
                            _audioSource.volume = audio2Volume;
                        }
                        else
                        {
                            break;
                        }
                    }

                    Debug.Log("audio1Volume: " + audio1Volume);
                    Debug.Log("audio2Volume: " + audio2Volume);

                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                Debug.Log("New music = old music");
            }
        }
    }
}
