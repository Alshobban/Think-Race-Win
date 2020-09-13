using UnityEngine;
using System;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPlayerConnectedTrigger : ObservableTriggerBase
    {
        private Subject<Player> onPlayerConnected;

        private void OnPlayerConnected(Player newPlayer)
        {
            onPlayerConnected?.OnNext(newPlayer);
        }

        /// <summary>
        /// リモートプレイヤが部屋に参加したことを通知する
        /// </summary>
        public IObservable<Player> OnPlayerConnectedAsObservable()
        {
            return onPlayerConnected ?? (onPlayerConnected = new Subject<Player>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onPlayerConnected?.OnCompleted();
        }
    }
}
