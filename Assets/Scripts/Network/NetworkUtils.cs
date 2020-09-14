using Photon.Pun;
using UnityEngine;

namespace Network
{
    public static class NetworkUtils
    {
        public static void ConnectToMaster()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public static bool CreateAndJoinRandomRoom(bool retryOnError = true)
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.LogError($"Already connected to room {PhotonNetwork.CurrentRoom.Name}");
                return false;
            }

            PhotonNetwork.CreateRoom(null);

            return true;
        }

        private static bool CreateRandomRoom()
        {
            return PhotonNetwork.CreateRoom(Random.Range(0, 1000).ToString());
        }
    }
}