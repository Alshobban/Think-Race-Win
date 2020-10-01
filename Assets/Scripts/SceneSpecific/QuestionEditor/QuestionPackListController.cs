using Questions;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace SceneSpecific.QuestionEditor
{
    public class QuestionPackListController : ScrollListController<QuestionPack>
    {
        [SerializeField]
        public ToggleGroup toggleGroup;

        public QuestionPack GetSelected => Objects[toggleGroup.GetFirstActiveToggle().gameObject];

        public void AddSavedQuestionPacks()
        {
            RemoveAll();
            foreach (var questionPack in QuestionPackLoader.QuestionPacks)
            {
                AddLine(questionPack);
            }
        }

        private void OnEnable()
        {
            LineAdded += OnLineAdded;
        }

        private void OnDisable()
        {
            LineAdded -= OnLineAdded;
        }

        private void OnLineAdded(GameObject obj)
        {
            obj.GetComponentInChildren<Toggle>().group = toggleGroup;
        }

        protected override string GetLineText(QuestionPack sourceObject)
        {
            return sourceObject.PackName;
        }
    }
}