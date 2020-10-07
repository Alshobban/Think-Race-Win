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
            var defaultPacks = Resources.LoadAll<QuestionPackScriptableObject>("Questions/");

            QuestionPackLoader.QuestionPacks = QuestionPackLoader.QuestionPacks
                .Union(defaultPacks.Select(t => t.QuestionPack)).ToArray();
        }
    }
}