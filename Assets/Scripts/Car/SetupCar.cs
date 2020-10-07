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
            }

            Destroy(this);
        }
    }
}