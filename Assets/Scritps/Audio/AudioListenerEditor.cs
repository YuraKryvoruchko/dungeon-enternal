using UnityEngine;
using UnityEngine.Audio;
using DungeonEternal.Settings;

namespace DungeonEternal.UI
{
    public class AudioListenerEditor : MonoBehaviour
    {
        [SerializeField] private DataForSettingsSO _dataForSetting;
        [Space]
        [SerializeField] private AudioMixer _musicAudioMixer;

        [SerializeField] private string _nameParameter = "AllExposedParam";

        private void Start()
        {
            SetGeneralVolume(_dataForSetting.GetGlobalMusicVolume());
        }

        private void OnEnable()
        {
            ValueEditor.OnChangeGeneralVolume += SetGeneralVolume;
        }
        private void OnDisable()
        {
            ValueEditor.OnChangeGeneralVolume -= SetGeneralVolume;
        }

        private void SetGeneralVolume(float volume)
        {
            float gap = -80f;

            float value = gap - (gap * volume);

            _musicAudioMixer.SetFloat(_nameParameter, value);
        }
    }
}
