using UnityEngine;
using System;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPlayerActivityChanged : ObservableTriggerBase
    {
        private Subject<Player> onChanged;

        private void OnPlayerActivityChanged(Player otherPlayer)
        {
            onChanged?.OnNext(otherPlayer);
        }

        public IObservable<Player> OnPlayerActivityChangedAsObservable()
        {
            return onChanged ?? (onChanged = new Subject<Player>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onChanged?.OnCompleted();
        }
    }
}
