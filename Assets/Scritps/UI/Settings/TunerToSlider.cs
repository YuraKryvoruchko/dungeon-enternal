using UnityEngine;
using UnityEngine.UI;
using DungeonEternal.Settings;

namespace DungeonEternal.UI
{
    public class TunerToSlider : MonoBehaviour
    {
        [SerializeField] private DataForSettingsSO _dataForSettingsSO;

        [Space]
        [SerializeField] private TypeSettingValue _typeSettingValue;

        private Slider _slider;

        private void OnEnable()
        {
            _slider = GetComponent<Slider>();   
            _slider.value = _dataForSettingsSO.GetSettingValue(_typeSettingValue);
        }
    }
}
