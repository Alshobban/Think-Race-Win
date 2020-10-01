using System;
using System.Linq;
using Questions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SceneSpecific.QuestionEditor
{
    public class QuestionPackEditorUiController : MonoBehaviour
    {
        [SerializeField]
        private GameObject questionPackEditorMenu;

        [SerializeField]
        private QuestionListController questionsList;

        [SerializeField]
        private TMP_InputField questionPackName;

        [SerializeField]
        private Button leaveButton;

        [SerializeField]
        private Button saveAndLeaveButton;

        private QuestionPack _editedQuestionPack;

        private void OnEnable()
        {
            leaveButton.onClick.AddListener(OnLeaveButtonClicked);
            saveAndLeaveButton.onClick.AddListener(OnSaveAndLeaveButtonClicked);
        }

        private void OnDisable()
        {
            leaveButton.onClick.RemoveListener(OnLeaveButtonClicked);
            saveAndLeaveButton.onClick.RemoveListener(OnSaveAndLeaveButtonClicked);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                OnLeaveButtonClicked();
            }
        }

        private void OnLeaveButtonClicked()
        {
            questionPackEditorMenu.SetActive(false);
            questionsList.Clear();
            _editedQuestionPack = null;
            QuestionEditorSceneData.Instance.QuestionPackListUiController.ShowAndReload();
        }

        private void OnSaveAndLeaveButtonClicked()
        {
            var foundQuestionPack = QuestionPackLoader.QuestionPacks.FirstOrDefault(t => t.Equals(_editedQuestionPack));

            _editedQuestionPack.PackName = questionPackName.text;
            _editedQuestionPack.Questions = questionsList.GetQuestions().ToList();

            if (foundQuestionPack != null)
            {
                var questionPacks = QuestionPackLoader.QuestionPacks.Where(t => t != foundQuestionPack)
                    .Append(_editedQuestionPack).ToArray();

                QuestionPackLoader.QuestionPacks = questionPacks;
            }
            else
            {
                QuestionPackLoader.QuestionPacks =
                    QuestionPackLoader.QuestionPacks.Append(_editedQuestionPack).ToArray();
            }

            OnLeaveButtonClicked();
        }


        public void OpenNewQuestionPack()
        {
            _editedQuestionPack = new QuestionPack();

            questionPackName.text = "";

            questionsList.AddEmptyQuestion();

            questionPackEditorMenu.SetActive(true);
        }

        public void OpenQuestionPack(QuestionPack questionPack)
        {
            _editedQuestionPack = questionPack;

            questionPackName.text = questionPack.PackName;

            foreach (var question in questionPack.Questions)
            {
                questionsList.AddQuestion(question);
            }

            questionPackEditorMenu.SetActive(true);
        }
    }
}