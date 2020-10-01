using NaughtyAttributes;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace SceneSpecific.RoomSetup
{
    public class LeaveRoom : MonoBehaviour
    {
        [SerializeField]
        private Button backButton;

        [Scene]
        [SerializeField]
        private string sceneToLoad;

        private void OnEnable()
        {
            backButton.onClick.AddListener(OnLeaveButtonClicked);
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveListener(OnLeaveButtonClicked);
        }

        private void OnLeaveButtonClicked()
        {
            if (PhotonNetwork.PlayerListOthers.Length > 0)
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerListOthers.RandomElement());
            }

            PhotonNetwork.LeaveRoom();
            PhotonNetwork.AutomaticallySyncScene = false;

            PhotonNetwork.LoadLevel(sceneToLoad);
        }
    }
}