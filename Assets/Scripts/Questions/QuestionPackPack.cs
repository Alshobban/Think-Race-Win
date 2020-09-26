using System;
using UnityEngine;

namespace Questions
{
    [Serializable]
    public class QuestionPackPack
    {
        [field: SerializeField]
        public QuestionPack[] QuestionPacks { get; private set; }

        public QuestionPackPack()
        {
        }

        public QuestionPackPack(QuestionPack[] questionPacks)
        {
            QuestionPacks = questionPacks;
        }
    }
}