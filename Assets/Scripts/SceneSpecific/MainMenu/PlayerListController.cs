using TMPro;
using UnityEngine;

namespace SceneSpecific.MainMenu
{
    public class PlayerListController : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerLinePrefab;

        public void AddPlayer(string playerName)
        {
            Instantiate(playerLinePrefab, Vector3.zero, Quaternion.identity, transform)
                .GetComponentInChildren<TextMeshProUGUI>()
                ?.SetText(playerName);
        }
    }
}