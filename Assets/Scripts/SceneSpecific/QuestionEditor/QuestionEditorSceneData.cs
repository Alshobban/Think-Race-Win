using UnityEngine;
using Utilities;

namespace SceneSpecific.QuestionEditor
{
    public class QuestionEditorSceneData : SceneData<QuestionEditorSceneData>
    {
        [field: SerializeField]
        public QuestionPackListUiController QuestionPackListUiController { get; private set; }

        [field: SerializeField]
        public QuestionPackEditorUiController QuestionPackEditorUiController { get; private set; }
    }
}