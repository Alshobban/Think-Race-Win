using System.Collections.Generic;
using desExt.Runtime.References;
using Photon.Pun;
using Photon.Realtime;
using Questions;
using UnityEngine;
using Utilities;

namespace SceneSpecific.Game
{
    public class GameSceneController : MonoBehaviour
    {
        [SerializeField]
        private StringReference localPlayerPrefabLocation;

        private void Awake()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.OfflineMode = true;
                PhotonNetwork.JoinRandomRoom();
                GameData.CurrentQuestionPack = QuestionPackLoader.QuestionPacks.RandomElement();
                GameData.QualifiedPlayers = new List<Player> {PhotonNetwork.LocalPlayer};
                Debug.LogWarning("Not connected to the network!");
            }

            if (PhotonNetwork.IsMasterClient)
            {
                foreach (var player in GameData.QualifiedPlayers)
                {
                    var startPosition = GameSceneData.Instance.GetVacantStartPosition();

                    var playersCar = PhotonNetwork.Instantiate(localPlayerPrefabLocation, startPosition.position,
                        Quaternion.identity);
                    playersCar.transform.GetChild(0).GetChild(0).Rotate(0f, startPosition.rotation.eulerAngles.y, 0f);

                    var newPhotonView = playersCar.GetComponent<PhotonView>();
                    newPhotonView.TransferOwnership(player);
                }
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                var startPosition = GameSceneData.Instance.GetVacantStartPosition();

                var playersCar = PhotonNetwork.Instantiate(localPlayerPrefabLocation, startPosition.position,
                    Quaternion.identity);
            }
        }
#endif
    }
}