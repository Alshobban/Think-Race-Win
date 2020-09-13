using UnityEngine;
using System;
using Photon.Pun;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonSerializeViewTrigger : ObservableTriggerBase
    {
        private bool isInitialized;

        private Subject<Tuple<PhotonStream, PhotonMessageInfo>> onPhotonSerializeView;

        private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            onPhotonSerializeView?.OnNext(new Tuple<PhotonStream, PhotonMessageInfo>(stream, info));
        }

        /// <summary>
        /// PhotonViewがデータの同期を行うタイミングを通知する
        /// </summary>
        public IObservable<Tuple<PhotonStream, PhotonMessageInfo>> OnPhotonSerializeViewAsObservable()
        {
            if (!isInitialized)
            {
                var view = PhotonView.Get(this);
                if (view == null) throw new Exception("Not found PhotonView.");
                if (!view.ObservedComponents.Contains(this)) view.ObservedComponents.Add(this);
                isInitialized = true;
            }

            return onPhotonSerializeView ??
                   (onPhotonSerializeView = new Subject<Tuple<PhotonStream, PhotonMessageInfo>>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onPhotonSerializeView?.OnCompleted();
        }
    }
}
