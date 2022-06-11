using UnityEngine;
using UnityEngine.UI;

namespace DungeonEternal.UI
{
    public class EqualizerToSlider : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void EqualizeText(InputField text)
        {
            text.text = _slider.normalizedValue.ToString();
        }
    }
}
