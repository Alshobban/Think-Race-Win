using System.Collections.Generic;
using Questions;
using UnityEngine;

namespace SceneSpecific.QuestionEditor
{
    public class QuestionEditorSceneController : MonoBehaviour
    {
        [SerializeField]
        private QuestionPackScriptableObject questionPack;

        private IEnumerable<QuestionPack> _questionPacks;

        private void Awake()
        {
            QuestionPackLoader.SaveQuestionPacks(questionPack.QuestionPack);

            _questionPacks = QuestionPackLoader.LoadSavedQuestionPacks();

            foreach (var pack in _questionPacks)
            {
                QuestionEditorSceneData.Instance.QuestionPackListController.AddLine(pack);
            }
        }
    }
}