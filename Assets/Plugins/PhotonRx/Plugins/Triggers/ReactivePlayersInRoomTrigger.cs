using UnityEngine;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ReactivePlayersTriggers : ObservableTriggerBase
    {
        private ReactiveCollection<Player> _playersReactiveCollection; 

        /// <summary>
        /// PhotonNetwork.Friendsが更新されたことを通知する
        /// </summary>
        public ReactiveCollection<Player> PlayersReactiveCollection()
        {
            return _playersReactiveCollection ??
                   (_playersReactiveCollection = new ReactiveCollection<Player>(PhotonNetwork.PlayerList.ToList()));
        }

        private void OnJoinedRoom()
        {
            if (_playersReactiveCollection == null) return;

            if (_playersReactiveCollection.Count > 0)
            {
                _playersReactiveCollection.Clear();
            }
            foreach (var player in PhotonNetwork.PlayerList)
            {
                _playersReactiveCollection.Add(player);
            }
        }

        private void OnLeftRoom()
        {
            _playersReactiveCollection?.Clear();
        }

        private void OnPlayerConnected(Player newPlayer)
        {
            _playersReactiveCollection?.Add(newPlayer);
        }

        private void OnPlayerDisconnected(Player otherPlayer)
        {
            _playersReactiveCollection?.Remove(otherPlayer);
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            _playersReactiveCollection?.Dispose();
        }
    }
}
