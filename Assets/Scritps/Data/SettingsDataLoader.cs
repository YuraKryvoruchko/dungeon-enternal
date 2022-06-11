using System;
using UnityEngine;
using DungeonEternal.Settings;

namespace DungeonEternal.Data
{
    public class SettingsDataLoader : MonoBehaviour
    {
        [SerializeField] private DataForSettingsSO _dataForSettingsSO;
        [Space]
        [SerializeField] private string _namePath;

        private DataForSettings _dataForSettings;

        [Serializable]
        private class DataForSettings
        {
            public float _sensitivityValue;
            public float _globalMusicVolume;
            public float _musicVolume;
            public float _soundVolume;

            public float SensitivityValue { get => _sensitivityValue; set => _sensitivityValue = value; }
            public float GlobalMusicVolume { get => _globalMusicVolume; set => _globalMusicVolume = value; }
            public float MusicVolume { get => _musicVolume; set => _musicVolume = value; }
            public float SoundVolume { get => _soundVolume; set => _soundVolume = value; }

            public DataForSettings()
            {
                _sensitivityValue = 1f;
                _globalMusicVolume = 1f;
                _musicVolume = 1f;
                _soundVolume = 1f;
            }
        }

        private void OnEnable()
        {
            LoadData();
        }
        private void OnDisable()
        {
            SaveData();
        }
        private void OnApplicationQuit()
        {
            SaveData();
        }
        private void OnApplicationPause(bool pause)
        {
            if (pause == true)
                SaveData();
            else
                LoadData();
        }

        private void SaveData()
        {
            if (_dataForSettings == null)
                _dataForSettings = new DataForSettings();

            _dataForSettings.GlobalMusicVolume = _dataForSettingsSO.GetGlobalMusicVolume();
            _dataForSettings.MusicVolume = _dataForSettingsSO.GetMusicVolume();
            _dataForSettings.SoundVolume = _dataForSettingsSO.GetSoundVolume();
            _dataForSettings.SensitivityValue = _dataForSettingsSO.GetSensitivityValue();

            DataSaveEditor.Save(_dataForSettings, _namePath);

            Debug.Log("Save");
        }
        private void LoadData()
        {
            _dataForSettings = DataSaveEditor.GetData<DataForSettings>(_namePath);

            if (_dataForSettings == null)
                return;

            _dataForSettingsSO.SetGlobalMusicVolume(_dataForSettings.GlobalMusicVolume);
            _dataForSettingsSO.SetMusicVolume(_dataForSettings.MusicVolume);
            _dataForSettingsSO.SetSoundVolume(_dataForSettings.SoundVolume);
            _dataForSettingsSO.SetSensitivity(_dataForSettings.SensitivityValue);

            Debug.Log("Load");
        }
    }
}
