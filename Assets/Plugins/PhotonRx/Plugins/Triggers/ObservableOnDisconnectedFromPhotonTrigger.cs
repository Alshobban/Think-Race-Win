using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnDisconnectedFromPhotonTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onDisconnectedFromPhoton;

        private void OnDisconnectedFromPhoton()
        {
            onDisconnectedFromPhoton?.OnNext(Unit.Default);
        }

        /// <summary>
        /// サーバから切断されたことを通知する
        /// </summary>
        public IObservable<Unit> OnDisconnectedFromPhotonAsObservable()
        {
            return onDisconnectedFromPhoton ?? (onDisconnectedFromPhoton = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onDisconnectedFromPhoton?.OnCompleted();
        }
    }
}
