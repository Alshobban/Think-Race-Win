using NaughtyAttributes;
using Photon.Pun;
using Photon.Realtime;
using SceneSpecific.QuestionEditor;
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

        [SerializeField]
        private QuestionPackListController questionPackListController;

        private void Start()
        {
            SetupInterface();

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

            startButton.onClick.RemoveListener(OnStartButtonClicked);
            readyButton.onClick.RemoveListener(OnReadyButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            PhotonNetwork.LoadLevel(gameScene);
        }

        private void OnReadyButtonClicked()
        {
            Debug.Log("does nothing yet, lul");
        }

        private void SetupInterface()
        {
            startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
            readyButton.gameObject.SetActive(!PhotonNetwork.IsMasterClient);

            // questionPackListController.transform.parent.gameObject.SetActive(PhotonNetwork.IsMasterClient);

            // if (PhotonNetwork.IsMasterClient)
            {
                questionPackListController.AddSavedQuestionPacks();
            }
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            SetupInterface();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            playerList.AddLine(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            playerList.RemoveLine(otherPlayer);

            SetupInterface();
        }
    }
}