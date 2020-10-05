using System;
using Car;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Photon.Pun;
using Quiz;
using UnityEngine;
using UnityEngine.UI;

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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // _sliderTween?.ChangeEndValue(_sliderTween.endValue, quizTime--, );
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
        pitstopTrigger.TriggerEntered += OnEnteredPitstop;
    }

    private async void OnEnteredPitstop(Collider car)
    {
        car.GetComponentInParent<CarMovementView>().SetControls(false);
        car.attachedRigidbody.DOMove(pitstopPosition.position, moveToPitstopInterval);

        await UniTask.Delay(TimeSpan.FromSeconds(moveToPitstopInterval));

        quizController.ShowQuiz();
        timeSlider.value = timeSlider.minValue;
        timeSlider.gameObject.SetActive(true);

        _sliderTween = timeSlider.DOValue(timeSlider.maxValue, quizTime).OnComplete(() => AfterQuiz(car));
    }

    private void AfterQuiz(Collider car)
    {
        timeSlider.gameObject.SetActive(false);
        quizController.HideQuiz();
        car.attachedRigidbody.DOMove(pitstopEndPosition.position, moveToPitstopInterval);
        car.GetComponentInParent<CarMovementView>().SetControls(true);
    }
}