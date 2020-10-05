using Cinemachine;
using desExt.Runtime.References;
using Photon.Pun;
using Questions;
using UnityEngine;
using Utilities;

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
                PhotonNetwork.OfflineMode = true;
                GameData.CurrentQuestionPack = QuestionPackLoader.QuestionPacks.RandomElement();

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