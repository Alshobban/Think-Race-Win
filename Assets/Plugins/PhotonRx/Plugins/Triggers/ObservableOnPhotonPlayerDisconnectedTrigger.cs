using UnityEngine;
using System;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPlayerDisconnectedTrigger : ObservableTriggerBase
    {
        private Subject<Player> onPlayerDisconnected;

        private void OnPlayerDisconnected(Player leftPlayer)
        {
            onPlayerDisconnected?.OnNext(leftPlayer);
        }

        /// <summary>
        /// リモートプレイヤが部屋から退出したことを通知する
        /// </summary>
        public IObservable<Player> OnPlayerDisconnectedAsObservable()
        {
            return onPlayerDisconnected ?? (onPlayerDisconnected = new Subject<Player>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onPlayerDisconnected?.OnCompleted();
        }
    }
}
