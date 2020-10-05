using System;
using System.Collections.Generic;
using System.Linq;
using SceneSpecific.Game;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

namespace Quiz
{
    public class QuizController : MonoBehaviour
    {
        public GameObject questionText;
        public GameObject[] answersOptions; //= new GameObject[];
        public Question questionObj;
        public Sprite wrongAnswerSprite, normalSprite;

        private List<Question> _questionList = new List<Question>();

        private int _randomIndex;

        public void ShowQuiz()
        {
            InitQuestions();
            GetComponent<Canvas>().enabled = true;
        }

        public void HideQuiz()
        {
            GetComponent<Canvas>().enabled = false;
        }

        private void Start()
        {
            HideQuiz();
        }

        // Start is called before the first frame update
        private void InitQuestions()
        {
            //fill in the question list!
            _questionList = GameData.CurrentQuestionPack.Questions.ToList();
            //Getting all the answers buttons on the scene!
            answersOptions = GameObject.FindGameObjectsWithTag("AnswerText");
            //defining the question from the four questions,its answers and re ordering the answers buttons
            DefiningQuestion();
        }

        // Update is called once per frame
        void Update()
        {
            //checking for click event!
            if (CheckingAnswers.clicked)
            {
                //changing the wrong answers colors!
                ChangingColor(answersOptions);

                //Checking for questions availability!
                if (_questionList.Count > 1)
                {
                    //removing the last question from the list!
                    SortingQuestions(_questionList);
                    //defining the question from the four questions,its answers and re ordering the answers buttons
                    Invoke(nameof(DefiningQuestion), 2f);
                }

                //re-assigning the static value!
                CheckingAnswers.clicked = false;
            }
        }

        //Fill in buttons text with answers related to the question selected !
        private void FillInAnswers(Question question, GameObject[] answers)
        {
            answers[0].gameObject.GetComponent<TextMeshProUGUI>().text = question.CorrectAnswer;
            answers[2].gameObject.GetComponent<TextMeshProUGUI>().text = question.IncorrectAnswer2;
            answers[3].gameObject.GetComponent<TextMeshProUGUI>().text = question.IncorrectAnswer3;
            answers[1].gameObject.GetComponent<TextMeshProUGUI>().text = question.IncorrectAnswer1;
        }

        //resorting method !
        private void ResortingAnswers(GameObject[] answers)
        {
            //help variable!
            //int random2 = Random.Range(0, answers.Length);
            for (int i = 0; i < 3; i++)
            {
                int random1 = Random.Range(0, answers.Length);
                var pos = answers[random1].transform.parent.position;
                answers[random1].transform.parent.position = answers[0].transform.position;
                answers[0].transform.parent.position = pos;
            }
        }

        //resorting questions list by removing the previous question!
        private void SortingQuestions(List<Question> questions)
        {
            questions.RemoveAt(_randomIndex);
        }

        // changing wrong answers color and disabling the button interactiveness!
        private void ChangingColor(GameObject[] answers)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].transform.parent.GetComponent<Button>().interactable = false;
                if (answers[i].gameObject.GetComponent<TextMeshProUGUI>().text != questionObj.CorrectAnswer)
                {
                    // answers[i].transform.parent.GetComponent<Image>().color = Color.red;
                    answers[i].transform.parent.GetComponent<Image>().sprite = wrongAnswerSprite;
                    //answers[i].transform.parent.GetComponent<Button>().enabled = false;
                   // answers[i].transform.parent.GetComponent<Button>().interactable = false;
                }
            }
        }

        // reset Colors color and enabling the buttons interactiveness!
        private void ResetColor(GameObject[] answers)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                // answers[i].transform.parent.GetComponent<Image>().color = Color.white;
                answers[i].transform.parent.GetComponent<Image>().sprite = normalSprite;
                // answers[i].transform.parent.GetComponent<Button>().enabled = true;
                answers[i].transform.parent.GetComponent<Button>().interactable = true;
            }
        }

        //Methode for defining question and it answers ()!
        private void DefiningQuestion()
        {
            //getting a random question !
            _randomIndex = Random.Range(0, _questionList.Count);
            questionObj = _questionList[_randomIndex];
            questionText.GetComponent<TextMeshProUGUI>().text = questionObj.QuestionText;
            FillInAnswers(questionObj, answersOptions);
            //calling resorting Method!
            ResortingAnswers(answersOptions);
            //reset Answers color!
            ResetColor(answersOptions);
        }
    }
}