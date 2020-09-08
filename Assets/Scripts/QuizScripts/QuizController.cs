using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    private List<Question> questionList = new List<Question>();
    public Text questionText;
    public GameObject[] answersOptions; //= new GameObject[];
    public Question questionObj;

    private int randomIndex;

    public void ShowQuiz()
    {
        InitQuestions();
        GetComponent<Canvas>().enabled = true;
    }

    public void HideQuiz()
    {
        GetComponent<Canvas>().enabled = false;
    }

    // Start is called before the first frame update
    private void InitQuestions()
    {
        //fillin the question list!
        FillInQuestions(questionList);
        //Getting all the answers buttons on the scene!
        answersOptions = GameObject.FindGameObjectsWithTag("AnswerText");
        //defining the question from the four questions,its answers and re ordring the answers buttons
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
            if (questionList.Count > 1)
            {
                //removing the last question from the list!
                SortingQustions(questionList);
                //defining the question from the four questions,its answers and re ordring the answers buttons
                Invoke("DefiningQuestion", 2f);
            }

            //re-assigninig the static value!
            CheckingAnswers.clicked = false;
        }
    }

    //fillin the questions list!
    private void FillInQuestions(List<Question> questions)
    {
        questions.Add(new Question("What's the capital of Norway ?", "Oslo", "Trondheim", "Bergen", "Stavanger"));
        questions.Add(new Question("What's the name of the first-ever videogame ?", "Tennis for two",
            "Tetris (Nintendo)", "Duck Hunt", "Super Mario World"));
        questions.Add(new Question("What's the best selling video game of all time ?", "Minecraft", "Pac-Man",
            "Super Mario Bros.", "Grand Theft Auto V"));
        questions.Add(new Question("Who discovered penicillin ?", "Alexander Fleming", "Louis Pasteur", "Robert Koch",
            "Joseph Lister"));
    }

    //Fill in buttons text with answers related to the question selected !
    private void FillInAnswers(Question question, GameObject[] answers)
    {
        answers[0].gameObject.GetComponent<Text>().text = question.correctAnswer.ToString();
        answers[1].gameObject.GetComponent<Text>().text = question.UncorrectAnswer1.ToString();
        answers[2].gameObject.GetComponent<Text>().text = question.UncorrectAnswer2.ToString();
        answers[3].gameObject.GetComponent<Text>().text = question.UncorrectAnswer3.ToString();
    }

    //resorting method !
    private void ResortingAnswers(GameObject[] answers)
    {
        //help variable!
        Vector3 pos;
        //int random2 = Random.Range(0, answers.Length);
        for (int i = 0; i < 3; i++)
        {
            int random1 = Random.Range(0, answers.Length);
            pos = answers[random1].transform.parent.position;
            answers[random1].transform.parent.position = answers[0].transform.position;
            answers[0].transform.parent.position = pos;
        }
    }

    //resorting questions list by removing the previous question!
    private void SortingQustions(List<Question> questions)
    {
        questions.RemoveAt(randomIndex);
    }

    // changing wrong answers color and diabling the button interactivness!
    private void ChangingColor(GameObject[] answers)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i].gameObject.GetComponent<Text>().text != questionObj.correctAnswer)
            {
                answers[i].transform.parent.GetComponent<Image>().color = Color.red;
                answers[i].transform.parent.GetComponent<Button>().enabled = false;
            }
        }
    }

    // reset Colors color and enabling the buttons interactivness!
    private void ResetColor(GameObject[] answers)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].transform.parent.GetComponent<Image>().color = Color.white;
            answers[i].transform.parent.GetComponent<Button>().enabled = true;
        }
    }

    //Methode for defining questionand it answers ()!
    private void DefiningQuestion()
    {
        //getting a random question !
        randomIndex = UnityEngine.Random.Range(0, questionList.Count);
        questionObj = questionList[randomIndex];
        questionText.text = questionObj.questionName.ToString();
        FillInAnswers(questionObj, answersOptions);
        //calling resorting Method!
        ResortingAnswers(answersOptions);
        //reset Answers color!
        ResetColor(answersOptions);
    }
}