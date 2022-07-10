using System;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using DungeonEternal.Settings;

namespace DungeonEternal.UI
{
    public class ValueEditor : MonoBehaviour
    {
        [SerializeField] private DataForSettingsSO _dataForSetting;

        public static event Action<float> OnChangeSensitivity;
        public static event Action<float> OnChangeMusicVolume;
        public static event Action<float> OnChangeSoundVolume;
        public static event Action<float> OnChangeGeneralVolume;

        public void SetSensitivity(InputField inputFieldValue)
        {
            CultureInfo dot = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            dot.NumberFormat.CurrencyDecimalSeparator = ".";

            float value = float.Parse(inputFieldValue.text, NumberStyles.Any, dot);

            _dataForSetting.SetSensitivity(value);

            OnChangeSensitivity?.Invoke(value);
        }
        public void SetMusicVolume(InputField inputFieldValue)
        {
            CultureInfo dot = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            dot.NumberFormat.CurrencyDecimalSeparator = ".";

            float value = float.Parse(inputFieldValue.text, NumberStyles.Any, dot);

            _dataForSetting.SetMusicVolume(value);

            OnChangeMusicVolume?.Invoke(value);
        }
        public void SetGeneralVolume(InputField inputFieldValue)
        {
            CultureInfo dot = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            dot.NumberFormat.CurrencyDecimalSeparator = ".";

            float value = float.Parse(inputFieldValue.text, NumberStyles.Any, dot);

            _dataForSetting.SetGlobalMusicVolume(value);

            OnChangeGeneralVolume?.Invoke(value);
        }
        public void SetSoundVolume(InputField inputFieldValue)
        {
            CultureInfo dot = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            dot.NumberFormat.CurrencyDecimalSeparator = ".";

            float value = float.Parse(inputFieldValue.text, NumberStyles.Any, dot);

            _dataForSetting.SetSoundVolume(value);

            OnChangeSoundVolume?.Invoke(value);
        }
        public void SetSensitivity(Slider sliderValue)
        {
            float value = sliderValue.value;

            _dataForSetting.SetSensitivity(value);

            OnChangeSensitivity?.Invoke(value);
        }
        public void SetMusicVolume(Slider sliderValue)
        {
            float value = sliderValue.value;

            _dataForSetting.SetMusicVolume(value);

            OnChangeMusicVolume?.Invoke(value);
        }
        public void SetGeneralVolume(Slider sliderValue)
        {
            float value = sliderValue.value;

            _dataForSetting.SetGlobalMusicVolume(value);

            OnChangeGeneralVolume?.Invoke(value);
        }
        public void SetSoundVolume(Slider sliderValue)
        {
            float value = sliderValue.value;

            _dataForSetting.SetSoundVolume(value);

            OnChangeSoundVolume?.Invoke(value);
        }
    }
}
