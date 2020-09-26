using UnityEngine;

namespace Questions
{
    [CreateAssetMenu(fileName = "QuestionPack")]
    public class QuestionPackScriptableObject : ScriptableObject
    {
        [SerializeField]
        private QuestionPack questionPack;

        public QuestionPack QuestionPack => questionPack;
    }
}