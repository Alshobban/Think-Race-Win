using System.Linq;
using Questions;
using UnityEngine;

namespace SceneSpecific.QuestionEditor
{
    public class QuestionEditorSceneController : MonoBehaviour
    {
        [SerializeField]
        private QuestionPackScriptableObject questionPack;

        private void Awake()
        {
            QuestionPackLoader.QuestionPacks =
                QuestionPackLoader.QuestionPacks.Append(questionPack.QuestionPack).ToArray();

            QuestionEditorSceneData.Instance.QuestionPackListUiController.ShowAndReload();
        }
    }
}