using Photon.Pun;
using Photon.Realtime;
using Settings;
using UnityEngine;

namespace Network
{
    public static class NetworkUtils
    {
        public static void ConnectToMaster()
        {
            if (PhotonNetwork.NetworkClientState != ClientState.Disconnected)
            {
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.ConnectToRegion("eu");
            }
            else
            {
                Debug.LogWarning("Already connected!");
            }
        }
    }
}