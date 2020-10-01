using System;
using System.Collections.Generic;
using System.Linq;
using Quiz;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace SceneSpecific.QuestionEditor
{
    public class QuestionListController : MonoBehaviour
    {
        [SerializeField]
        private GameObject questionViewPrefab;

        [SerializeField]
        private ScrollRect questionList;

        [SerializeField]
        private Button addQuestionButton;

        [SerializeField]
        private Button removeQuestionButton;

        private QuestionView _selectedQuestionView;

        private readonly List<QuestionView> _questionViews = new List<QuestionView>();

        private void Update()
        {
            if (EventSystem.current.isFocused)
            {
                var questionView = EventSystem.current.currentSelectedGameObject?.GetComponentInParent<QuestionView>();
                if (questionView)
                {
                    _selectedQuestionView = questionView;
                }
            }
        }

        public void Clear()
        {
            foreach (var questionView in _questionViews)
            {
                Destroy(questionView.gameObject);
            }
            
            _questionViews.Clear();
        }

        private void OnEnable()
        {
            addQuestionButton.onClick.AddListener(AddEmptyQuestion);
            removeQuestionButton.onClick.AddListener(TryRemoveSelectedQuestion);
        }

        private void OnDisable()
        {
            addQuestionButton.onClick.RemoveListener(AddEmptyQuestion);
            removeQuestionButton.onClick.RemoveListener(TryRemoveSelectedQuestion);
        }

        public Question[] GetQuestions()
        {
            return _questionViews.Select(t => t.GetQuestion()).ToArray();
        }

        public void AddEmptyQuestion()
        {
            var newQuestionView = Instantiate(questionViewPrefab, questionList.content).GetComponent<QuestionView>();

            newQuestionView.SetQuestion(new Question());
            _questionViews.Add(newQuestionView);
        }

        private void TryRemoveSelectedQuestion()
        {
            if (_selectedQuestionView != null)
            {
                Destroy(_selectedQuestionView.gameObject);
                _questionViews.Remove(_selectedQuestionView);
            }
        }

        public void AddQuestion(Question question)
        {
            var newQuestionView = Instantiate(questionViewPrefab, questionList.content).GetComponent<QuestionView>();

            newQuestionView.SetQuestion(question);

            _questionViews.Add(newQuestionView);
        }
    }
}