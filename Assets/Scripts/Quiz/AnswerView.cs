using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Quiz
{
    public class AnswerView : MonoBehaviour
    {
        public event Action<string> AnswerChosen;

        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                answerButton.interactable = value;
            }
        }

        [SerializeField]
        private float hotkeyAnswerDelay;

        [SerializeField]
        private KeyCode hotkey;

        [SerializeField]
        private Image background;

        [SerializeField]
        private Slider hotkeyProgressSlider;

        [SerializeField]
        private Button answerButton;

        [SerializeField]
        private TextMeshProUGUI answerText;

        private bool _holdingButton;
        private bool _interactable;
        private string _currentAnswer;
        private float _hotkeyTimer;

        public string CurrentAnswer
        {
            get => _currentAnswer;
            set
            {
                _currentAnswer = value;
                answerText.text = value;
            }
        }

        public void SetBackgroundColor(Color color)
        {
            var answerButtonColors = answerButton.colors;
            answerButtonColors.disabledColor = color;
            answerButton.colors = answerButtonColors;
        }

        private void Update()
        {
            if (Interactable && (Input.GetKey(hotkey) || _holdingButton))
            {
                _hotkeyTimer += Time.deltaTime;
                hotkeyProgressSlider.value = _hotkeyTimer;

                if (_hotkeyTimer >= hotkeyAnswerDelay)
                {
                    AnswerChosen?.Invoke(CurrentAnswer);
                    ResetTimer();
                }
            }

            if (Input.GetKeyUp(hotkey))
            {
                ResetTimer();
            }
        }

        private void ResetTimer()
        {
            hotkeyProgressSlider.value = _hotkeyTimer = 0f;
        }

        private void Awake()
        {
            ResetTimer();
            hotkeyProgressSlider.maxValue = hotkeyAnswerDelay;
        }

        private void OnEnable()
        {
            answerButton.onClick.AddListener(OnAnswerButtonClicked);
        }

        private void OnDisable()
        {
            answerButton.onClick.RemoveListener(OnAnswerButtonClicked);
        }

        public void OnPointerDown()
        {
            _holdingButton = true;
        }

        public void OnPointerUp()
        {
            _holdingButton = false;
            _hotkeyTimer = 0f;
        }

        private void OnAnswerButtonClicked()
        {
            // AnswerChosen?.Invoke(CurrentAnswer);
        }
    }
}