using System.IO;
using UnityEngine;

namespace Questions
{
    public static class QuestionPackLoader
    {
        private const string QuestionPacksPrefsName = "QuestionPacks";

        public static QuestionPack[] QuestionPacks
        {
            get => _questionPacks ?? (_questionPacks = LoadSavedQuestionPacks());

            set
            {
                SaveQuestionPacks(value);
                _questionPacks = LoadSavedQuestionPacks();
            }
        }

        private static QuestionPack[] _questionPacks;

        private static QuestionPack[] LoadSavedQuestionPacks()
        {
            var loadSavedQuestionPacks = JsonUtility
                .FromJson<QuestionPackPack>(PlayerPrefs.GetString(QuestionPacksPrefsName)).QuestionPacks;
            
            if (loadSavedQuestionPacks == null)
            {
                return new QuestionPack[0];
            }

            return loadSavedQuestionPacks;
        }

        private static void SaveQuestionPacks(params QuestionPack[] questionPacks)
        {
            var questionPackPack = new QuestionPackPack(questionPacks);

            var data = JsonUtility.ToJson(questionPackPack);
            Debug.Log($"Question packs' saved size is {data.Length * sizeof(char)} bytes");

            PlayerPrefs.SetString(QuestionPacksPrefsName, data);
        }
    }
}