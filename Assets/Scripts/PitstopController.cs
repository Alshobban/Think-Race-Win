using Car;
using DG.Tweening;
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

    [SerializeField]
    private CarMovement carMovement;

    [SerializeField]
    private SteerRotate carSteering;


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

    private void SetControls(bool isEnabled)
    {
        carMovement.enabled = isEnabled;
        carSteering.enabled = isEnabled;
    }

    private void OnEnteredPitstop(Collider car)
    {
        if (car.CompareTag("Player"))
        {
            DOTween.Sequence()
                .AppendCallback(() => SetControls(false))
                .Append(car.attachedRigidbody.DOMove(pitstopPosition.position, moveToPitstopInterval))
                .AppendCallback(() =>
                {
                    quizController.ShowQuiz();
                    timeSlider.value = timeSlider.minValue;
                    timeSlider.gameObject.SetActive(true);
                })
                .AppendInterval(quizTime)
                .Join(timeSlider.DOValue(timeSlider.maxValue, quizTime))
                .AppendCallback(() =>
                {
                    timeSlider.gameObject.SetActive(false);
                    quizController.HideQuiz();
                })
                .Append(car.attachedRigidbody.DOMove(pitstopEndPosition.position, moveToPitstopInterval))
                .AppendCallback(() => SetControls(true));
        }
    }
}