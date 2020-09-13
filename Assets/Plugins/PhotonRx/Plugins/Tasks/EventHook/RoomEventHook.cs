#if ( NET_4_6 || NET_STANDARD_2_0)
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Photon.Pun;

namespace PhotonRx
{
    public class RoomEventHook : MonoBehaviourPunCallbacks
    {
        private object gate = new object();

        private List<TaskCompletionSource<IResult<FailureReason, bool>>> observers
            = new List<TaskCompletionSource<IResult<FailureReason, bool>>>();

        public Task<IResult<FailureReason, bool>> Join(Action joinAction)
        {
            var tcs = new TaskCompletionSource<IResult<FailureReason, bool>>();
            lock (gate)
            {
                observers.Add(tcs);
            }

            joinAction();
            return tcs.Task;
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            var reason = new FailureReason(returnCode, message);
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Failure.Create<FailureReason, bool>(reason));
                }
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            var reason = new FailureReason(returnCode, message);
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Failure.Create<FailureReason, bool>(reason));
                }
            }
        }

        public override void OnJoinedRoom()
        {
            lock (gate)
            {
                var targets = observers.ToArray();
                observers.Clear();
                foreach (var t in targets)
                {
                    t.SetResult(Success.Create<FailureReason, bool>(true));
                }
            }
        }
    }
}
#endif