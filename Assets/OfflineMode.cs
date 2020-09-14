using Photon.Pun;
using UnityEngine;

public class OfflineMode : MonoBehaviour
{
    private void OnEnable()
    {
        PhotonNetwork.OfflineMode = true;
    }

    private void OnDisable()
    {
        PhotonNetwork.OfflineMode = false;
    }
}