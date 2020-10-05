using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using SceneSpecific.Game;
using UnityEngine;
using TMPro;
using Utilities;

namespace Quiz
{
    public class QuizController : MonoBehaviour
    {
        public event Action<bool> AnsweredCorrectly;

        [SerializeField]
        private GameObject questionText;

        [SerializeField]
        private AnswerView[] answerViews;

        [SerializeField]
        private float afterClickDelay;

        [SerializeField]
        private Color normalColor;

        [SerializeField]
        private Color correctAnswerColor;

        [SerializeField]
        private Color incorrectAnswerColor;

        private CancellationTokenSource _cancelQuiz;

        private Question _currentQuestion;

        private readonly Queue<Question> _questionList = new Queue<Question>();

        private Question GetNextQuestion()
        {
            if (_questionList.Count < 1)
            {
                foreach (var question in GameData.CurrentQuestionPack.Questions.Shuffle())
                {
                    _questionList.Enqueue(question);
                }
            }

            return _questionList.Dequeue();
        }

        public void ShowQuiz()
        {
            _cancelQuiz = new CancellationTokenSource();

            SetQuestion(GetNextQuestion());
            gameObject.SetActive(true);
        }

        public void HideQuiz()
        {
            _cancelQuiz?.Cancel();
            _cancelQuiz?.Dispose();

            gameObject.SetActive(false);
        }

        private void Awake()
        {
            HideQuiz();
        }

        private void OnEnable()
        {
            foreach (var answerView in answerViews)
            {
                answerView.AnswerChosen += OnAnswerChosen;
            }
        }

        private void OnDisable()
        {
            foreach (var answerView in answerViews)
            {
                answerView.AnswerChosen -= OnAnswerChosen;
            }
        }

        private async void OnAnswerChosen(string answer)
        {
            foreach (var answerView in answerViews)
            {
                answerView.Interactable = false;
            }

            var chosenAnswer = answerViews.First(t => t.CurrentAnswer == answer);

            if (chosenAnswer.CurrentAnswer == _currentQuestion.CorrectAnswer)
            {
                chosenAnswer.SetBackgroundColor(correctAnswerColor);
                AnsweredCorrectly?.Invoke(true);
            }
            else
            {
                chosenAnswer.SetBackgroundColor(incorrectAnswerColor);
                AnsweredCorrectly?.Invoke(false);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(afterClickDelay)).WithCancellation(_cancelQuiz.Token)
                .SuppressCancellationThrow();

            SetQuestion(GetNextQuestion());
        }

        private void SetQuestion(Question question)
        {
            // for (int i = 0; i < answers.Length; i++)
            // {
            //     answers[i].transform.parent.GetComponent<Button>().interactable = false;
            //     if (answers[i].gameObject.GetComponent<TextMeshProUGUI>().text != questionObj.CorrectAnswer)
            //     {
            //         answers[i].transform.parent.GetComponent<Image>().sprite = wrongAnswerSprite;
            //     }
            // }
            _currentQuestion = question;
            ResetButtons();
            questionText.GetComponent<TextMeshProUGUI>().text = question.QuestionText;
            FillInAnswers(question);
        }

        private void ResetButtons()
        {
            foreach (var answerView in answerViews)
            {
                answerView.SetBackgroundColor(normalColor);
                answerView.Interactable = true;
            }
        }

        private void FillInAnswers(Question question)
        {
            answerViews.Shuffle();
            answerViews[0].CurrentAnswer = question.CorrectAnswer;
            answerViews[1].CurrentAnswer = question.IncorrectAnswer1;
            answerViews[2].CurrentAnswer = question.IncorrectAnswer2;
            answerViews[3].CurrentAnswer = question.IncorrectAnswer3;
        }
    }
}