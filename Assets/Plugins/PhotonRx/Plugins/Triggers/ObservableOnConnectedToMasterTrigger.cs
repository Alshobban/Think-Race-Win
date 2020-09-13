using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnConnectedToMasterTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onConnectedToMaster;

        private void OnConnectedToMaster()
        {
            onConnectedToMaster?.OnNext(Unit.Default);
        }

        /// <summary>
        /// PhotonNetwork.autoJoinLobbyがfalseのときにMasterServerのロビーに参加できたことを通知する
        /// </summary>
        public IObservable<Unit> OnConnectedToMasterAsObservable()
        {
            return onConnectedToMaster ?? (onConnectedToMaster = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onConnectedToMaster?.OnCompleted();
        }
    }
}
