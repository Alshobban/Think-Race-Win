using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace SceneSpecific.Qualifiers
{
    public class QualifiersUiController : MonoBehaviour, IOnEventCallback
    {
        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        [SerializeField]
        private TextMeshProUGUI leaderboard;

        public void UpdateLeaderboard(IDictionary<string, float> leaderboardValues)
        {
            var sortedLeaderboard = leaderboardValues.OrderByDescending(t => t.Value);

            leaderboard.text = "";
            foreach (var score in sortedLeaderboard)
            {
                leaderboard.text += score.Key + " " + (int) score.Value + '\n';
            }
        }

        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == PunEvents.LeaderboardUpdated)
            {
                UpdateLeaderboard((Dictionary<string, float>) photonEvent.CustomData);
            }
        }
    }
}