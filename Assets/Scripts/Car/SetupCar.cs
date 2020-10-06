using Network;
using Photon.Pun;
using Photon.Realtime;
using SceneSpecific.Game;

namespace Car
{
    public class SetupCar : MonoBehaviourPunCallbacks, IOnPhotonViewOwnerChange
    {
        public void OnOwnerChange(Player newOwner, Player previousOwner)
        {
            if (newOwner.Equals(PhotonNetwork.LocalPlayer))
            {
                var anchor = transform.GetChild(0).GetChild(0);
                GameSceneData.Instance.CinemachineVirtualCamera.Follow = anchor;
                GameSceneData.Instance.CinemachineVirtualCamera.LookAt = anchor;
                GetComponent<NetworkScriptsIgnore>().IgnoreScripts();
            }

            Destroy(this);
        }
    }
}