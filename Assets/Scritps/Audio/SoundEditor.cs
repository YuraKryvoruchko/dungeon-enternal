using UnityEngine;
using UnityEngine.Audio;
using DungeonEternal.Settings;

namespace DungeonEternal.UI
{
    public class SoundEditor : MonoBehaviour
    {
        [SerializeField] private DataForSettingsSO _dataForSetting;
        [Space]
        [SerializeField] private AudioMixer _musicAudioMixer;

        [SerializeField] private string _nameParameter = "SFXExposedParam";

        private void Start()
        {
            SetGeneralVolume(_dataForSetting.GetSoundVolume());
        }

        private void OnEnable()
        {
            ValueEditor.OnChangeSoundVolume += SetGeneralVolume;
        }
        private void OnDisable()
        {
            ValueEditor.OnChangeSoundVolume -= SetGeneralVolume;
        }

        private void SetGeneralVolume(float volume)
        {
            float gap = -80f;

            float value = gap - (gap * volume);

            _musicAudioMixer.SetFloat(_nameParameter, value);
        }
    }
}
