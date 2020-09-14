using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UniRx;
using UnityEngine;

namespace SceneSpecific.MainMenu
{
    public class PlayerListController : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerLinePrefab;

        [SerializeField]
        private Transform contentTransform;

        private readonly Dictionary<Player, GameObject> _players = new Dictionary<Player, GameObject>();

        public void AddPlayer(Player player)
        {
            var newPlayerLine = Instantiate(playerLinePrefab, Vector3.zero, Quaternion.identity, contentTransform);

            newPlayerLine.GetComponentInChildren<TextMeshProUGUI>()?.SetText(player.NickName);

            _players.Add(player, newPlayerLine);
        }

        public void RemovePlayer(Player player)
        {
            if (_players.ContainsKey(player))
            {
                Destroy(_players[player]);
                _players.Remove(player);
            }
            else
            {
                Debug.LogError($"User {player.UserId}:{player.NickName} not registered in players list!");
            }
        }
    }
}