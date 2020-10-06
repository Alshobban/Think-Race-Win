using Photon.Pun;
using UnityEngine;

namespace Network
{
    [RequireComponent(typeof(PhotonView))]
    public class NetworkScriptsIgnore : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour[] scriptsToIgnore;

        private void Awake()
        {
            var photonView = GetComponent<PhotonView>();
            foreach (var script in scriptsToIgnore)
            {
                if (!photonView.IsMine)
                    script.enabled = false;
            }
        }
    }
}