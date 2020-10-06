using System.Collections.Generic;
using Photon.Realtime;
using Questions;
using Quiz;
using Utilities;

public static class GameData
{
    public static List<Player> QualifiedPlayers { get; set; }
    public static QuestionPack CurrentQuestionPack { get; set; }

    private static readonly Queue<Question> QuestionList = new Queue<Question>();

    public static Question GetNextQuestion()
    {
        if (QuestionList.Count < 1)
        {
            foreach (var question in CurrentQuestionPack.Questions.Shuffle())
            {
                QuestionList.Enqueue(question);
            }
        }

        return QuestionList.Dequeue();
    }
}