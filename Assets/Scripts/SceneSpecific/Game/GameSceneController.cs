using Cinemachine;
using desExt.Runtime.References;
using Network;
using Photon.Pun;
using UnityEngine;

namespace SceneSpecific.Game
{
    public class GameSceneController : MonoBehaviour
    {
        [SerializeField]
        private Transform startPosition;

        [SerializeField]
        private StringReference localPlayerPrefabLocation;

        [SerializeField]
        private CinemachineVirtualCamera cinemachineVirtualCamera;

        private void Awake()
        {
            GameObject newCar;
            if (PhotonNetwork.IsConnected)
            {
                newCar = PhotonNetwork.Instantiate(localPlayerPrefabLocation, startPosition.position,
                    Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Not connected to the network!");
                newCar = Instantiate(Resources.Load<GameObject>(localPlayerPrefabLocation + "Offline"),
                    startPosition.position,
                    Quaternion.identity);
            }

            newCar.transform.GetChild(0).GetChild(0).Rotate(0f, startPosition.rotation.eulerAngles.y, 0f);

            var anchor = newCar.transform.GetChild(0).GetChild(0);
            cinemachineVirtualCamera.Follow = anchor;
            cinemachineVirtualCamera.LookAt = anchor;
        }
    }
}