using NaughtyAttributes;
using Network;
using Photon.Pun;
using Photon.Realtime;
using PhotonRx;
using TMPro;
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

        [SerializeField]
        private TMP_InputField nameInput;

        [Scene]
        [SerializeField]
        private string roomSetupScene;

        private void Awake()
        {
            createRoomButton.interactable = false;
            joinRoomButton.interactable = false;

            if (PhotonNetwork.NickName != "")
            {
                nameInput.text = PhotonNetwork.NickName;
            }

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

        private bool ValidateAndSetName()
        {
            if (nameInput.text != "")
            {
                PhotonNetwork.NickName = nameInput.text;
                return true;
            }

            MessagePanel.ShowMessage("Name should not be empty!");
            return false;
        }

        private void OnCreateRoomButtonClicked()
        {
            if (ValidateAndSetName())
                PhotonNetwork.CreateRoom(null);
        }

        private void OnJoinRoomButtonClicked()
        {
            if (ValidateAndSetName())
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
            PhotonNetwork.LoadLevel(roomSetupScene);
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