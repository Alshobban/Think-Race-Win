using System;
using System.Threading;
using Car;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class PitstopController : MonoBehaviour
    {
        [SerializeField]
        private QuizController quizController;

        [SerializeField]
        private TriggerCallbacks pitstopTrigger;

        [SerializeField]
        private Transform pitstopPosition;

        [SerializeField]
        private Transform pitstopEndPosition;

        [SerializeField]
        private float moveToPitstopInterval = 2f;

        [SerializeField]
        private float quizTime = 15f;

        [SerializeField]
        private Slider timeSlider;

        private TweenerCore<float, float, FloatOptions> _sliderTween;
        private bool _inProgress;
        private float _finalQuizTime;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                quizTime--;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                quizTime++;
            }
        }

        private void FixedUpdate()
        {
            if (_inProgress)
            {
                var sliderStepPerFixedUpdate =
                    (timeSlider.maxValue - timeSlider.minValue) * Time.fixedDeltaTime / _finalQuizTime;
                timeSlider.value += sliderStepPerFixedUpdate;

                if (timeSlider.value >= timeSlider.maxValue)
                {
                    _inProgress = false;
                }
            }
        }

        private void Start()
        {
            timeSlider.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            pitstopTrigger.TriggerEntered += OnEnteredPitstop;
        }

        private void OnDisable()
        {
            pitstopTrigger.TriggerEntered -= OnEnteredPitstop;
        }

        private void OnAnsweredQuestion(bool correct)
        {
            if (correct)
            {
                _finalQuizTime -= 5f;
            }
            else
            {
                _finalQuizTime += 5f;
            }
        }

        private async void OnEnteredPitstop(Collider car)
        {
            car.GetComponentInParent<CarMovementView>().SetControls(false);
            car.attachedRigidbody.DOMove(pitstopPosition.position, moveToPitstopInterval);

            await UniTask.Delay(TimeSpan.FromSeconds(moveToPitstopInterval));

            quizController.ShowQuiz();
            timeSlider.value = timeSlider.minValue;
            timeSlider.gameObject.SetActive(true);

            _finalQuizTime = quizTime;
            quizController.AnsweredCorrectly += OnAnsweredQuestion;

            _inProgress = true;
            await UniTask.WaitUntil(() => _inProgress == false).WithCancellation(this.GetCancellationTokenOnDestroy())
                .SuppressCancellationThrow();

            quizController.AnsweredCorrectly -= OnAnsweredQuestion;
            timeSlider.gameObject.SetActive(false);
            quizController.HideQuiz();
            car.attachedRigidbody.DOMove(pitstopEndPosition.position, moveToPitstopInterval);
            await UniTask.Delay(TimeSpan.FromSeconds(moveToPitstopInterval));

            car.GetComponentInParent<CarMovementView>().SetControls(true);
        }
    }
}