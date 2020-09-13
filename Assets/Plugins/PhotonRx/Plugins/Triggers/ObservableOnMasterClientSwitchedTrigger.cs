using UnityEngine;
using System;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnMasterClientSwitchedTrigger : ObservableTriggerBase
    {
        private Subject<Player> onMasterClientSwitched;

        private void OnMasterClientSwitched(Player masterClient)
        {
            onMasterClientSwitched?.OnNext(masterClient);
        }

        /// <summary>
        /// マスタークライアントが退室し、新しいマスタークライアントに切り替わったことを通知する
        /// </summary>
        public IObservable<Player> OnMasterClientSwitchedAsObservable()
        {
            return onMasterClientSwitched ?? (onMasterClientSwitched = new Subject<Player>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onMasterClientSwitched?.OnCompleted();
        }
    }
}
