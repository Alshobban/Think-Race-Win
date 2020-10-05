using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    [RequireComponent(typeof(Slider))]
    public class DisableFillOnMinValue : MonoBehaviour
    {
        private Slider _slider;

        private void OnValueChanged(float newValue)
        {
            if (_slider.minValue == newValue)
            {
                _slider.fillRect.gameObject.SetActive(false);
            }
            else if (!_slider.fillRect.gameObject.activeSelf)
            {
                _slider.fillRect.gameObject.SetActive(true);
            }
        }

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            OnValueChanged(_slider.value);
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }
    }
}