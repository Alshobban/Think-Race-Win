#if ( NET_4_6 || NET_STANDARD_2_0)
using System;
using System.Threading;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace PhotonRx
{
    public static class PhotonTask
    {
        #region Connect

        public static Task<IResult<DisconnectCause, bool>> ConnectToBestCloudServer(
            CancellationToken cancellationToken = default)
        {
            return Connect(() => PhotonNetwork.ConnectToBestCloudServer(), cancellationToken);
        }

        public static Task<IResult<DisconnectCause, bool>> ConnectToMaster(string masterServerAddress, int port,
            string appID, CancellationToken cancellationToken = default)
        {
            return Connect(() => PhotonNetwork.ConnectToMaster(masterServerAddress, port, appID),
                cancellationToken);
        }

        public static Task<IResult<DisconnectCause, bool>> ConnectToRegion(string region,
            CancellationToken cancellationToken = default)
        {
            return Connect(() => PhotonNetwork.ConnectToRegion(region), cancellationToken);
        }

        public static Task<IResult<DisconnectCause, bool>> ConnectUsingSettings(
            CancellationToken cancellationToken = default)
        {
            return Connect(() => PhotonNetwork.ConnectUsingSettings(), cancellationToken);
        }

        public static Task<IResult<DisconnectCause, bool>> ConnectUsingSettings(AppSettings appSettings,
            CancellationToken cancellationToken = default)
        {
            return Connect(() => PhotonNetwork.ConnectUsingSettings(appSettings), cancellationToken);
        }

        private static async Task<IResult<DisconnectCause, bool>> Connect(Action connectAction,
            CancellationToken cancellationToken)
        {
            var eventHook = GetOrAddComponent<ConnectionEventHook>(PhotonEventManager.Instance.gameObject);
            var result = await eventHook.Connect(connectAction).WithCancellation(cancellationToken);
            return result;
        }

        #endregion

        #region JoinRoom

        public static Task<IResult<FailureReason, bool>> CreateRoom(string roomName,
            CancellationToken cancellationToken = default)
        {
            return JoinRoom(() => PhotonNetwork.CreateRoom(roomName), cancellationToken);
        }

        public static Task<IResult<FailureReason, bool>> CreateRoom(string roomName, RoomOptions roomOptions,
            TypedLobby typedLobby, CancellationToken cancellationToken = default)
        {
            return JoinRoom(() => PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby), cancellationToken);
        }

        public static Task<IResult<FailureReason, bool>> CreateRoom(string roomName, RoomOptions roomOptions,
            TypedLobby typedLobby, string[] expectedUsers,
            CancellationToken cancellationToken = default)
        {
            return JoinRoom(() => PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby, expectedUsers),
                cancellationToken);
        }

        public static Task<IResult<FailureReason, bool>> JoinRoom(string roomName,
            CancellationToken cancellationToken = default)
        {
            return JoinRoom(() => PhotonNetwork.JoinRoom(roomName), cancellationToken);
        }

        public static Task<IResult<FailureReason, bool>> JoinOrCreateRoom(string roomName, RoomOptions roomOptions,
            TypedLobby typedLobby, string[] expectedUsers = null,
            CancellationToken cancellationToken = default)
        {
            return JoinRoom(() => PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, expectedUsers),
                cancellationToken);
        }

        public static Task<IResult<FailureReason, bool>> JoinRandomRoom(
            CancellationToken cancellationToken = default)
        {
            return JoinRoom(() => PhotonNetwork.JoinRandomRoom(), cancellationToken);
        }

        public static Task<IResult<FailureReason, bool>> JoinRandomRoom(Hashtable expectedCustomRoomProperties,
            byte expectedMaxPlayers, CancellationToken cancellationToken = default)
        {
            return JoinRoom(() => PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, expectedMaxPlayers),
                cancellationToken);
        }

        public static Task<IResult<FailureReason, bool>> JoinRandomRoom(Hashtable expectedCustomRoomProperties,
            byte expectedMaxPlayers, MatchmakingMode matchingType, TypedLobby typedLobby, string sqlLobbyFilter,
            string[] expectedUsers = null, CancellationToken cancellationToken = default)
        {
            return JoinRoom(() => PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, expectedMaxPlayers,
                matchingType, typedLobby, sqlLobbyFilter, expectedUsers), cancellationToken);
        }

        public static Task<IResult<FailureReason, bool>> RejoinRoom(string roomName,
            CancellationToken cancellationToken = default)
        {
            return JoinRoom(() => PhotonNetwork.RejoinRoom(roomName), cancellationToken);
        }

        private static async Task<IResult<FailureReason, bool>> JoinRoom(Action joinAction,
            CancellationToken cancellationToken)
        {
            var eventHook = GetOrAddComponent<RoomEventHook>(PhotonEventManager.Instance.gameObject);
            var result = await eventHook.Join(joinAction).WithCancellation(cancellationToken);
            return result;
        }

        #endregion

        private static T GetOrAddComponent<T>(GameObject gameObject)
            where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        // from https://blogs.msdn.microsoft.com/pfxteam/2012/10/05/how-do-i-cancel-non-cancelable-async-operations/
        public static async Task<T> WithCancellation<T>(
            this Task<T> task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(
                s => ((TaskCompletionSource<bool>) s).TrySetResult(true), tcs))
                if (task != await Task.WhenAny(task, tcs.Task))
                    throw new OperationCanceledException(cancellationToken);
            return await task;
        }
    }
}

#endif