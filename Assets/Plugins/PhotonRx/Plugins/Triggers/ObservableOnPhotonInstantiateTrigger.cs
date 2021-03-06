﻿using UnityEngine;
using System;
using Photon.Pun;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPhotonInstantiateTrigger : ObservableTriggerBase
    {
        private Subject<PhotonMessageInfo> onPhotonInstantiate;

        private void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            onPhotonInstantiate?.OnNext(info);
        }

        /// <summary>
        /// PhotonNetwork.InstantiateによってGameObjectが生成されたことを通知する
        /// </summary>
        public IObservable<PhotonMessageInfo> OnPhotonInstantiateAsObservable()
        {
            return onPhotonInstantiate ?? (onPhotonInstantiate = new Subject<PhotonMessageInfo>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onPhotonInstantiate?.OnCompleted();
        }
    }
}
