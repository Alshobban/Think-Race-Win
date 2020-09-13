using UnityEngine;
using System;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;

namespace PhotonRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnPlayerPropertiesChangedTrigger : ObservableTriggerBase
    {
        private Subject<Tuple<Player,Hashtable>> onPlayerPropertiesChanged;

        private void OnPlayerPropertiesChanged(object[] data)
        {
            onPlayerPropertiesChanged?.OnNext(new Tuple<Player, Hashtable>(
                data[0] as Player,
                data[1] as Hashtable
            ));
        }

        /// <summary>
        /// プレイヤのカスタムプロパティが変更されたことを通知する
        /// </summary>
        public IObservable<Tuple<Player, Hashtable>> OnPlayerPropertiesChangedAsObservable()
        {
            return onPlayerPropertiesChanged ?? (onPlayerPropertiesChanged = new Subject<Tuple<Player, Hashtable>>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            onPlayerPropertiesChanged?.OnCompleted();
        }
    }
}
