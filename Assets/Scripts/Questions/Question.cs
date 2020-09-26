using System;
using UnityEngine;

namespace Quiz
{
    [Serializable]
    public struct Question
    {
        [field: SerializeField]
        public string QuestionText { get; private set; }

        [field: SerializeField]
        public string CorrectAnswer { get; private set; }

        [field: SerializeField]
        public string IncorrectAnswer1 { get; private set; }

        [field: SerializeField]
        public string IncorrectAnswer2 { get; private set; }

        [field: SerializeField]
        public string IncorrectAnswer3 { get; private set; }

        public Question(string questionText, string correctAnswer, string ans1, string ans2, string ans3)
        {
            QuestionText = questionText;
            CorrectAnswer = correctAnswer;
            IncorrectAnswer1 = ans1;
            IncorrectAnswer2 = ans2;
            IncorrectAnswer3 = ans3;
        }
    }
}