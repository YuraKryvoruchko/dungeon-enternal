using UnityEngine;
using UnityEngine.Audio;
using DungeonEternal.Settings;

namespace DungeonEternal.UI
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusicPlayer : MonoBehaviour
    {
        [SerializeField] private DataForSettingsSO _dataForSetting;
        [Space]
        [SerializeField] private AudioMixer _musicAudioMixer;

        [SerializeField] private string _nameParameter = "MusicExposedParam";

        private void Start()
        {
            SetMusicValue(_dataForSetting.GetMusicVolume());
        }

        private void OnEnable()
        {
            ValueEditor.OnChangeMusicVolume += SetMusicValue;
        }
        private void OnDisable()
        {
            ValueEditor.OnChangeMusicVolume -= SetMusicValue;
        }

        private void SetMusicValue(float volume) 
        {
            float gap = -80f;

            float value = gap - (gap * volume);

            _musicAudioMixer.SetFloat(_nameParameter, value);
        } 
    }
}
