using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Quiz;
using SceneSpecific.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SceneSpecific.Qualifiers
{
    public class QualifiersController : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        public event Action<Dictionary<string, float>> LeaderboardUpdated;
        public event Action Finished;

        [SerializeField]
        private QuizController quizController;

        [SerializeField]
        private int numberOfQuestions = 5;

        [SerializeField]
        private float timeToAnswer;

        [SerializeField]
        private float pauseAfterAllAnswered = 2f;

        [SerializeField]
        private float scoreMultiplier;

        [SerializeField]
        private Slider timeSlider;

        private Tween _timeSliderTween;

        private bool _qualifiersRunning;
        private readonly Dictionary<int, float> _playersScores = new Dictionary<int, float>();
        private readonly Dictionary<int, bool> _playersAnswered = new Dictionary<int, bool>();

        public override void OnEnable()
        {
            base.OnEnable();

            quizController.AnsweredCorrectly += OnAnsweredQuestion;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            quizController.AnsweredCorrectly -= OnAnsweredQuestion;
        }

        public async void StartQualifiers()
        {
            if (_qualifiersRunning)
            {
                Debug.LogError("Qualifiers are already running!");
                return;
            }

            _qualifiersRunning = true;

            _playersAnswered.Clear();
            _playersScores.Clear();
            foreach (var player in PhotonNetwork.CurrentRoom.Players.Keys)
            {
                _playersScores.Add(player, 0f);
                _playersAnswered.Add(player, false);
            }

            for (var i = 0; i < numberOfQuestions; i++)
            {
                photonView.RPC(nameof(SetQuestion), RpcTarget.All, GameData.GetNextQuestion());

                Debug.Log("waiting for everyone to answer");
                await UniTask.WaitUntil(() => _playersAnswered.Values.All(answered => answered));
                //Reset 'answered' flag
                foreach (var player in _playersAnswered.Keys.ToList())
                {
                    _playersAnswered[player] = false;
                }

                Debug.Log("everyone answered, updating a leaderboard");
                var leaderboard = _playersScores.ToDictionary(
                    player => PhotonNetwork.CurrentRoom.GetPlayer(player.Key).NickName, score => score.Value);
                LeaderboardUpdated?.Invoke(leaderboard);
                PhotonNetwork.RaiseEvent(PunEvents.LeaderboardUpdated, leaderboard, RaiseEventOptions.Default,
                    SendOptions.SendReliable);

                Debug.Log("waiting a delay before the next question");
                await UniTask.Delay(TimeSpan.FromSeconds(pauseAfterAllAnswered));
            }

            GameData.QualifiedPlayers = _playersScores.OrderByDescending(t => t.Value)
                .Select(t => PhotonNetwork.CurrentRoom.GetPlayer(t.Key)).ToList();

            foreach (var player in GameData.QualifiedPlayers)
            {
                photonView.RPC(nameof(SetQualifiedPlace), player, GameData.QualifiedPlayers.IndexOf(player));
            }

            Finished?.Invoke();
            _qualifiersRunning = false;
        }

        [PunRPC]
        private void SetQualifiedPlace(int place)
        {
            GameData.QualifiedPlace = place;
        }

        [PunRPC]
        private void SetQuestion(Question question)
        {
            Debug.Log("showing the question");

            quizController.ShowQuiz(question, false);
            timeSlider.gameObject.SetActive(true);
            timeSlider.value = timeSlider.minValue;

            //Start timer and automatically send wrong answer if not answered in time
            _timeSliderTween = timeSlider.DOValue(timeSlider.maxValue, timeToAnswer)
                .OnComplete(() => RaiseAnswerEvent(false, default));
        }


        private void OnAnsweredQuestion(bool correct)
        {
            var timeTakenToAnswer = _timeSliderTween.Elapsed();
            _timeSliderTween.Kill();
            _timeSliderTween = null;

            RaiseAnswerEvent(correct, timeTakenToAnswer);
        }

        private void RaiseAnswerEvent(bool correct, float timeTakenToAnswer)
        {
            PhotonNetwork.RaiseEvent(PunEvents.QualifierQuestionAnswered, new object[] {correct, timeTakenToAnswer},
                new RaiseEventOptions {Receivers = ReceiverGroup.MasterClient}, SendOptions.SendReliable);
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == PunEvents.QualifierQuestionAnswered)
            {
                var data = (object[]) photonEvent.CustomData;

                //If answered correctly
                if ((bool) data[0])
                {
                    _playersScores[photonEvent.Sender] += (timeToAnswer - (float) data[1]) * scoreMultiplier;
                }

                _playersAnswered[photonEvent.Sender] = true;
            }
        }
    }
}