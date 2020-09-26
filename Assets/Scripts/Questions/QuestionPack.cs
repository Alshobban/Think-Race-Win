using System;
using System.Collections.Generic;
using System.Linq;
using Quiz;
using UnityEngine;

namespace Questions
{
    [Serializable]
    public class QuestionPack
    {
        [field: SerializeField]
        public string PackName { get; private set; }

        [field: SerializeField]
        public List<Question> Questions { get; private set; }

        public QuestionPack(IEnumerable<Question> questions)
        {
            Questions = questions.ToList();
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public void RemoveQuestion(Question question)
        {
            Questions.Remove(question);
        }
    }
}