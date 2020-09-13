using UnityEngine;
using System;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnConnectionFailTrigger : ObservableTriggerBase
    {
        private Subject<DisconnectCause> onConnectionFail;

        private void OnConnectionFail(DisconnectCause cause)
        {
            onConnectionFail?.OnNext(cause);
        }

        /// <summary>
        /// 接続が失敗したことを原因とともに通知する
        /// </summary>
        public IObservable<DisconnectCause> OnConnectionFailAsObservable()
        {
            return onConnectionFail ?? (onConnectionFail = new Subject<DisconnectCause>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onConnectionFail?.OnCompleted();
        }
    }
}
