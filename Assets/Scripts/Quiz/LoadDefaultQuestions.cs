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
                .Union(defaultPacks.Where(t => !QuestionPackLoader.QuestionPacks.Any(q => q.Equals(t.QuestionPack)))
                    .Select(t => t.QuestionPack)).ToArray();
        }
    }
}