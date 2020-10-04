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
            QuestionEditorSceneData.Instance.QuestionPackListUiController.ShowAndReload();
        }
    }
}