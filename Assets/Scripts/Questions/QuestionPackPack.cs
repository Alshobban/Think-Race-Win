using System;
using UnityEngine;

namespace Questions
{
    [Serializable]
    public class QuestionPackPack
    {
        [SerializeField]
        private QuestionPack[] p;

        public QuestionPack[] QuestionPacks => p;

        public QuestionPackPack()
        {
        }

        public QuestionPackPack(QuestionPack[] questionPacks)
        {
            this.p = questionPacks;
        }
    }
}