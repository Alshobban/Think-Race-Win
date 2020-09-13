using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnOwnershipRequestTrigger : ObservableTriggerBase
    {
        private Subject<Tuple<PhotonView,Player>> onOwnershipRequest;

        private void OnOwnershipRequest(object[] data)
        {
            onOwnershipRequest?.OnNext(
                new Tuple<PhotonView, Player>(
                    data[0] as PhotonView,
                    data[1] as Player
                ));
        }

        /// <summary>
        /// PhotonViewの所有権の譲渡リクエストがきたことを通知する
        /// </summary>
        public IObservable<Tuple<PhotonView, Player>> OnOwnershipRequestAsObservable()
        {
            return onOwnershipRequest ?? (onOwnershipRequest = new Subject<Tuple<PhotonView, Player>>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onOwnershipRequest?.OnCompleted();
        }
    }
}
