using UnityEngine;
using Utilities;

namespace SceneSpecific.QuestionEditor
{
    public class QuestionEditorSceneData : SceneData<QuestionEditorSceneData>
    {
        [field: SerializeField]
        public QuestionPackListController QuestionPackListController { get; private set; }
    }
}