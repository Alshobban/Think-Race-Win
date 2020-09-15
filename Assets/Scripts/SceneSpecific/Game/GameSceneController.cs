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
            if (PhotonNetwork.OfflineMode)
                return;

            var newCar = PhotonNetwork.Instantiate(localPlayerPrefabLocation, startPosition.position,
                Quaternion.identity);

            newCar.transform.GetChild(0).GetChild(0).Rotate(0f, startPosition.rotation.eulerAngles.y, 0f);
            Debug.Log(newCar.transform.GetChild(0).rotation.y);

            var anchor = newCar.transform.GetChild(0).GetChild(0);

            cinemachineVirtualCamera.Follow = anchor;
            cinemachineVirtualCamera.LookAt = anchor;
        }
    }
}