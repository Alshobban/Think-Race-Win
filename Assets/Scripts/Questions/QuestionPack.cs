using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using Quiz;
using UnityEngine;
using UnityEngine.Serialization;

namespace Questions
{
    [Serializable]
    public class QuestionPack : IEquatable<QuestionPack>
    {
        public string PackName
        {
            get => n;
            set => n = value;
        }

        public List<Question> Questions
        {
            get => q;
            set => q = value;
        }

        [FormerlySerializedAs("_guid")]
        [SerializeField]
        public string guid;

        [FormerlySerializedAs("packName")]
        [SerializeField]
        private string n;

        [FormerlySerializedAs("questions")]
        [SerializeField]
        private List<Question> q;

        public QuestionPack(string packName, IEnumerable<Question> questions)
        {
            guid = Guid.NewGuid().ToString();
            PackName = packName;
            Questions = questions.ToList();
        }

        public QuestionPack()
        {
            guid = Guid.NewGuid().ToString();
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public void RemoveQuestion(Question question)
        {
            Questions.Remove(question);
        }

        public bool Equals(QuestionPack other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return guid.Equals(other.guid);
        }

        public static byte[] Serialize(object questionPack)
        {
            var q = (QuestionPack) questionPack;

            return Encoding.ASCII.GetBytes(JsonUtility.ToJson(q));
        }

        public static object Deserialize(byte[] questionPack)
        {
            return JsonUtility.FromJson<QuestionPack>(Encoding.ASCII.GetString(questionPack));
        }

        [RuntimeInitializeOnLoadMethod]
        private static void RegisterSerialization()
        {
            PhotonPeer.RegisterType(typeof(QuestionPack), (byte) 'z', Serialize, Deserialize);
        }
    }
}