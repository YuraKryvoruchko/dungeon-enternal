using UnityEngine;

namespace DungeonEternal.Settings
{
    [CreateAssetMenu(fileName = "DataForSettingsSO", menuName = "ScriptableObjects/DataForSettings", order = 1)]
    public class DataForSettingsSO : ScriptableObject
    {
        [SerializeField] private float _sensitivityValue;
        [SerializeField] private float _globalMusicVolume;
        [SerializeField] private float _musicVolume;
        [SerializeField] private float _soundVolume;

        private const float DEFAULT_NUMBER = 1f;

        public void SetSensitivity(float value)
        {
            _sensitivityValue = value;
        }
        public void SetGlobalMusicVolume(float value)
        {
            _globalMusicVolume = value;
        }
        public void SetMusicVolume(float value)
        {
            _musicVolume = value;
        }
        public void SetSoundVolume(float value)
        {
            _soundVolume = value;
        }
        public void SetSettingValue(float value, TypeSettingValue typeSettingValue)
        {
            switch (typeSettingValue)
            {
                case TypeSettingValue.SensetiveValue:
                    SetSensitivity(value);
                    break;
                case TypeSettingValue.SoundVolume:
                    SetSoundVolume(value);
                    break;
                case TypeSettingValue.MusicVolume:
                    SetMusicVolume(value);
                    break;
                case TypeSettingValue.GeneralVolume:
                    SetGlobalMusicVolume(value);
                    break;
            }
        }
        public float GetSensitivityValue()
        {
            return _sensitivityValue;
        }
        public float GetGlobalMusicVolume()
        {
            return _globalMusicVolume;
        }
        public float GetMusicVolume()
        {
            return _musicVolume;
        }
        public float GetSoundVolume()
        {
            return _soundVolume;
        }
        public float GetSettingValue(TypeSettingValue typeSettingValue)
        {
            switch (typeSettingValue)
            {
                case TypeSettingValue.SensetiveValue:
                    return GetSensitivityValue();
                case TypeSettingValue.SoundVolume:
                    return GetSoundVolume();
                case TypeSettingValue.MusicVolume:
                    return GetMusicVolume();
                case TypeSettingValue.GeneralVolume:
                    return GetGlobalMusicVolume();
                default:
                    return DEFAULT_NUMBER;
            }
        }
    }

    public enum TypeSettingValue
    {
        SensetiveValue,
        SoundVolume,
        MusicVolume,
        GeneralVolume
    }
}
