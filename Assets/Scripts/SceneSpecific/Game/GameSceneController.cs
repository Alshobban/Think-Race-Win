using Cinemachine;
using Photon.Pun;
using UnityEngine;

namespace SceneSpecific.Game
{
    public class GameSceneController : MonoBehaviour
    {
        [SerializeField]
        private Transform startPosition;

        [SerializeField]
        private string localPlayerPrefabLocation;

        [SerializeField]
        private CinemachineVirtualCamera cinemachineVirtualCamera;

        private void Awake()
        {
            if (!PhotonNetwork.IsConnectedAndReady)
                return;

            var newCar = PhotonNetwork.Instantiate(localPlayerPrefabLocation, startPosition.position,
                startPosition.rotation);

            var anchor = newCar.transform.GetChild(0).GetChild(0);

            cinemachineVirtualCamera.Follow = anchor;
            cinemachineVirtualCamera.LookAt = anchor;
        }
    }
}