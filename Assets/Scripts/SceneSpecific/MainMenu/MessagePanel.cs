using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SceneSpecific.MainMenu
{
    public class MessagePanel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI messageText;

        public static async void ShowMessage(string message, float timeout = 3f, bool copyToDebug = true)
        {
            if (copyToDebug)
            {
                Debug.Log(message);
            }

            var panel = FindObjectOfType<MessagePanel>(true);
            if (panel)
            {
                panel.gameObject.SetActive(true);
                panel.messageText.text = message;
                await UniTask.Delay(TimeSpan.FromSeconds(timeout));
                panel.gameObject.SetActive(false);
            }
        }
    }
}