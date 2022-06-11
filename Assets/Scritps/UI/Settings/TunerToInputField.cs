using UnityEngine;
using UnityEngine.UI;
using DungeonEternal.Settings;

namespace DungeonEternal.UI
{
    public class TunerToInputField : MonoBehaviour
    {
        [SerializeField] private DataForSettingsSO _dataForSettingsSO;

        [Space]
        [SerializeField] private TypeSettingValue _typeSettingValue;

        private InputField _inputField;

        private void OnEnable()
        {
            _inputField = GetComponent<InputField>();
            _inputField.text = _dataForSettingsSO.GetSettingValue(_typeSettingValue).ToString();
        }
    }
}
