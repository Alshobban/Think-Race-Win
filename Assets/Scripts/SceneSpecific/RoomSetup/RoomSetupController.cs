using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace SceneSpecific.RoomSetup
{
    public class RoomSetupController : MonoBehaviour
    {
        [SerializeField]
        private ScrollRect playerList;

        
        
        [SerializeField]
        private Button startButton;

        [SerializeField]
        private Button readyButton;

        private void Awake()
        {
            startButton.gameObject.SetActive(false);
            readyButton.gameObject.SetActive(false);
            
            // playerList.
        }
    }
}