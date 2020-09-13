using System;

namespace Quiz
{
    [Serializable]
    public struct Question
    {
        public string QuestionName { get; private set; }
        public string CorrectAnswer { get; private set; }
        public string IncorrectAnswer1 { get; private set; }
        public string IncorrectAnswer2 { get; private set; }
        public string IncorrectAnswer3 { get; private set; }

        public Question(string qName, string cAns, string ans1, string ans2, string ans3)
        {
            QuestionName = qName;
            CorrectAnswer = cAns;
            IncorrectAnswer1 = ans1;
            IncorrectAnswer2 = ans2;
            IncorrectAnswer3 = ans3;
        }
    }
}