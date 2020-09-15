using Photon.Pun;

namespace Network
{
    public static class NetworkUtils
    {
        public static void ConnectToMaster()
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}