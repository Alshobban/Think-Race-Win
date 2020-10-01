using System.Linq;
using Questions;
using UnityEngine;
using UnityEngine.UI;

namespace SceneSpecific.QuestionEditor
{
    public class QuestionPackListUiController : MonoBehaviour
    {
        [SerializeField]
        private GameObject questionPackListMenu;

        [SerializeField]
        private QuestionPackListController questionPackListController;

        [SerializeField]
        private Button addPackButton;

        [SerializeField]
        private Button editPackButton;

        [SerializeField]
        private Button removePackButton;

        private void OnEnable()
        {
            addPackButton.onClick.AddListener(OnAddPackClicked);
            editPackButton.onClick.AddListener(OnEditPackClicked);
            removePackButton.onClick.AddListener(OnRemovePackClicked);
        }

        private void OnDisable()
        {
            addPackButton.onClick.RemoveListener(OnAddPackClicked);
            editPackButton.onClick.RemoveListener(OnEditPackClicked);
            removePackButton.onClick.RemoveListener(OnRemovePackClicked);
        }

        public void ShowAndReload()
        {
            questionPackListMenu.SetActive(true);
            questionPackListController.AddSavedQuestionPacks();
        }

        private void OnRemovePackClicked()
        {
            var selected = questionPackListController.GetSelected;

            if (selected != null)
            {
                var questionPacksExceptSelected =
                    QuestionPackLoader.QuestionPacks.Where(t => !t.Equals(selected)).ToArray();

                QuestionPackLoader.QuestionPacks = questionPacksExceptSelected;
                questionPackListController.RemoveLine(selected);
            }
        }

        private void OnEditPackClicked()
        {
            QuestionEditorSceneData.Instance.QuestionPackEditorUiController.OpenQuestionPack(questionPackListController
                .GetSelected);

            questionPackListMenu.SetActive(false);
        }

        private void OnAddPackClicked()
        {
            questionPackListMenu.SetActive(false);

            QuestionEditorSceneData.Instance.QuestionPackEditorUiController.OpenNewQuestionPack();
        }
    }
}