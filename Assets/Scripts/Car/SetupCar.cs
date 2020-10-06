using Photon.Pun;
using SceneSpecific.Game;

namespace Car
{
    public class SetupCar : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
    {
        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            if (info.photonView == photonView)
            {
                var anchor = transform.GetChild(0).GetChild(0);
                GameSceneData.Instance.CinemachineVirtualCamera.Follow = anchor;
                GameSceneData.Instance.CinemachineVirtualCamera.LookAt = anchor;
            }

            Destroy(this);
        }
    }
}