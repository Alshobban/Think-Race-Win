using System.Collections.Generic;
using UnityEngine;

namespace Questions
{
    public static class QuestionPackLoader
    {
        public const string QuestionPacksPrefsName = "QuestionPacks";

        public static IEnumerable<QuestionPack> LoadSavedQuestionPacks()
        {
            return JsonUtility.FromJson<QuestionPackPack>(PlayerPrefs.GetString(QuestionPacksPrefsName)).QuestionPacks;
        }

        public static void SaveQuestionPacks(params QuestionPack[] questionPacks)
        {
            var questionPackPack = new QuestionPackPack(questionPacks);

            var data = JsonUtility.ToJson(questionPackPack);
            Debug.Log($"Question packs' saved size is {data.Length * sizeof(char)} bytes");

            PlayerPrefs.SetString(QuestionPacksPrefsName, data);
        }
    }
}