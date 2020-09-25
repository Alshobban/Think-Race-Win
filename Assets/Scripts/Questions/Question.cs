using System;
using MessagePack;

namespace Quiz
{
    [Serializable]
    [MessagePackObject]
    public struct Question
    {
        [Key(0)]
        public string QuestionText { get; private set; }

        [Key(1)]
        public string CorrectAnswer { get; private set; }

        [Key(2)]
        public string IncorrectAnswer1 { get; private set; }

        [Key(3)]
        public string IncorrectAnswer2 { get; private set; }

        [Key(4)]
        public string IncorrectAnswer3 { get; private set; }

        public Question(string questionText, string correctAnswer, string ans1, string ans2, string ans3)
        {
            QuestionText = questionText;
            CorrectAnswer = correctAnswer;
            IncorrectAnswer1 = ans1;
            IncorrectAnswer2 = ans2;
            IncorrectAnswer3 = ans3;
        }

        public static byte[] Serialize(object customObject)
        {
            return MessagePackSerializer.Serialize((Question) customObject,
                MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block));
        }

        public static object Deserialize(byte[] serializedCustomObject)
        {
            return MessagePackSerializer.Deserialize(typeof(Question), serializedCustomObject,
                MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block));
        }
    }
}