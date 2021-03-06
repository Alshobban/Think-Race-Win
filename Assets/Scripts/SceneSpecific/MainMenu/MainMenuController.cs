﻿using NaughtyAttributes;
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
        private Button questionEditorButton;

        [SerializeField]
        private TMP_InputField nameInput;


        [Scene]
        [SerializeField]
        private string questionEditorScene;

        [Scene]
        [SerializeField]
        private string roomSetupScene;

        private void Awake()
        {
            ToggleOnlineButtons(PhotonNetwork.IsConnected);

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
            questionEditorButton.onClick.AddListener(OnQuestionEditorButtonClicked);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            createRoomButton.onClick.RemoveListener(OnCreateRoomButtonClicked);
            joinRoomButton.onClick.RemoveListener(OnJoinRoomButtonClicked);
            questionEditorButton.onClick.RemoveListener(OnQuestionEditorButtonClicked);
        }

        private void OnQuestionEditorButtonClicked()
        {
            SceneManager.LoadScene(questionEditorScene);
        }

        public override void OnConnectedToMaster()
        {
            ToggleOnlineButtons(true);
        }

        private void ToggleOnlineButtons(bool state)
        {
            createRoomButton.interactable = state;
            joinRoomButton.interactable = state;
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
            Debug.Log($"Room created: {PhotonNetwork.CurrentRoom}");
            SceneManager.LoadScene(roomSetupScene);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"Joined Room: {PhotonNetwork.CurrentRoom}");
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