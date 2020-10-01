using System.Linq;
using Questions;
using UnityEngine;

namespace Quiz
{
    public static class LoadDefaultQuestions
    {
        [RuntimeInitializeOnLoadMethod]
        private static void LoadDefaultQuestionPack()
        {
            var defaultPack = Resources.Load<QuestionPackScriptableObject>("Questions/QuestionPack").QuestionPack;

            if (!QuestionPackLoader.QuestionPacks.Contains(defaultPack))
            {
                QuestionPackLoader.QuestionPacks = QuestionPackLoader.QuestionPacks.Append(defaultPack).ToArray();
            }
        }
    }
}