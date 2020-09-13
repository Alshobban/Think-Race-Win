using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnCustomAuthenticationFailedTrigger : ObservableTriggerBase
    {
        private Subject<string> onCustomAuthenticationFailed;

        private void OnCustomAuthenticationFailed(string message)
        {
            onCustomAuthenticationFailed?.OnNext(message);
        }

        /// <summary>
        /// カスタム認証に失敗したことを通知する
        /// </summary>
        public IObservable<string> OnCustomAuthenticationFailedAsObservable()
        {
            return onCustomAuthenticationFailed ?? (onCustomAuthenticationFailed = new Subject<string>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onCustomAuthenticationFailed?.OnCompleted();
        }
    }
}
