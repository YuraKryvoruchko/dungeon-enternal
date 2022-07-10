using UnityEngine;
using StarterAssets;
using DungeonEternal.Settings;

namespace DungeonEternal.UI
{
    public class PlayerSensitivityEditor : MonoBehaviour
    {
        [SerializeField] private DataForSettingsSO _dataForSetting;

        private FirstPersonController _firstPerson;

        private void Awake()
        {
            _firstPerson = FindObjectOfType<FirstPersonController>();

            if(_firstPerson != null)
                _firstPerson.RotationSpeed = _dataForSetting.GetSensitivityValue();
        }

        private void OnEnable()
        { 
            ValueEditor.OnChangeSensitivity += SetSensitivity; 
        }
        private void OnDisable()
        {
            ValueEditor.OnChangeSensitivity -= SetSensitivity;
        }

        private void SetSensitivity(float value)
        {
            if (_firstPerson != null)
            {
                if (value >= 0)
                    _firstPerson.RotationSpeed = value;
            }
        }
    }
}
