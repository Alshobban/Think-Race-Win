#if ( NET_4_6 || NET_STANDARD_2_0)
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;

namespace PhotonRx
{
    public class ConnectionEventHook : MonoBehaviourPunCallbacks
    {
        private object gate = new object();

        private List<TaskCompletionSource<IResult<DisconnectCause, bool>>> observers
            = new List<TaskCompletionSource<IResult<DisconnectCause, bool>>>();

        public Task<IResult<DisconnectCause, bool>> Connect(Action connectAction)
        {
            var tcs = new TaskCompletionSource<IResult<DisconnectCause, bool>>();
            lock (gate)
            {
                observers.Add(tcs);
            }

            connectAction();
            return tcs.Task;
        }

        public override void OnConnectedToMaster()
        {
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Success.Create<DisconnectCause, bool>(true));
                }
            }
        }

        public override void OnJoinedLobby()
        {
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Success.Create<DisconnectCause, bool>(true));
                }
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Failure.Create<DisconnectCause, bool>(cause));
                }
            }
        }
    }
}
#endif