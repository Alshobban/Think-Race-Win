using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnUpdatedFriendListTrigger : ObservableTriggerBase
    {
        private Subject<Unit> onUpdatedFriendList;

        private void OnUpdatedFriendList()
        {
            onUpdatedFriendList?.OnNext(Unit.Default);
        }

        /// <summary>
        /// PhotonNetwork.Friendsが更新されたことを通知する
        /// </summary>
        public IObservable<Unit> OnUpdatedFriendListAsObservable()
        {
            return onUpdatedFriendList ?? (onUpdatedFriendList = new Subject<Unit>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onUpdatedFriendList?.OnCompleted();
        }
    }
}
