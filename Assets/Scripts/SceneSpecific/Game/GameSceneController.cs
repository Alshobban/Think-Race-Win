using System.Collections.Generic;
using Car;
using desExt.Runtime.References;
using Photon.Pun;
using Photon.Realtime;
using Questions;
using UnityEngine;
using Utilities;

namespace SceneSpecific.Game
{
    public class GameSceneController : MonoBehaviourPunCallbacks
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

            // var startPosition =
            // GameSceneData.Instance.GetPositionByNumber(
            // GameData.QualifiedPlayers.IndexOf(
            // GameData.QualifiedPlayers.Find(t => t.Equals(PhotonNetwork.LocalPlayer))));

            foreach (var player in GameData.QualifiedPlayers)
            {
                var startPosition = GameSceneData.Instance.GetVacantStartPosition();
                photonView.RPC(nameof(SpawnCar), player, Random.Range(0, 20), startPosition.position,
                    startPosition.rotation);
            }
        }

        [PunRPC]
        private void SpawnCar(int prefabNumber, Vector3 position, Quaternion rotation)
        {
            var newCar = PhotonNetwork.Instantiate(localPlayerPrefabLocation, position, Quaternion.identity);
            newCar.GetComponent<CarSpawner>()?.SpawnCar(prefabNumber);
            newCar.transform.GetChild(0).GetChild(0).Rotate(0f, rotation.eulerAngles.y, 0f);
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