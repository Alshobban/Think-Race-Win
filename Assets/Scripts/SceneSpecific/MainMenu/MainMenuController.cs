using NaughtyAttributes;
using Network;
using Photon.Pun;
using Photon.Realtime;
using PhotonRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneSpecific.MainMenu
{
    public class MainMenuController : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Button createRoomButton;

        [SerializeField]
        private Button joinRoomButton;

        [Scene]
        [SerializeField]
        private string roomSetupScene;

        private void Awake()
        {
            createRoomButton.interactable = false;
            joinRoomButton.interactable = false;

            PhotonNetwork.AutomaticallySyncScene = true;
            NetworkUtils.ConnectToMaster();
        }

        public override void OnEnable()
        {
            base.OnEnable();

            createRoomButton.onClick.AddListener(OnCreateRoomButtonClicked);
            joinRoomButton.onClick.AddListener(OnJoinRoomButtonClicked);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            createRoomButton.onClick.RemoveListener(OnCreateRoomButtonClicked);
            joinRoomButton.onClick.RemoveListener(OnJoinRoomButtonClicked);
        }

        public override void OnConnectedToMaster()
        {
            createRoomButton.interactable = true;
            joinRoomButton.interactable = true;
        }

        private async void OnCreateRoomButtonClicked()
        {
            await PhotonTask.CreateRoom(null);
        }

        private void OnJoinRoomButtonClicked()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("room created");
            SceneManager.LoadScene(roomSetupScene);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room");
            SceneManager.LoadScene(roomSetupScene);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            MessagePanel.ShowMessage(message);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            MessagePanel.ShowMessage(cause.ToString());
        }
    }
}