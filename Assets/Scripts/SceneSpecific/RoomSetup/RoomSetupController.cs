using NaughtyAttributes;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Utilities;
using Button = UnityEngine.UI.Button;

namespace SceneSpecific.RoomSetup
{
    public class RoomSetupController : MonoBehaviourPunCallbacks
    {
        [Scene]
        [SerializeField]
        private string gameScene;

        [SerializeField]
        private PlayerListController playerList;

        [SerializeField]
        private Button startButton;

        [SerializeField]
        private Button readyButton;

        private void Awake()
        {
            SetupButtons();

            foreach (var player in PhotonNetwork.PlayerList)
            {
                playerList.AddLine(player);
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();

            startButton.onClick.AddListener(OnStartButtonClicked);
            readyButton.onClick.AddListener(OnReadyButtonClicked);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            startButton.onClick.AddListener(OnStartButtonClicked);
            readyButton.onClick.AddListener(OnReadyButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            PhotonNetwork.LoadLevel(gameScene);
        }

        private void OnReadyButtonClicked()
        {
            Debug.Log("does nothing yet, lul");
        }

        private void SetupButtons()
        {
            startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
            readyButton.gameObject.SetActive(!PhotonNetwork.IsMasterClient);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            playerList.AddLine(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            playerList.RemoveLine(otherPlayer);

            SetupButtons();
        }
    }
}