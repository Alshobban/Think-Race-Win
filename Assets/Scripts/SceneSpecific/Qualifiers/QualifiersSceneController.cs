using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ExitGames.Client.Photon;
using NaughtyAttributes;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace SceneSpecific.Qualifiers
{
    public class QualifiersSceneController : MonoBehaviour
    {
        [Scene]
        [SerializeField]
        private string gameScene;

        [SerializeField]
        private float beforeGameDelay;

        private void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                QualifiersSceneData.Instance.QualifiersController.StartQualifiers();
                var leaderboardTemplate = new Dictionary<string, float>();
                foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
                {
                    leaderboardTemplate.Add(player.NickName, 0f);
                }

                PhotonNetwork.RaiseEvent(PunEvents.LeaderboardUpdated, leaderboardTemplate,
                    new RaiseEventOptions {Receivers = ReceiverGroup.All},
                    SendOptions.SendReliable);
            }
        }

        private void OnEnable()
        {
            QualifiersSceneData.Instance.QualifiersController.LeaderboardUpdated +=
                QualifiersSceneData.Instance.QualifiersUiController.UpdateLeaderboard;

            QualifiersSceneData.Instance.QualifiersController.Finished += OnQualifiersFinished;
        }

        private void OnDisable()
        {
            QualifiersSceneData.Instance.QualifiersController.LeaderboardUpdated -=
                QualifiersSceneData.Instance.QualifiersUiController.UpdateLeaderboard;

            QualifiersSceneData.Instance.QualifiersController.Finished -= OnQualifiersFinished;
        }

        private async void OnQualifiersFinished()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(beforeGameDelay));
            PhotonNetwork.LoadLevel(gameScene);
        }
    }
}