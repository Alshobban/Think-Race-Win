using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnOwnershipTransferred : ObservableTriggerBase
    {

        private Subject<OwnershipTransferredObject> onChanged;

        public void OnOwnershipTransferred(object[] viewAndPlayers)
        {
            if (onChanged == null) return;

            var view = viewAndPlayers[0] as PhotonView;

            var newOwner = viewAndPlayers[1] as Player;

            var oldOwner = viewAndPlayers[2] as Player;

            onChanged.OnNext(new OwnershipTransferredObject(view, newOwner, oldOwner));
        }

        /// <summary>
        /// 自身が部屋から出たことを通知する
        /// </summary>
        public IObservable<OwnershipTransferredObject> OnOwnershipTransferredAsObservable()
        {
            return onChanged ?? (onChanged = new Subject<OwnershipTransferredObject>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onChanged?.OnCompleted();
        }
    }

    public struct OwnershipTransferredObject
    {
        public PhotonView View { get; private set; }
        public Player OldOwner { get; private set; }
        public Player NewOwner { get; private set; }

        public OwnershipTransferredObject(PhotonView view, Player oldOwner, Player newOwner) : this()
        {
            View = view;
            OldOwner = oldOwner;
            NewOwner = newOwner;
        }
    }
}
