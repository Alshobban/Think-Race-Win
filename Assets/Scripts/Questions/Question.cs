using System;
using System.Text;
using ExitGames.Client.Photon;
using Questions;
using UnityEngine;

namespace Quiz
{
    [Serializable]
    public struct Question : IEquatable<Question>
    {
        //Names are bad for the sake of saving the space
        [SerializeField]
        private string q;

        [SerializeField]
        private string a;

        [SerializeField]
        private string i1;

        [SerializeField]
        private string i2;

        [SerializeField]
        private string i3;

        public string QuestionText => q;

        public string CorrectAnswer => a;

        public string IncorrectAnswer1 => i1;

        public string IncorrectAnswer2 => i2;

        public string IncorrectAnswer3 => i3;

        public Question(string questionText, string correctAnswer, string ans1, string ans2, string ans3)
        {
            q = questionText;
            a = correctAnswer;
            i1 = ans1;
            i2 = ans2;
            i3 = ans3;
        }

        public bool Equals(Question other)
        {
            return QuestionText == other.QuestionText && CorrectAnswer == other.CorrectAnswer &&
                   IncorrectAnswer1 == other.IncorrectAnswer1 && IncorrectAnswer2 == other.IncorrectAnswer2 &&
                   IncorrectAnswer3 == other.IncorrectAnswer3;
        }

        public static byte[] Serialize(object questionPack)
        {
            var q = (Question) questionPack;

            return Encoding.ASCII.GetBytes(JsonUtility.ToJson(q));
        }

        public static object Deserialize(byte[] questionPack)
        {
            return JsonUtility.FromJson<Question>(Encoding.ASCII.GetString(questionPack));
        }

        [RuntimeInitializeOnLoadMethod]
        private static void RegisterSerialization()
        {
            PhotonPeer.RegisterType(typeof(Question), (byte) 'x', Serialize, Deserialize);
        }
    }
}