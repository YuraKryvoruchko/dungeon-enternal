using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonEternal.UI
{
    public class EqualizerToInputField : MonoBehaviour
    {
        private InputField _inputField;

        private void Awake()
        {
            _inputField = GetComponent<InputField>();
        }

        public void EqualizeSliderValue(Slider slider)
        {
            CultureInfo dot = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            dot.NumberFormat.CurrencyDecimalSeparator = ".";

            float value = float.Parse(_inputField.text, NumberStyles.Any, dot);

            slider.normalizedValue = value;
        }
    }
}
