using Quiz;
using TMPro;
using UnityEngine;

namespace SceneSpecific.QuestionEditor
{
    public class QuestionView : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField questionText;

        [SerializeField]
        private TMP_InputField correctAnswer;

        [SerializeField]
        private TMP_InputField incorrectAnswer1;

        [SerializeField]
        private TMP_InputField incorrectAnswer2;

        [SerializeField]
        private TMP_InputField incorrectAnswer3;

        public Question GetQuestion()
        {
            return new Question(questionText.text, correctAnswer.text, incorrectAnswer1.text, incorrectAnswer2.text,
                incorrectAnswer3.text);
        }

        public void SetQuestion(Question question)
        {
            questionText.text = question.QuestionText;
            correctAnswer.text = question.CorrectAnswer;
            incorrectAnswer1.text = question.IncorrectAnswer1;
            incorrectAnswer2.text = question.IncorrectAnswer2;
            incorrectAnswer3.text = question.IncorrectAnswer3;
        }
    }
}